using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TRole
{
    public int CnIdRol { get; set; }

    public string? CtNombreRol { get; set; }

    public int? CnJerarquia { get; set; }

    public virtual ICollection<TUsuario> TUsuarios { get; set; } = new List<TUsuario>();

    public virtual ICollection<TiRolXPantallaXAccion> TiRolXPantallaXAccions { get; set; } = new List<TiRolXPantallaXAccion>();
}
