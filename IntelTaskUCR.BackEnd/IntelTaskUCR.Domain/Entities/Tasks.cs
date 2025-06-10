using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class Tasks
    {
        public int CnIdTarea { get; set; }

        public int? CnTareaOrigen { get; set; }

        public string? CtTituloTarea { get; set; }

        public string CtDescripcionTarea { get; set; } = null!;

        public string? CtDescripcionEspera { get; set; }

        public byte CnIdComplejidad { get; set; }

        public byte CnIdEstado { get; set; }

        public byte CnIdPrioridad { get; set; }

        public string? CnNumeroGis { get; set; }

        public DateTime CfFechaAsignacion { get; set; }

        public DateTime CfFechaLimite { get; set; }

        public DateTime CfFechaFinalizacion { get; set; }

        public int CnUsuarioCreador { get; set; }

        public int? CnUsuarioAsignado { get; set; }

        public Tasks(int cnIdTarea, int? cnTareaOrigen, string? ctTituloTarea, string ctDescripcionTarea,
        string? ctDescripcionEspera, byte cnIdComplejidad, byte cnIdEstado, byte cnIdPrioridad, string? cnNumeroGis,
        DateTime cfFechaAsignacion, DateTime cfFechaLimite, DateTime cfFechaFinalizacion, int cnUsuarioCreador,
        int? cnUsuarioAsignado)
        {
            CnIdTarea = cnIdTarea;
            CnTareaOrigen = cnTareaOrigen;
            CtTituloTarea = ctTituloTarea;
            CtDescripcionTarea = ctDescripcionTarea;
            CtDescripcionEspera = ctDescripcionEspera;
            CnIdComplejidad = cnIdComplejidad;
            CnIdEstado = cnIdEstado;
            CnIdPrioridad = cnIdPrioridad;
            CnNumeroGis = cnNumeroGis;
            CfFechaAsignacion = cfFechaAsignacion;
            CfFechaLimite = cfFechaLimite;
            CfFechaFinalizacion = cfFechaFinalizacion;
            CnUsuarioCreador = cnUsuarioCreador;
            CnUsuarioAsignado = cnUsuarioAsignado;
        }
    }
}
