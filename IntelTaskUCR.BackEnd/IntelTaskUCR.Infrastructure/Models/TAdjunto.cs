using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TAdjunto
{
    public int CnIdAdjuntos { get; set; }

    public string CtArchivoRuta { get; set; } = null!;

    public int CnUsuarioAccion { get; set; }

    public DateTime CfFechaRegistro { get; set; }

    public virtual TUsuario CnUsuarioAccionNavigation { get; set; } = null!;
}
