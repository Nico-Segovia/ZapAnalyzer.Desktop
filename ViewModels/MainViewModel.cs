using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ZapAnalyzer.Desktop.Models;
using ZapAnalyzer.Desktop.Services;

namespace ZapAnalyzer.Desktop.ViewModels;

/// <summary>
/// Orquestador principal de la interfaz de usuario. Gestiona el estado de procesamiento y la persistencia de resultados.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly IAnalyzerService _analyzerService;
    private AnalysisSummary? _summary;
    private string _statusMessage = "Listo para procesar archivos JSON.";
    private bool _isProcessing;

    public MainViewModel()
    {
        _analyzerService = new AnalyzerService();

        // Inicialización de Comandos
        LoadJsonCommand = new RelayCommand(ExecuteLoadJson, () => !IsProcessing);
        SaveResultsCommand = new RelayCommand(ExecuteSaveResults, () => Summary != null && !IsProcessing);
        OpenWebCommand = new RelayCommand(ExecuteOpenWeb); // FIX: Inicialización del comando
    }

    public AnalysisSummary? Summary
    {
        get => _summary;
        set { _summary = value; OnPropertyChanged(); }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    public bool IsProcessing
    {
        get => _isProcessing;
        set { _isProcessing = value; OnPropertyChanged(); }
    }

    public ICommand LoadJsonCommand { get; }
    public ICommand SaveResultsCommand { get; }
    public ICommand OpenWebCommand { get; } // FIX: Declaración de la propiedad

    private async void ExecuteLoadJson()
    {
        var dialog = new OpenFileDialog
        {
            Filter = "JSON Files (*.json)|*.json",
            Title = "ZAP | Seleccionar base de datos"
        };

        if (dialog.ShowDialog() != true) return;

        try
        {
            IsProcessing = true;
            StatusMessage = "Ejecutando análisis espacial...";

            string content = await File.ReadAllTextAsync(dialog.FileName);
            Summary = await _analyzerService.ProcessJsonDataAsync(content);

            StatusMessage = Summary.ValidationDetails.Any(v => !v.IsValid)
                ? "Análisis finalizado con advertencias técnicas."
                : "Análisis finalizado exitosamente.";
        }
        catch (IOException ex)
        {
            StatusMessage = "Error de acceso al archivo.";
            MessageBox.Show($"No se pudo leer el archivo: {ex.Message}", "Error de E/S", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsProcessing = false;
        }
    }

    private async void ExecuteSaveResults()
    {
        if (Summary == null) return;

        var dialog = new SaveFileDialog
        {
            Filter = "Text Files (*.txt)|*.txt",
            FileName = $"ZAP_Auditoria_{DateTime.Now:yyyyMMdd_HHmm}.txt"
        };

        if (dialog.ShowDialog() != true) return;

        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== INFORME TÉCNICO DE AUDITORÍA - ZAP ARQUITECTOS ===");
            sb.AppendLine($"Generado: {DateTime.Now}");
            sb.AppendLine(new string('-', 60));

            sb.AppendLine("\nRESUMEN EJECUTIVO:");
            sb.AppendLine($"  Total Objetos Procesados:   {Summary.TotalProcessed}");
            sb.AppendLine($"  Objetos Válidos:            {Summary.ValidCount}");
            sb.AppendLine($"  Objetos con Errores:        {Summary.InvalidCount}");
            sb.AppendLine(new string('-', 30));
            sb.AppendLine($"  Intersecciones Detectadas:  {Summary.IntersectionsCount}");
            sb.AppendLine($"  Contenciones Detectadas:    {Summary.ContainmentCount}");
            sb.AppendLine($"  Objetos Aislados:           {Summary.IsolatedObjects.Count}");

            if (Summary.IsolatedObjects.Any())
            {
                sb.AppendLine("\nLISTADO DE OBJETOS AISLADOS:");
                foreach (var obj in Summary.IsolatedObjects)
                {
                    sb.AppendLine($"  - [ID: {obj.Id}] {obj.Name}");
                }
            }

            if (Summary.ValidationDetails.Any(v => !v.IsValid))
            {
                sb.AppendLine("\nDETALLE DE OBSERVACIONES TÉCNICAS:");
                foreach (var res in Summary.ValidationDetails.Where(v => !v.IsValid))
                {
                    sb.AppendLine($"  - [{res.Identifier}]: {res.FormattedErrors}");
                }
            }

            await File.WriteAllTextAsync(dialog.FileName, sb.ToString());
            StatusMessage = "Informe técnico exportado correctamente.";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al exportar resultados: {ex.Message}", "Error de Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Invoca al navegador predeterminado para abrir el portal corporativo.
    /// </summary>
    private void ExecuteOpenWeb()
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://zaparquitectos.com/",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo abrir el portal: {ex.Message}", "Error de Navegación");
        }
    }
}