using BenaudoOverlayManager.Authentication;
using BenaudoOverlayManager.ViewModels;

namespace BenaudoOverlayManager.Tests;

[TestClass]
public sealed class LoginViewModelTests
{
    [DataTestMethod]
    [DataRow("pilote@example.com", true)]
    [DataRow("  pilote@example.com  ", true)]
    [DataRow("pilote", false)]
    [DataRow("@example.com", false)]
    [DataRow("", false)]
    public void EmailValidationReturnsExpectedResult(string email, bool expected) =>
        Assert.AreEqual(expected, LoginViewModel.IsValidEmail(email));

    [TestMethod]
    public async Task AuthenticationFailureIsExposedToTheUser()
    {
        var service = new FakeAuthenticationService(AuthenticationResult.Failure("Identifiants invalides."));
        var viewModel = new LoginViewModel(service) { Email = "pilote@example.com" };

        viewModel.SignInCommand.Execute("incorrect");
        await service.Called.Task.WaitAsync(TimeSpan.FromSeconds(2));
        await WaitUntilAsync(() => !viewModel.IsBusy);

        Assert.AreEqual("Identifiants invalides.", viewModel.ErrorMessage);
        Assert.IsTrue(viewModel.HasError);
    }

    private static async Task WaitUntilAsync(Func<bool> condition)
    {
        var timeout = DateTime.UtcNow.AddSeconds(2);
        while (!condition() && DateTime.UtcNow < timeout) await Task.Delay(10);
        Assert.IsTrue(condition());
    }

    private sealed class FakeAuthenticationService(AuthenticationResult result) : IAuthenticationService
    {
        public TaskCompletionSource Called { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public Task<AuthenticationResult> SignInAsync(string email, string password, bool rememberSession, CancellationToken cancellationToken)
        {
            Called.TrySetResult();
            return Task.FromResult(result);
        }
    }
}
