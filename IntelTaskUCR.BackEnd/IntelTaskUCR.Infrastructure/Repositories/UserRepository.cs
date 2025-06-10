using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IntelTaskUcrContext _dbContext;
        public UserRepository(IntelTaskUcrContext dbContext) => _dbContext = dbContext;

        public async Task<List<User>> ReadUsersAsync(int? id)
        {
            var result = new List<User>();

            if (id.HasValue)
            {
                var user = await _dbContext.TUsuarios.FindAsync(id);

                if (user != null)
                {
                    result.Add(new User(
                        user.CnIdUsuario,
                        user.CtNombreUsuario,
                        user.CtCorreoUsuario,
                        user.CfFechaNacimiento,
                        user.CbEstadoUsuario,
                        user.CnIdRol
                        ));
                }
                return result;
            }

            var users = await _dbContext.TUsuarios.ToListAsync();

            if (!users.IsNullOrEmpty())
            {
                foreach (var user in users)
                {
                    result.Add(new User(
                        user.CnIdUsuario,
                        user.CtNombreUsuario,
                        user.CtCorreoUsuario,
                        user.CfFechaNacimiento,
                        user.CbEstadoUsuario,
                        user.CnIdRol
                        ));
                }
            }
            return result;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if(await _dbContext.TUsuarios.FindAsync(user.CnIdUsuario) == null)
            {
                var newUser = new TUsuario();
                newUser.CnIdUsuario = user.CnIdUsuario;
                newUser.CtNombreUsuario = user.CtNombreUsuario;
                newUser.CtCorreoUsuario = user.CtCorreoUsuario;
                newUser.CfFechaNacimiento = user.CfFechaNacimiento;
                newUser.CtContrasenna = user.CtContrasenna;
                newUser.CbEstadoUsuario = user.CbEstadoUsuario;
                newUser.CfFechaCreacionUsuario = user.CfFechaCreacionUsuario;
                newUser.CfFechaModificacionUsuario = user.CfFechaModificacionUsuario;
                newUser.CnIdRol = user.CnIdRol;

                await _dbContext.AddAsync(newUser);
                return await _dbContext.SaveChangesAsync() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateUserAsync(int idUser, Dictionary<string, object> newData)
        {
            var user = await _dbContext.TUsuarios.FindAsync(idUser);

            if (user != null)
            {
                var type = user.GetType();

                foreach (var value in newData)
                {
                    var propiedad = type.GetProperty(value.Key);
                    propiedad?.SetValue(user, value.Value);
                }
                _dbContext.Entry(user).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int idUser)
        {
            var user = await _dbContext.TUsuarios.FindAsync(idUser);
            if (user != null)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
