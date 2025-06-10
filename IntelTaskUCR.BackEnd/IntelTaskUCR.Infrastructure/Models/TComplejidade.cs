using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TComplejidade
{
    public byte CnIdComplejidad { get; set; }

    public string CtNombre { get; set; } = null!;

    public virtual ICollection<TTarea> TTareas { get; set; } = new List<TTarea>();
}
