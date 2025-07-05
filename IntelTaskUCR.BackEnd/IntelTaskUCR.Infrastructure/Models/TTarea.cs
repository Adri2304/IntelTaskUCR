using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TTarea
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

    public virtual TComplejidade CnIdComplejidadNavigation { get; set; } = null!;

    public virtual TEstado CnIdEstadoNavigation { get; set; } = null!;

    public virtual TPrioridade CnIdPrioridadNavigation { get; set; } = null!;

    public virtual TTarea? CnTareaOrigenNavigation { get; set; }

    public virtual TUsuario? CnUsuarioAsignadoNavigation { get; set; }

    public virtual TUsuario CnUsuarioCreadorNavigation { get; set; } = null!;

    public virtual ICollection<TTarea> InverseCnTareaOrigenNavigation { get; set; } = new List<TTarea>();

    public virtual ICollection<TTareasIncumplimiento> TTareasIncumplimientos { get; set; } = new List<TTareasIncumplimiento>();

    public virtual ICollection<TTareasJustificacionRechazo> TTareasJustificacionRechazos { get; set; } = new List<TTareasJustificacionRechazo>();

    public virtual ICollection<TTareasSeguimiento> TTareasSeguimientos { get; set; } = new List<TTareasSeguimiento>();
}
