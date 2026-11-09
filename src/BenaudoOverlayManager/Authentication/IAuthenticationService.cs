namespace BenaudoOverlayManager.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> SignInAsync(string email, string password, bool rememberSession, CancellationToken cancellationToken);
}
