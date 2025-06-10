using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TTareasSeguimiento
{
    public int CnIdSeguimiento { get; set; }

    public int CnIdTarea { get; set; }

    public string? CtComentario { get; set; }

    public DateTime CfFechaSeguimiento { get; set; }

    public virtual TTarea CnIdTareaNavigation { get; set; } = null!;
}
