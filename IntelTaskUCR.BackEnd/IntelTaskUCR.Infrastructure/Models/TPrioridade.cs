using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TPrioridade
{
    public byte CnIdPrioridad { get; set; }

    public string CtNombrePrioridad { get; set; } = null!;

    public string CtDescripcionPrioridad { get; set; } = null!;

    public virtual ICollection<TTarea> TTareas { get; set; } = new List<TTarea>();
}
