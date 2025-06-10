using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TEstado
{
    public byte CnIdEstado { get; set; }

    public string CtEstado { get; set; } = null!;

    public string? CtDescripcion { get; set; }

    public virtual ICollection<TBitacoraCambiosEstado> TBitacoraCambiosEstadoCnIdEstadoAnteriorNavigations { get; set; } = new List<TBitacoraCambiosEstado>();

    public virtual ICollection<TBitacoraCambiosEstado> TBitacoraCambiosEstadoCnIdEstadoNuevoNavigations { get; set; } = new List<TBitacoraCambiosEstado>();

    public virtual ICollection<TPermiso> TPermisos { get; set; } = new List<TPermiso>();

    public virtual ICollection<TTarea> TTareas { get; set; } = new List<TTarea>();
}
