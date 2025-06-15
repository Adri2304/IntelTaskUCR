using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelTaskUCR.Domain.Entities;

namespace IntelTaskUCR.Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Tasks>> ReadTaskAsync(int? id);

        Task<bool> CreateTaskAsync(Dictionary<string, object?> newTask);
        Task<bool> UpdateTaskAsync(int idTask, Dictionary<string, object?> newData);
        Task<bool> ChangeStatusTaskAsync(int idTask, int idStatus);
        Task<bool> ChangeStatusTaskAsync(int idTask, int idStatus, string message);
    }
}
