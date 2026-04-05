namespace ZapAnalyzer.Desktop.Models;

/// <summary>
/// Representa una unidad de inspección en el visor de código fuente de la interfaz.
/// </summary>
public class JsonLine
{
    public string Text { get; set; } = string.Empty;
    public bool IsError { get; set; }
}