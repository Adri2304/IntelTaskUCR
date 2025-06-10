using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IntelTaskUcrContext _dbContext;
        public TaskRepository(IntelTaskUcrContext dbContext) => _dbContext = dbContext;

        public async Task<List<Tasks>> ReadTaskAsync(int? id)
        {
            var result = new List<Tasks>();

            if (id.HasValue)
            {
                var task = await _dbContext.TTareas.FindAsync(id.Value);

                if (task != null)
                {
                    result.Add(new (task.CnIdTarea, task.CnTareaOrigen, task.CtTituloTarea, task.CtDescripcionTarea,
                        task.CtDescripcionEspera, task.CnIdComplejidad, task.CnIdEstado, task.CnIdPrioridad, task.CnNumeroGis,
                        task.CfFechaAsignacion, task.CfFechaLimite, task.CfFechaFinalizacion, task.CnUsuarioCreador,
                        task.CnUsuarioAsignado));
                }

                return result;
            }

            var tasks = await _dbContext.TTareas.ToListAsync();

            if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    result.Add(new(task.CnIdTarea, task.CnTareaOrigen, task.CtTituloTarea, task.CtDescripcionTarea,
                        task.CtDescripcionEspera, task.CnIdComplejidad, task.CnIdEstado, task.CnIdPrioridad, task.CnNumeroGis,
                        task.CfFechaAsignacion, task.CfFechaLimite, task.CfFechaFinalizacion, task.CnUsuarioCreador,
                        task.CnUsuarioAsignado));
                }
            }
            return result;
        }

        public async Task<bool> CreateTaskAsync(Dictionary<string, object?> newTask)
        {
            var task = new TTarea();
            var type = task.GetType();

            foreach (var item in newTask)
            {
                var property = type.GetProperty(item.Key);
                property?.SetValue(task, item.Value);
            }

            await _dbContext.TTareas.AddAsync(task);
            return await _dbContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateTaskAsync(int idTask, Dictionary<string, object?> newData)
        {
            var task = await _dbContext.TTareas.FindAsync(idTask);
            if (task != null)
            {
                var type = task.GetType();

                foreach (var item in newData)
                {
                    var property = type.GetProperty(item.Key);
                    property?.SetValue(task, item.Value);
                }

                _dbContext.Entry(task).State = EntityState.Modified;
                return await _dbContext.SaveChangesAsync() == 1;
            }
            return false;
        }
    }
}
