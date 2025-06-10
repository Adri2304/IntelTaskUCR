using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TNotificacione
{
    public int CnIdNotificacion { get; set; }

    public int CnTipoNotificacion { get; set; }

    public string CtTituloNotificacion { get; set; } = null!;

    public string CtTextoNotificacion { get; set; } = null!;

    public string CtCorreoOrigen { get; set; } = null!;

    public DateTime CfFechaRegistro { get; set; }

    public DateTime CfFechaNotificacion { get; set; }

    public int? CnIdRecordatorio { get; set; }

    public virtual TFrecuenciaRecordatorio? CnIdRecordatorioNavigation { get; set; }

    public virtual ICollection<TiNotificacionesXUsuario> TiNotificacionesXUsuarios { get; set; } = new List<TiNotificacionesXUsuario>();
}
