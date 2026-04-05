using System;
using System.Windows.Input;

namespace ZapAnalyzer.Desktop.ViewModels;

/// <summary>
/// Implementación de ICommand para la delegación de lógica desde la vista hacia el ViewModel.
/// </summary>
public class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => canExecute == null || canExecute();

    public void Execute(object? parameter) => execute();
}