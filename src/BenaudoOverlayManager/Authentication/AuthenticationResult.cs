namespace BenaudoOverlayManager.Authentication;

public sealed record AuthenticationResult(bool Succeeded, string? DisplayName, string? ErrorMessage)
{
    public static AuthenticationResult Success(string displayName) => new(true, displayName, null);
    public static AuthenticationResult Failure(string message) => new(false, null, message);
}
