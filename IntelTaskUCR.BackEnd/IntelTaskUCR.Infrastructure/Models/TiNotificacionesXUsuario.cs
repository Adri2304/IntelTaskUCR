using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TiNotificacionesXUsuario
{
    public int CnIdNotificacion { get; set; }

    public int CnIdUsuario { get; set; }

    public string CtCorreoDestino { get; set; } = null!;

    public virtual TNotificacione CnIdNotificacionNavigation { get; set; } = null!;

    public virtual TUsuario CnIdUsuarioNavigation { get; set; } = null!;
}
