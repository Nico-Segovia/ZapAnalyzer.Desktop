using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZapAnalyzer.Desktop.ViewModels;

/// <summary>
/// Provee la infraestructura base para la notificación de cambios en el patrón MVVM.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}