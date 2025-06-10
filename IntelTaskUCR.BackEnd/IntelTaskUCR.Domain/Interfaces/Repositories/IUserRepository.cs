using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> ReadUsersAsync(int? id);
        Task<bool> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(int idUser, Dictionary<string, object> newData);
        Task<bool> DeleteUserAsync(int idUser);
    }
}
