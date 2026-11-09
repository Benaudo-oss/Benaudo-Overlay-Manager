namespace BenaudoOverlayManager.Authentication;

/// <summary>
/// Point de remplacement du futur service commercial. Il refuse tout accès tant
/// qu'un serveur d'identité et de licences n'est pas configuré.
/// </summary>
public sealed class UnconfiguredAuthenticationService : IAuthenticationService
{
    public async Task<AuthenticationResult> SignInAsync(
        string email,
        string password,
        bool rememberSession,
        CancellationToken cancellationToken)
    {
        await Task.Delay(450, cancellationToken);
        return AuthenticationResult.Failure(
            "Le service de connexion n'est pas encore disponible. Réessayez lors de l'ouverture de la bêta.");
    }
}
