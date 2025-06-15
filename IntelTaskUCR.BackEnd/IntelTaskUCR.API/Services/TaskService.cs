using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelTaskUCR.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRejectJustifyingRepository _rejectJustifyingRepository;
        public TaskService(ITaskRepository taskRepository, IRejectJustifyingRepository rejectJustifyingRepository)
        {
            _taskRepository = taskRepository;
            _rejectJustifyingRepository = rejectJustifyingRepository;
        }
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

        public async Task<bool> ChangeStatusTaskAsync(int idTask, int idUser, int idStatus, object? additionalData)
        {

            if (additionalData != null)
            {
                if (int.TryParse(additionalData.ToString(), out int assignedUser))
                {
                    //Si viene id para asignar usuario
                    return false;
                }
                else
                {
                    //Si viene mensaje
                    return await ChangeStatusWithMessageAsync(idTask, idStatus, message: additionalData.ToString());
                }
            }
            return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus);
        }

        public async Task<bool> ChangeStatusWithMessageAsync(int idTask, int idStatus, string message)
        {
            switch (idStatus)
            {
                case 4:
                    return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus, message);

                case 6:
                    {
                        //Falta actualizar la tabla de justificacion
                        if (await _rejectJustifyingRepository.CreateRejectJustifyingAsync(idTask, message))
                            return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus);

                        return false;
                    }
            }
            return false;
        }
    }
}
