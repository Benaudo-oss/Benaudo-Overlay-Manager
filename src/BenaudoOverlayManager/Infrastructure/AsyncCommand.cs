using System.Windows.Input;

namespace BenaudoOverlayManager.Infrastructure;

public sealed class AsyncCommand<T>(Func<T?, Task> execute, Predicate<T?> canExecute) : ICommand
{
    private bool _executing;
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => !_executing && canExecute(Convert(parameter));

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter)) return;
        _executing = true; NotifyCanExecuteChanged();
        try { await execute(Convert(parameter)); }
        finally { _executing = false; NotifyCanExecuteChanged(); }
    }

    public void NotifyCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    private static T? Convert(object? parameter) => parameter is T value ? value : default;
}
