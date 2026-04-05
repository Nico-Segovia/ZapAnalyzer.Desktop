using System.Collections.Generic;
using System.Linq;

namespace ZapAnalyzer.Desktop.Models;

/// <summary>
/// Encapsula el estado de validación y la trazabilidad de errores de un objeto procesado.
/// </summary>
public class ValidationResult
{
    public ThreeDObject? Object { get; set; }
    public bool IsValid => !Errors.Any();
    public List<string> Errors { get; set; } = new();
    public string Identifier { get; set; } = "Desconocido";
    public List<JsonLine> JsonDisplayLines { get; set; } = new();
    public string? TechnicalDetail { get; set; }

    /// <summary>
    /// Agrega los errores en una sola cadena formateada para visualización técnica.
    /// </summary>
    public string FormattedErrors => string.Join("   |   ", Errors);
}