using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IntelTaskUcrContext _dbContext;

        public AuthRepository(IntelTaskUcrContext dbContext) => _dbContext = dbContext;

        public async Task<string?> GetUserPasswordAsync(string userEmail)
        {
            var password = await _dbContext.TUsuarios
                .Where(x => x.CtCorreoUsuario == userEmail)
                .Select(x => x.CtContrasenna)
                .FirstOrDefaultAsync();
                
            return password;
        }
    }
}
