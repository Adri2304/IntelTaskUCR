using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TAdjunto
{
    public int CnIdAdjuntos { get; set; }

    public int CnDocumento { get; set; }

    public string CtArchivoRuta { get; set; } = null!;

    public int CnUsuarioAccion { get; set; }

    public DateTime CfFechaRegistro { get; set; }

    public virtual TTarea CnDocumento1 { get; set; } = null!;

    public virtual TPermiso CnDocumentoNavigation { get; set; } = null!;

    public virtual TUsuario CnUsuarioAccionNavigation { get; set; } = null!;
}
