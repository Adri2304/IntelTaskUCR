using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Interfaces.Services;

namespace IntelTaskUCR.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository) => _authRepository = authRepository;

        public async Task<bool> AutenticateUser(string userName, string password)
        {
            var _password = await _authRepository.GetUserPasswordAsync(userName);
            if (_password != null)
                return password.Equals(_password);

            return false;
        }
    }
}
