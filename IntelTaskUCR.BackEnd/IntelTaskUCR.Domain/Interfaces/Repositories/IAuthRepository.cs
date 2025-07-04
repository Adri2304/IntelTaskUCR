using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<string?> GetUserPasswordAsync(string userEmail);
        Task<Dictionary<string, int>> GetAuthenticateUserInfoAsync(string userEmail);
    }
}
