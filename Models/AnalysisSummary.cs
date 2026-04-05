using System.Collections.Generic;

namespace ZapAnalyzer.Desktop.Models;

/// <summary>
/// Estructura de datos para el reporte consolidado de métricas espaciales y técnicas.
/// </summary>
public class AnalysisSummary
{
    public int TotalProcessed { get; set; }
    public int ValidCount { get; set; }
    public int InvalidCount { get; set; }
    public int IntersectionsCount { get; set; }
    public int ContainmentCount { get; set; }

    public List<ThreeDObject> IsolatedObjects { get; set; } = new();
    public List<ValidationResult> ValidationDetails { get; set; } = new();
}