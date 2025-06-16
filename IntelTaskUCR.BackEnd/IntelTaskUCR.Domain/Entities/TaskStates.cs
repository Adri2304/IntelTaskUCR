using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public enum TaskStates
    {
        Registrada = 1,
        Asignada = 2,
        EnProceso = 3,
        EnEspera = 4,
        EnRevision = 5,
        Rechazada = 6,
        Terminado = 7,
        Incumplimiento = 8

    }
}
