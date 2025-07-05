using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TBitacoraCambiosEstado
{
    public int CnIdCambioEstado { get; set; }

    public int CnIdTareaPermiso { get; set; }

    public int CnIdTipoDocumento { get; set; }

    public byte CnIdEstadoAnterior { get; set; }

    public byte CnIdEstadoNuevo { get; set; }

    public DateTime CfFechaHoraCambio { get; set; }

    public int CnIdUsuarioResponsable { get; set; }

    public string? CtObservaciones { get; set; }

    public virtual TEstado CnIdEstadoAnteriorNavigation { get; set; } = null!;

    public virtual TEstado CnIdEstadoNuevoNavigation { get; set; } = null!;

    public virtual TTiposDocumento CnIdTipoDocumentoNavigation { get; set; } = null!;

    public virtual TUsuario CnIdUsuarioResponsableNavigation { get; set; } = null!;
}
