using IntelTaskUCR.API.DTOs.Auth;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Interfaces.Services;

namespace IntelTaskUCR.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository) => _authRepository = authRepository;

        public async Task<bool> AuthenticateUser(string userEmail, string password)
        {
            var _password = await _authRepository.GetUserPasswordAsync(userEmail);
            if (_password != null)
                return password.Equals(_password);

            return false;
        }

        public async Task<Dictionary<string, int>> GetAuthenticateUserInfoAsync(string userEmail)
        {
            return await _authRepository.GetAuthenticateUserInfoAsync(userEmail);
        }
    }
}
