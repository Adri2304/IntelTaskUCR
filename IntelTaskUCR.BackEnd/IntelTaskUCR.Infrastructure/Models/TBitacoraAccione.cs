using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TBitacoraAccione
{
    public int CnIdBitacora { get; set; }

    public DateTime CfFechaHoraRegistro { get; set; }

    public int CnIdAccion { get; set; }

    public int CnIdPantalla { get; set; }

    public int CnIdUsuario { get; set; }

    public string CtInformacionImportante { get; set; } = null!;

    public int CnIdTipoDocumento { get; set; }

    /// <summary>
    /// Campo llave del documentos
    /// </summary>
    public int? CnIdTareaPermiso { get; set; }

    public virtual TAccione CnIdAccionNavigation { get; set; } = null!;

    public virtual TPantalla CnIdPantallaNavigation { get; set; } = null!;

    public virtual TTiposDocumento CnIdTipoDocumentoNavigation { get; set; } = null!;

    public virtual TUsuario CnIdUsuarioNavigation { get; set; } = null!;
}
