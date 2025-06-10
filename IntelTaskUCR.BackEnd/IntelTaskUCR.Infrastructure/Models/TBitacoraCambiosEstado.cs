using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TBitacoraCambiosEstado
{
    public int CnIdCambioEstado { get; set; }

    public int CnIdEntidadAfectada { get; set; }

    public string CtTipoEntidad { get; set; } = null!;

    public byte CnIdEstadoAnterior { get; set; }

    public byte CnIdEstadoNuevo { get; set; }

    public DateTime CfFechaHoraCambio { get; set; }

    public int CnIdUsuarioResponsable { get; set; }

    public string? CtObservaciones { get; set; }

    public virtual TEstado CnIdEstadoAnteriorNavigation { get; set; } = null!;

    public virtual TEstado CnIdEstadoNuevoNavigation { get; set; } = null!;

    public virtual TUsuario CnIdUsuarioResponsableNavigation { get; set; } = null!;
}
