using POS_API.BusinessObjects;

namespace POS_API.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginRequest request);
        Task<string> GoogleAuthenticateAsync(string idToken);
    }
}
