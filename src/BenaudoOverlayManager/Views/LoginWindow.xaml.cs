using System.Diagnostics;
using System.Windows;
using BenaudoOverlayManager.Authentication;
using BenaudoOverlayManager.Infrastructure;
using BenaudoOverlayManager.ViewModels;

namespace BenaudoOverlayManager.Views;

public partial class LoginWindow : Window
{
    private const string AccountUrl = "https://github.com/Benaudo-oss/Benaudo-Overlay-Manager";
    private readonly LoginViewModel _viewModel;

    public LoginWindow()
    {
        InitializeComponent();
        _viewModel = new LoginViewModel(new UnconfiguredAuthenticationService());
        _viewModel.SignedIn += OnSignedIn;
        DataContext = _viewModel;
        Loaded += (_, _) => EmailBox.Focus();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) =>
        (_viewModel.SignInCommand as AsyncCommand<string>)?.NotifyCanExecuteChanged();
    private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void Close_Click(object sender, RoutedEventArgs e) => Close();
    private void ForgotPassword_Click(object sender, RoutedEventArgs e) => OpenExternal(AccountUrl);
    private void CreateAccount_Click(object sender, RoutedEventArgs e) => OpenExternal(AccountUrl);

    private void OnSignedIn(object? sender, string displayName)
    {
        PasswordBox.Clear();
        MessageBox.Show($"Bienvenue {displayName}.", "Connexion", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private static void OpenExternal(string url)
    {
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch { MessageBox.Show("La page ne peut pas être ouverte pour le moment.", "Benaudo Overlay Manager", MessageBoxButton.OK, MessageBoxImage.Warning); }
    }
}
