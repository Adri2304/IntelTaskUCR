using IntelTaskUCR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Interfaces.Services
{
    public interface ITaskService
    {
        Task<List<Tasks>> ReadTaskAsync(int? idTask);
        Task<List<Tasks>> ReadTasksPerUserAsync(int idUser);
        Task<bool> CreateTaskAsync(Dictionary<string, object?> newTask);
        Task<bool> UpdateTaskAsync(int id, Dictionary<string, object?> newData);
        Task<bool> ChangeStatusTaskAsync(int idTask, int idUser, int idStatus, object? additionalData);
        Task<bool> ChangeStatusWithMessageAsync(int idTask, int idStatus, string message, int idUser);
        Task<IEnumerable<Tasks>> filterTaskPerUser(int idUser, int[] states, bool acendent);
        Task<object> AllInfoAsync(int idTask);
    }
}
