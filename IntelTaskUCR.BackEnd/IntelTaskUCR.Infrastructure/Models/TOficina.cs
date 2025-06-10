using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TOficina
{
    public int CnCodigoOficina { get; set; }

    public string CtNombreOficina { get; set; } = null!;

    public int? CnOficinaEncargada { get; set; }

    public virtual ICollection<TUsuario> CnIdUsuarios { get; set; } = new List<TUsuario>();
}
