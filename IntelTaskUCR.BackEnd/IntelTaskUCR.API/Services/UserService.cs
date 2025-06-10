using IntelTaskUCR.Domain.Interfaces.Services;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.API.DTOs;
using BCrypt.Net;
using System.Reflection.Metadata.Ecma335;

namespace IntelTaskUCR.API.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<List<User>> ReadUsersAsync(int? id)
        {
            var result = await _userRepository.ReadUsersAsync(id);
            return result;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var resultado = await _userRepository.CreateUserAsync(user);
            return resultado == true ? true : false;
        }

        public async Task<bool> UpdateUserAsync(int idUser, Dictionary<string, object> newData)
        {
            return await _userRepository.UpdateUserAsync(idUser, newData);
        }

        public async Task<bool> DeleteUserAsync(int idUser)
        {
            return await _userRepository.DeleteUserAsync(idUser);
        }
        
    }
}
