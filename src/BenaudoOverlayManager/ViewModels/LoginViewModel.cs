using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using BenaudoOverlayManager.Authentication;
using BenaudoOverlayManager.Infrastructure;

namespace BenaudoOverlayManager.ViewModels;

public sealed partial class LoginViewModel : INotifyPropertyChanged
{
    private readonly IAuthenticationService _authentication;
    private string _email = string.Empty;
    private bool _rememberSession = true;
    private bool _isBusy;
    private string? _errorMessage;

    public LoginViewModel(IAuthenticationService authentication)
    {
        _authentication = authentication;
        SignInCommand = new AsyncCommand<string>(SignInAsync, CanSignIn);
    }

    public string Email
    {
        get => _email;
        set { if (Set(ref _email, value)) RefreshCommand(); }
    }

    public bool RememberSession
    {
        get => _rememberSession;
        set => Set(ref _rememberSession, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        private set { if (Set(ref _isBusy, value)) RefreshCommand(); }
    }

    public string? ErrorMessage
    {
        get => _errorMessage;
        private set { Set(ref _errorMessage, value); OnPropertyChanged(nameof(HasError)); }
    }

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    public ICommand SignInCommand { get; }
    public event EventHandler<string>? SignedIn;
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool CanSignIn(string? password) => !IsBusy && IsValidEmail(Email) && !string.IsNullOrWhiteSpace(password);

    private async Task SignInAsync(string? password)
    {
        ErrorMessage = null;
        if (!IsValidEmail(Email)) { ErrorMessage = "Saisissez une adresse e-mail valide."; return; }
        if (string.IsNullOrWhiteSpace(password)) { ErrorMessage = "Saisissez votre mot de passe."; return; }

        IsBusy = true;
        try
        {
            var result = await _authentication.SignInAsync(Email.Trim(), password, RememberSession, CancellationToken.None);
            if (result.Succeeded) SignedIn?.Invoke(this, result.DisplayName ?? Email.Trim());
            else ErrorMessage = result.ErrorMessage ?? "Connexion impossible.";
        }
        catch (OperationCanceledException) { ErrorMessage = "Connexion annulée."; }
        catch { ErrorMessage = "Le service est momentanément inaccessible."; }
        finally { IsBusy = false; }
    }

    public static bool IsValidEmail(string? email) =>
        !string.IsNullOrWhiteSpace(email) && EmailPattern().IsMatch(email.Trim());

    private void RefreshCommand() => (SignInCommand as AsyncCommand<string>)?.NotifyCanExecuteChanged();

    private bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value; OnPropertyChanged(propertyName); return true;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    [GeneratedRegex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.CultureInvariant)]
    private static partial Regex EmailPattern();
}
