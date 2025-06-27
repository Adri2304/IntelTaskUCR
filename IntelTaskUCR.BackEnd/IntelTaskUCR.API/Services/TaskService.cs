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
        private readonly IMachineStateService _MachineStateService;
        public TaskService(ITaskRepository taskRepository, IRejectJustifyingRepository rejectJustifyingRepository, IMachineStateService machineStateService)
        {
            _taskRepository = taskRepository;
            _rejectJustifyingRepository = rejectJustifyingRepository;
            _MachineStateService = machineStateService;
        }
        public async Task<List<Tasks>> ReadTaskAsync(int? idTask)
        {
            return await _taskRepository.ReadTaskAsync(idTask);
        }
        public async Task<List<Tasks>> ReadTasksPerUserAsync(int idUser)
        {
            var tasks = await _taskRepository.ReadTasksPerUserAsync(idUser);
            var filteredTasks = tasks.Where(x => x.CnIdEstado != 9).ToList();
            return filteredTasks;
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
                    return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus, assignedUser);
                }
                else
                {
                    //Si viene mensaje
                    return await ChangeStatusWithMessageAsync(idTask, idStatus, message: additionalData.ToString()!);
                }
            }
            //Si no viene nada y solamente es cambiar el estado
            if (idStatus == 7)
                return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus, "");
            return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus);
        }

        public async Task<bool> ChangeStatusWithMessageAsync(int idTask, int idStatus, string message)
        {
            switch (idStatus)
            {
                case 4: //En espera
                    return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus, message);

                case 6://Rechazado
                    {
                        if (await _rejectJustifyingRepository.CreateRejectJustifyingAsync(idTask, message))
                            return await _taskRepository.ChangeStatusTaskAsync(idTask, idStatus);

                        return false;
                    }
            }
            return false;
        }
    }
}
