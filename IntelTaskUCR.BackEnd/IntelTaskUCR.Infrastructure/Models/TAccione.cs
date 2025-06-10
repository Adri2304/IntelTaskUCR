using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TAccione
{
    public int CnIdAccion { get; set; }

    public string CtDescripcionAccion { get; set; } = null!;

    public virtual ICollection<TBitacoraAccione> TBitacoraAcciones { get; set; } = new List<TBitacoraAccione>();

    public virtual ICollection<TiRolXPantallaXAccion> TiRolXPantallaXAccions { get; set; } = new List<TiRolXPantallaXAccion>();
}
