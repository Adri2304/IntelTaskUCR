using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class Request
    {
        public int CnIdPermiso { get; set; }

        public string? CtTituloPermiso { get; set; }

        public string CtDescripcionPermiso { get; set; } = null!;

        public byte CnIdEstado { get; set; }

        public string? CtDescripcionRechazo { get; set; }

        public DateTime CfFechaHoraRegistro { get; set; }

        public DateTime CfFechaHoraInicioPermiso { get; set; }

        public DateTime CfFechaHoraFinPermiso { get; set; }

        public int CnUsuarioCreador { get; set; }

        public Request(int cnIdPermiso, string? ctTituloPermiso, string ctDescripcionPermiso,byte cnIdEstado,
            string? ctDescripcionRechazo, DateTime cfFechaHoraRegistro,DateTime cfFechaHoraInicioPermiso,
            DateTime cfFechaHoraFinPermiso,int cnUsuarioCreador)
        {
            CnIdPermiso = cnIdPermiso;
            CtTituloPermiso = ctTituloPermiso;
            CtDescripcionPermiso = ctDescripcionPermiso;
            CnIdEstado = cnIdEstado;
            CtDescripcionRechazo = ctDescripcionRechazo;
            CfFechaHoraRegistro = cfFechaHoraRegistro;
            CfFechaHoraInicioPermiso = cfFechaHoraInicioPermiso;
            CfFechaHoraFinPermiso = cfFechaHoraFinPermiso;
            CnUsuarioCreador = cnUsuarioCreador;
        }
    }
}
