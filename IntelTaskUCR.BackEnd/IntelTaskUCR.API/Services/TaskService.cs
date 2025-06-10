using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository) => _taskRepository = taskRepository;
        public async Task<List<Tasks>> ReadTaskAsync(int? idTask)
        {
            return await _taskRepository.ReadTaskAsync(idTask);
        }

        public async Task<bool> CreateTaskAsync(Dictionary<string, object?> newTask)
        {
            return await _taskRepository.CreateTaskAsync(newTask);
        }

        public async Task<bool> UpdateTaskAsync(int idTask, Dictionary<string, object?> newData)
        {
            return await _taskRepository.UpdateTaskAsync(idTask, newData);
        }
    }
}
