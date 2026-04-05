using System.Threading.Tasks;
using ZapAnalyzer.Desktop.Models;

namespace ZapAnalyzer.Desktop.Services;

/// <summary>
/// Define el contrato para la validación y el análisis geométrico de datos espaciales.
/// </summary>
public interface IAnalyzerService
{
    /// <summary>
    /// Ejecuta el procesamiento integral de datos JSON, incluyendo auditoría técnica y relaciones espaciales.
    /// </summary>
    Task<AnalysisSummary> ProcessJsonDataAsync(string jsonContent);
}