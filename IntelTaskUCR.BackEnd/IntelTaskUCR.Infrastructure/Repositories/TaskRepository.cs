using IntelTaskUCR.Domain.Interfaces.Repositories;
using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;


namespace IntelTaskUCR.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IntelTaskUcrContext _dbContext;
        private readonly IBitacoraEstadosRepository _bitacoraEstadosRepository;
        public TaskRepository(IntelTaskUcrContext dbContext, IBitacoraEstadosRepository bitacoraEstadosRepository)
        {
            _dbContext = dbContext;
            _bitacoraEstadosRepository = bitacoraEstadosRepository;
        }

        public async Task<List<Tasks>> ReadTaskAsync(int? id)
        {
            var result = new List<Tasks>();

            if (id.HasValue)
            {
                var task = await _dbContext.TTareas.FindAsync(id.Value);

                if (task != null)
                {
                    result.Add(new(task.CnIdTarea, task.CnTareaOrigen, task.CtTituloTarea, task.CtDescripcionTarea,
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

        public async Task<List<Tasks>> ReadTasksPerUserAsync(int idUser)
        {
            var result = new List<Tasks>();

            var tasks = await _dbContext.TTareas
                .Where(x => x.CnUsuarioAsignado == idUser || x.CnUsuarioCreador == idUser)
                //.OrderByDescending(x => x)
                .ToListAsync();

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

        public async Task<bool> ChangeStatusTaskAsync(int idTask, int idStatus, int idUser)
        {
            int idOldState = await _dbContext.TTareas
                .Where(x => x.CnIdTarea == idTask)
                .Select(x => x.CnIdEstado)
                .FirstOrDefaultAsync();

            var task = await _dbContext.TTareas.FindAsync(idTask);
            task!.CnIdEstado = (byte)idStatus;
            _dbContext.Entry(task).State = EntityState.Modified;

            if (await _dbContext.SaveChangesAsync() == 1)
            {
                _ = await _bitacoraEstadosRepository.createAsync(idTask, idOldState, idStatus, idUser);
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeStatusTaskAsync(int idTask, int idStatus, int idUsuarioAsignado, int idUser)
        {
            int idOldState = await _dbContext.TTareas
                .Where(x => x.CnIdTarea == idTask)
                .Select(x => x.CnIdEstado)
                .FirstOrDefaultAsync();

            var task = await _dbContext.TTareas.FindAsync(idTask);
            task!.CnIdEstado = (byte)idStatus;
            task.CnUsuarioAsignado = idUsuarioAsignado;
            task.CfFechaAsignacion = DateTime.Now;
            _dbContext.Entry(task).State = EntityState.Modified;

            if (await _dbContext.SaveChangesAsync() == 1)
            {
                _ = await _bitacoraEstadosRepository.createAsync(idTask, idOldState, idStatus, idUser);
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeStatusTaskAsync(int idTask, int idStatus, string message, int idUser)
        {
            int idOldState = await _dbContext.TTareas
                .Where(x => x.CnIdTarea == idTask)
                .Select(x => x.CnIdEstado)
                .FirstOrDefaultAsync();

            var task = await _dbContext.TTareas.FindAsync(idTask);
            task!.CnIdEstado = (byte)idStatus;

            if (message.IsNullOrEmpty())
                task.CfFechaFinalizacion = DateTime.Now;
            else
                task!.CtDescripcionEspera = message;

            _dbContext.Entry(task).State = EntityState.Modified;
            if (await _dbContext.SaveChangesAsync() == 1)
            {
                _ = await _bitacoraEstadosRepository.createAsync(idTask, idOldState, idStatus, idUser);
                return true;
            }
            return false;
        }

        public async Task<object> AllInfoAsync(int idTask)
        {
            // 1. Obtener la tarea
            var task = await _dbContext.TTareas.FirstOrDefaultAsync(t => t.CnIdTarea == idTask);
            if (task == null)
                return null;

            var tarea = new Tasks(task.CnIdTarea, task.CnTareaOrigen, task.CtTituloTarea, task.CtDescripcionTarea,
                        task.CtDescripcionEspera, task.CnIdComplejidad, task.CnIdEstado, task.CnIdPrioridad, task.CnNumeroGis,
                        task.CfFechaAsignacion, task.CfFechaLimite, task.CfFechaFinalizacion, task.CnUsuarioCreador,
                        task.CnUsuarioAsignado);

            // 2. Obtener usuarios relacionados (por id de creador y asignado)
            var idsUsuarios = new List<int>();

            idsUsuarios.Add(tarea.CnUsuarioCreador);
            if (tarea.CnUsuarioAsignado.HasValue)
            {
                idsUsuarios.Add(tarea.CnUsuarioAsignado.Value);
            }

            var usuarios = await (
                from u in _dbContext.TUsuarios
                join r in _dbContext.TRoles on u.CnIdRol equals r.CnIdRol
                where idsUsuarios.Contains(u.CnIdUsuario)
                select new UsuarioConRolDto
                {
                    IdUsuario = u.CnIdUsuario,
                    NombreUsuario = u.CtNombreUsuario,
                    CorreoUsuario = u.CtCorreoUsuario,
                    NombreRol = r.CtNombreRol!
                }
            ).ToListAsync();


            // 3. Obtener bitácora de cambios de estado
            var bitacora = await _dbContext.TBitacoraCambiosEstados
                .Where(b => b.CnIdTareaPermiso == idTask)
                .Select(x => new
                {
                    cnIdEstadoAnterior = x.CnIdEstadoAnterior,
                    cnIdEstadoNuevo = x.CnIdEstadoNuevo,
                    cfFechaHoraCambio = x.CfFechaHoraCambio,
                    cnIdUsuarioResponsable = x.CnIdUsuarioResponsable,
                })
                .ToListAsync();

            //return bitacora;

            // 4. Obtener justificaciones de rechazo
            //var justificaciones = await _dbContext.TTareasJustificacionRechazos
            //    .Where(j => j.CnIdTarea == idTask)
            //    .ToListAsync();

            var justificaciones = await _dbContext.TTareasJustificacionRechazos
                .Where(x => x.CnIdTarea == idTask)
                .Select(x => new
                {
                    cfFechaHoraRechazo = x.CfFechaHoraRechazo,
                    ctDescripcionRechazo = x.CtDescripcionRechazo
                })
                .ToListAsync();

            // Retornar objeto agrupado
            return new { tarea, usuarios, bitacora, justificaciones };
        }
    }
    public class TaskDetailDto
    {
        public TTarea? Tarea { get; set; }
        public List<UsuarioConRolDto> UsuariosRelacionados { get; set; } = [];
        public List<TBitacoraCambiosEstado> BitacoraEstados { get; set; } = [];
        public List<TTareasJustificacionRechazo> JustificacionesRechazo { get; set; } = [];
    }

    public class UsuarioConRolDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string CorreoUsuario { get; set; } = string.Empty;
        public string NombreRol { get; set; } = string.Empty;
    }

}
