using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> AuthenticateUser(string userEmail, string password);
        Task<Dictionary<string, int>> GetAuthenticateUserInfoAsync(string userEmail);
    }
}
