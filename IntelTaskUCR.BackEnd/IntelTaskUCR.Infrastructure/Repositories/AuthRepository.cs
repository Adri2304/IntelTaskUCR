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
            var data = await _dbContext.TUsuarios
                .Where(x => x.CtCorreoUsuario == userEmail)
                .Select(x => x.CtContrasenna)
                .FirstOrDefaultAsync();
                
            return data;
        }

        public async Task<Dictionary<string, int>> GetAuthenticateUserInfoAsync(string userEmail)
        {
            var result = await _dbContext.TUsuarios
                .Where(x => x.CtCorreoUsuario == userEmail)
                .Select(x => new {
                    x.CnIdRol,
                    x.CnIdUsuario
                })
                .FirstOrDefaultAsync();

            if (result == null)
                return new Dictionary<string, int>();

            return new Dictionary<string, int>
            {
                { "cnIdRol", result.CnIdRol },
                { "cnIdUsuario", result.CnIdUsuario }
            };
        }
    }
}
