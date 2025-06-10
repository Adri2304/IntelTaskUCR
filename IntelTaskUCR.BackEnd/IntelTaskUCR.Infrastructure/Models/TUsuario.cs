using System;
using System.Collections.Generic;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class TUsuario
{
    public int CnIdUsuario { get; set; }

    public string CtNombreUsuario { get; set; } = null!;

    public string CtCorreoUsuario { get; set; } = null!;

    public DateOnly? CfFechaNacimiento { get; set; }

    public string CtContrasenna { get; set; } = null!;

    public bool CbEstadoUsuario { get; set; }

    public DateOnly? CfFechaCreacionUsuario { get; set; }

    public DateOnly? CfFechaModificacionUsuario { get; set; }

    public int CnIdRol { get; set; }

    public virtual TRole CnIdRolNavigation { get; set; } = null!;

    public virtual ICollection<TAdjunto> TAdjuntos { get; set; } = new List<TAdjunto>();

    public virtual ICollection<TBitacoraAccione> TBitacoraAcciones { get; set; } = new List<TBitacoraAccione>();

    public virtual ICollection<TBitacoraCambiosEstado> TBitacoraCambiosEstados { get; set; } = new List<TBitacoraCambiosEstado>();

    public virtual ICollection<TFrecuenciaRecordatorio> TFrecuenciaRecordatorios { get; set; } = new List<TFrecuenciaRecordatorio>();

    public virtual ICollection<TPermiso> TPermisos { get; set; } = new List<TPermiso>();

    public virtual ICollection<TTarea> TTareaCnUsuarioAsignadoNavigations { get; set; } = new List<TTarea>();

    public virtual ICollection<TTarea> TTareaCnUsuarioCreadorNavigations { get; set; } = new List<TTarea>();

    public virtual ICollection<TiNotificacionesXUsuario> TiNotificacionesXUsuarios { get; set; } = new List<TiNotificacionesXUsuario>();

    public virtual ICollection<TOficina> CnCodigoOficinas { get; set; } = new List<TOficina>();
}
