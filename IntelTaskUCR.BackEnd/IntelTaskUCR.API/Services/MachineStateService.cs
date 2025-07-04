using IntelTaskUCR.Domain.Entities;
using IntelTaskUCR.Domain.Interfaces.Services;
using Stateless;

namespace IntelTaskUCR.API.Services
{
    public class MachineStateService : IMachineStateService
    {
        private StateMachine<TaskStates, TaskStates> ConfigurarStateMachine(TaskStates estadoActual, Action<TaskStates> actualizarEstado)
        {
            var stateMachine = new StateMachine<TaskStates, TaskStates>(() => estadoActual, s => actualizarEstado(s));

            stateMachine.Configure(TaskStates.Registrada)
                .Permit(TaskStates.Asignada, TaskStates.Asignada);

            stateMachine.Configure(TaskStates.Asignada)
                .Permit(TaskStates.EnProceso, TaskStates.EnProceso);

            stateMachine.Configure(TaskStates.EnProceso)
                .Permit(TaskStates.EnEspera, TaskStates.EnEspera)
                .Permit(TaskStates.EnRevision, TaskStates.EnRevision);

            stateMachine.Configure(TaskStates.EnEspera)
                .Permit(TaskStates.EnProceso, TaskStates.EnProceso);

            stateMachine.Configure(TaskStates.EnRevision)
                .Permit(TaskStates.Rechazada, TaskStates.Rechazada)
                .Permit(TaskStates.Terminado, TaskStates.Terminado);

            stateMachine.Configure(TaskStates.Rechazada)
                .Permit(TaskStates.Asignada, TaskStates.Asignada);


            // Falta terminar con el estado incumplimiento y el rechazado

            return stateMachine;
        }

        public IEnumerable<TaskStates> ObtenerTransicionesValidas(TaskStates estadoActual)
        {
            TaskStates estadoTemp = estadoActual;
            var stateMachine = ConfigurarStateMachine(estadoTemp, s => estadoTemp = s);
            return stateMachine.PermittedTriggers;
        }

        public TaskStates EjecutarTransicion(TaskStates estadoActual, TaskStates nuevoEstado)
        {
            TaskStates estadoTemp = estadoActual;
            var stateMachine = ConfigurarStateMachine(estadoTemp, s => estadoTemp = s);

            if (!stateMachine.CanFire(nuevoEstado))
                throw new InvalidOperationException($"No se puede transicionar de {estadoActual} a {nuevoEstado}");

            stateMachine.Fire(nuevoEstado);
            return estadoTemp;
        }
    }
}
