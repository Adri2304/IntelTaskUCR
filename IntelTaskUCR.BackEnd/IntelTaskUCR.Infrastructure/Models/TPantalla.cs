using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TPantalla
{
    public int CnIdPantalla { get; set; }

    public string? CtDescripcion { get; set; }

    public virtual ICollection<TBitacoraAccione> TBitacoraAcciones { get; set; } = new List<TBitacoraAccione>();

    public virtual ICollection<TiRolXPantallaXAccion> TiRolXPantallaXAccions { get; set; } = new List<TiRolXPantallaXAccion>();
}
