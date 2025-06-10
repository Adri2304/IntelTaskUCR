using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TiRolXPantallaXAccion
{
    public int CnIdPantalla { get; set; }

    public int CnIdAccion { get; set; }

    public int CnIdRol { get; set; }

    public virtual TAccione CnIdAccionNavigation { get; set; } = null!;

    public virtual TPantalla CnIdPantallaNavigation { get; set; } = null!;

    public virtual TRole CnIdRolNavigation { get; set; } = null!;
}
