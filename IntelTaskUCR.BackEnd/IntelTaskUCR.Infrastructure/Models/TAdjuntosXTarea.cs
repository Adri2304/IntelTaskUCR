using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TAdjuntosXTarea
{
    public int CnIdAdjuntos { get; set; }

    public int CnIdTarea { get; set; }

    public virtual TAdjunto CnIdAdjuntosNavigation { get; set; } = null!;

    public virtual TTarea CnIdTareaNavigation { get; set; } = null!;
}
