using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TTiposDocumento
{
    public int CnIdTipoDocumento { get; set; }

    public string CtNombreTipoDocumento { get; set; } = null!;

    public virtual ICollection<TBitacoraAccione> TBitacoraAcciones { get; set; } = new List<TBitacoraAccione>();
}
