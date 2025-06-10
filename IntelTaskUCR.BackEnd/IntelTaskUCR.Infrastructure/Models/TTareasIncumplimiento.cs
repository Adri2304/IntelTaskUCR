using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TTareasIncumplimiento
{
    public int CnIdTareaIncumplimiento { get; set; }

    public int CnIdTarea { get; set; }

    public string? CtJustificacionIncumplimiento { get; set; }

    public DateTime CfFechaIncumplimiento { get; set; }

    public virtual TTarea CnIdTareaNavigation { get; set; } = null!;
}
