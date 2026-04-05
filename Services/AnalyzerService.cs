using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ZapAnalyzer.Desktop.Models;

namespace ZapAnalyzer.Desktop.Services;

/// <summary>
/// Servicio de motor de análisis para procesamiento de volúmenes AABB y validación de esquemas.
/// </summary>
public class AnalyzerService : IAnalyzerService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true
    };

    public async Task<AnalysisSummary> ProcessJsonDataAsync(string jsonContent)
    {
        var summary = new AnalysisSummary();

        try
        {
            var rawObjects = JsonSerializer.Deserialize<List<ThreeDObject>>(jsonContent, JsonOptions);
            if (rawObjects == null || !rawObjects.Any()) return summary;

            summary.TotalProcessed = rawObjects.Count;
            var validObjects = new List<ThreeDObject>();
            var usedIds = new HashSet<int>();

            foreach (var obj in rawObjects)
            {
                var failedProperties = new List<string>();
                var result = ValidateTechnicalRules(obj, usedIds, failedProperties);

                GenerateJsonTraceability(result, obj, failedProperties);
                summary.ValidationDetails.Add(result);

                if (result.IsValid && result.Object != null)
                {
                    validObjects.Add(result.Object);
                    usedIds.Add(result.Object.Id);
                }
            }

            summary.ValidCount = validObjects.Count;
            summary.InvalidCount = summary.TotalProcessed - summary.ValidCount;

            PerformSpatialAnalysis(validObjects, summary);
        }
        catch (JsonException ex)
        {
            summary.ValidationDetails.Add(new ValidationResult
            {
                Identifier = "Estructura JSON",
                Errors = { "Error crítico en la sintaxis del archivo." },
                TechnicalDetail = $"Posición: L{ex.LineNumber} C{ex.BytePositionInLine} | Msg: {ex.Message}"
            });
        }

        return await Task.FromResult(summary);
    }

    private ValidationResult ValidateTechnicalRules(ThreeDObject obj, HashSet<int> usedIds, List<string> failedProps)
    {
        var result = new ValidationResult { Object = obj, Identifier = obj.Name ?? obj.Id.ToString() };

        if (usedIds.Contains(obj.Id))
        {
            result.Errors.Add($"ID duplicado ({obj.Id}): El identificador debe ser único.");
            failedProps.Add("\"Id\"");
        }

        if (string.IsNullOrWhiteSpace(obj.Category))
        {
            result.Errors.Add("Categoría faltante: El tipo de objeto es obligatorio.");
            failedProps.Add("\"Category\"");
        }

        if (obj.Width <= 0)
        {
            result.Errors.Add($"Ancho inválido ({obj.Width}): Debe ser mayor a 0.");
            failedProps.Add("\"Width\"");
        }

        if (obj.Height <= 0)
        {
            result.Errors.Add($"Alto inválido ({obj.Height}): Debe ser mayor a 0.");
            failedProps.Add("\"Height\"");
        }

        if (obj.Depth <= 0)
        {
            result.Errors.Add($"Profundidad inválida ({obj.Depth}): Debe ser mayor a 0.");
            failedProps.Add("\"Depth\"");
        }

        return result;
    }

    private void GenerateJsonTraceability(ValidationResult result, ThreeDObject obj, List<string> failedProps)
    {
        var rawLines = JsonSerializer.Serialize(obj, JsonOptions).Split(Environment.NewLine);
        foreach (var line in rawLines)
        {
            result.JsonDisplayLines.Add(new JsonLine
            {
                Text = line,
                IsError = failedProps.Any(p => line.Contains(p))
            });
        }
    }

    private void PerformSpatialAnalysis(List<ThreeDObject> objects, AnalysisSummary summary)
    {
        var relatedIds = new HashSet<int>();
        var uniqueIntersections = new HashSet<(int, int)>();
        var containedIds = new HashSet<int>();

        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = 0; j < objects.Count; j++)
            {
                if (i == j) continue;
                var a = objects[i];
                var b = objects[j];

                if (a.Contains(b))
                {
                    containedIds.Add(b.Id);
                    relatedIds.UnionWith([a.Id, b.Id]);
                }

                if (a.IntersectsWith(b))
                {
                    var pair = a.Id < b.Id ? (a.Id, b.Id) : (b.Id, a.Id);
                    uniqueIntersections.Add(pair);
                    relatedIds.UnionWith([a.Id, b.Id]);
                }
            }
        }
        summary.ContainmentCount = containedIds.Count;
        summary.IntersectionsCount = uniqueIntersections.Count;
        summary.IsolatedObjects = objects.Where(o => !relatedIds.Contains(o.Id)).ToList();
    }
}