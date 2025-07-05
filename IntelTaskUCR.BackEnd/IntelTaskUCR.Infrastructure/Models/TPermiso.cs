using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TPermiso
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

    public virtual TEstado CnIdEstadoNavigation { get; set; } = null!;

    public virtual TUsuario CnUsuarioCreadorNavigation { get; set; } = null!;
}
