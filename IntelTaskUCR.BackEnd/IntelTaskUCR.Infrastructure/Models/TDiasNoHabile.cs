using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TDiasNoHabile
{
    public int CnIdDiasNoHabiles { get; set; }

    public DateOnly CfFechaInicio { get; set; }

    public DateOnly CfFechaFin { get; set; }

    public string CtDescripcion { get; set; } = null!;

    public bool CbActivo { get; set; }
}
