using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IntelTaskUCR.Infrastructure.Models;

public partial class IntelTaskUcrContext : DbContext
{
    public IntelTaskUcrContext()
    {
    }

    public IntelTaskUcrContext(DbContextOptions<IntelTaskUcrContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAccione> TAcciones { get; set; }

    public virtual DbSet<TAdjunto> TAdjuntos { get; set; }

    public virtual DbSet<TBitacoraAccione> TBitacoraAcciones { get; set; }

    public virtual DbSet<TBitacoraCambiosEstado> TBitacoraCambiosEstados { get; set; }

    public virtual DbSet<TComplejidade> TComplejidades { get; set; }

    public virtual DbSet<TDemo> TDemos { get; set; }

    public virtual DbSet<TDiasNoHabile> TDiasNoHabiles { get; set; }

    public virtual DbSet<TEstado> TEstados { get; set; }

    public virtual DbSet<TFrecuenciaRecordatorio> TFrecuenciaRecordatorios { get; set; }

    public virtual DbSet<TNotificacione> TNotificaciones { get; set; }

    public virtual DbSet<TOficina> TOficinas { get; set; }

    public virtual DbSet<TPantalla> TPantallas { get; set; }

    public virtual DbSet<TPermiso> TPermisos { get; set; }

    public virtual DbSet<TPrioridade> TPrioridades { get; set; }

    public virtual DbSet<TRole> TRoles { get; set; }

    public virtual DbSet<TTarea> TTareas { get; set; }

    public virtual DbSet<TTareasIncumplimiento> TTareasIncumplimientos { get; set; }

    public virtual DbSet<TTareasJustificacionRechazo> TTareasJustificacionRechazos { get; set; }

    public virtual DbSet<TTareasSeguimiento> TTareasSeguimientos { get; set; }

    public virtual DbSet<TTiposDocumento> TTiposDocumentos { get; set; }

    public virtual DbSet<TUsuario> TUsuarios { get; set; }

    public virtual DbSet<TiNotificacionesXUsuario> TiNotificacionesXUsuarios { get; set; }

    public virtual DbSet<TiRolXPantallaXAccion> TiRolXPantallaXAccions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAccione>(entity =>
        {
            entity.HasKey(e => e.CnIdAccion).HasName("PK__T_Accion__7A7E29FCA4860424");

            entity.ToTable("T_Acciones");

            entity.Property(e => e.CnIdAccion).HasColumnName("CN_Id_accion");
            entity.Property(e => e.CtDescripcionAccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CT_Descripcion_accion");
        });

        modelBuilder.Entity<TAdjunto>(entity =>
        {
            entity.HasKey(e => new { e.CnIdAdjuntos, e.CnDocumento }).HasName("PK__T_Adjunt__F001B021DBC172C2");

            entity.ToTable("T_Adjuntos");

            entity.Property(e => e.CnIdAdjuntos)
                .ValueGeneratedOnAdd()
                .HasColumnName("CN_Id_adjuntos");
            entity.Property(e => e.CnDocumento).HasColumnName("CN_Documento");
            entity.Property(e => e.CfFechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_registro");
            entity.Property(e => e.CnUsuarioAccion).HasColumnName("CN_Usuario_accion");
            entity.Property(e => e.CtArchivoRuta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CT_Archivo_ruta");

            entity.HasOne(d => d.CnDocumentoNavigation).WithMany(p => p.TAdjuntos)
                .HasForeignKey(d => d.CnDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Adjuntos_T_Permisos");

            entity.HasOne(d => d.CnDocumento1).WithMany(p => p.TAdjuntos)
                .HasForeignKey(d => d.CnDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Adjuntos_T_Tareas");

            entity.HasOne(d => d.CnUsuarioAccionNavigation).WithMany(p => p.TAdjuntos)
                .HasForeignKey(d => d.CnUsuarioAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Adjuntos_T_Usuarios");
        });

        modelBuilder.Entity<TBitacoraAccione>(entity =>
        {
            entity.HasKey(e => e.CnIdBitacora).HasName("PK_CN_Id_bitacora");

            entity.ToTable("T_Bitacora_Acciones");

            entity.Property(e => e.CnIdBitacora).HasColumnName("CN_Id_bitacora");
            entity.Property(e => e.CfFechaHoraRegistro)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_registro");
            entity.Property(e => e.CnDocumento)
                .HasComment("Campo llave del documentos")
                .HasColumnName("CN_Documento");
            entity.Property(e => e.CnIdAccion).HasColumnName("CN_Id_accion");
            entity.Property(e => e.CnIdPantalla).HasColumnName("CN_Id_pantalla");
            entity.Property(e => e.CnIdTipoDocumento).HasColumnName("CN_Id_tipo_documento");
            entity.Property(e => e.CnIdUsuario).HasColumnName("CN_id_usuario");
            entity.Property(e => e.CtInformacionImportante)
                .IsUnicode(false)
                .HasColumnName("CT_informacion_importante");

            entity.HasOne(d => d.CnIdAccionNavigation).WithMany(p => p.TBitacoraAcciones)
                .HasForeignKey(d => d.CnIdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Acciones_T_Acciones");

            entity.HasOne(d => d.CnIdPantallaNavigation).WithMany(p => p.TBitacoraAcciones)
                .HasForeignKey(d => d.CnIdPantalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Acciones_T_Pantallas");

            entity.HasOne(d => d.CnIdTipoDocumentoNavigation).WithMany(p => p.TBitacoraAcciones)
                .HasForeignKey(d => d.CnIdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Acciones_T_Tipos_documentos");

            entity.HasOne(d => d.CnIdUsuarioNavigation).WithMany(p => p.TBitacoraAcciones)
                .HasForeignKey(d => d.CnIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Acciones_T_Usuarios");
        });

        modelBuilder.Entity<TBitacoraCambiosEstado>(entity =>
        {
            entity.HasKey(e => e.CnIdCambioEstado).HasName("PK__T_Bitaco__B906A6F3523AB22C");

            entity.ToTable("T_Bitacora_Cambios_Estados");

            entity.Property(e => e.CnIdCambioEstado).HasColumnName("CN_Id_cambio_estado");
            entity.Property(e => e.CfFechaHoraCambio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_cambio");
            entity.Property(e => e.CnIdEntidadAfectada).HasColumnName("CN_Id_entidad_afectada");
            entity.Property(e => e.CnIdEstadoAnterior).HasColumnName("CN_Id_estado_anterior");
            entity.Property(e => e.CnIdEstadoNuevo).HasColumnName("CN_Id_estado_nuevo");
            entity.Property(e => e.CnIdUsuarioResponsable).HasColumnName("CN_Id_usuario_responsable");
            entity.Property(e => e.CtObservaciones)
                .HasColumnType("text")
                .HasColumnName("CT_Observaciones");
            entity.Property(e => e.CtTipoEntidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CT_Tipo_entidad");

            entity.HasOne(d => d.CnIdEstadoAnteriorNavigation).WithMany(p => p.TBitacoraCambiosEstadoCnIdEstadoAnteriorNavigations)
                .HasForeignKey(d => d.CnIdEstadoAnterior)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Cambios_Estados_T_Estados");

            entity.HasOne(d => d.CnIdEstadoNuevoNavigation).WithMany(p => p.TBitacoraCambiosEstadoCnIdEstadoNuevoNavigations)
                .HasForeignKey(d => d.CnIdEstadoNuevo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Cambios_Estados_T_Estados1");

            entity.HasOne(d => d.CnIdUsuarioResponsableNavigation).WithMany(p => p.TBitacoraCambiosEstados)
                .HasForeignKey(d => d.CnIdUsuarioResponsable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Bitacora_Cambios_Estados_T_Usuarios");
        });

        modelBuilder.Entity<TComplejidade>(entity =>
        {
            entity.HasKey(e => e.CnIdComplejidad).HasName("PK__T_Comple__E1A713D709E9B2FF");

            entity.ToTable("T_Complejidades");

            entity.HasIndex(e => e.CtNombre, "UQ__T_Comple__04C06E122FDE0E2D").IsUnique();

            entity.Property(e => e.CnIdComplejidad)
                .ValueGeneratedOnAdd()
                .HasColumnName("CN_Id_complejidad");
            entity.Property(e => e.CtNombre)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CT_Nombre");
        });

        modelBuilder.Entity<TDemo>(entity =>
        {
            entity.HasKey(e => e.TnCodigo);

            entity.ToTable("T_Demo");

            entity.Property(e => e.TnCodigo)
                .ValueGeneratedNever()
                .HasColumnName("TN_Codigo");
            entity.Property(e => e.TcDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TC_Descripcion");
        });

        modelBuilder.Entity<TDiasNoHabile>(entity =>
        {
            entity.HasKey(e => e.CnIdDiasNoHabiles).HasName("PK__T_Dias_N__D1FD7E26F2BA044F");

            entity.ToTable("T_Dias_No_Habiles");

            entity.Property(e => e.CnIdDiasNoHabiles).HasColumnName("CN_Id_dias_no_habiles");
            entity.Property(e => e.CbActivo).HasColumnName("CB_Activo");
            entity.Property(e => e.CfFechaFin).HasColumnName("CF_Fecha_fin");
            entity.Property(e => e.CfFechaInicio).HasColumnName("CF_Fecha_inicio");
            entity.Property(e => e.CtDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CT_Descripcion");
        });

        modelBuilder.Entity<TEstado>(entity =>
        {
            entity.HasKey(e => e.CnIdEstado).HasName("PK__T_Estado__510F27FC773A78A2");

            entity.ToTable("T_Estados");

            entity.Property(e => e.CnIdEstado)
                .ValueGeneratedOnAdd()
                .HasColumnName("CN_Id_estado");
            entity.Property(e => e.CtDescripcion)
                .HasMaxLength(500)
                .HasColumnName("CT_Descripcion");
            entity.Property(e => e.CtEstado)
                .HasMaxLength(100)
                .HasColumnName("CT_Estado");
        });

        modelBuilder.Entity<TFrecuenciaRecordatorio>(entity =>
        {
            entity.HasKey(e => e.CnIdRecordatorio).HasName("PK__T_Frecue__3167F975AD1C5162");

            entity.ToTable("T_Frecuencia_Recordatorio");

            entity.Property(e => e.CnIdRecordatorio).HasColumnName("CN_Id_recordatorio");
            entity.Property(e => e.CbEstado).HasColumnName("CB_Estado");
            entity.Property(e => e.CfFechaFinalEvento)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_final_evento");
            entity.Property(e => e.CfFechaHoraEventoPospuesto)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_evento_pospuesto");
            entity.Property(e => e.CfFechaHoraRecordatorio)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_recordatorio");
            entity.Property(e => e.CfFechaHoraRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_registro");
            entity.Property(e => e.CnFrecuenciaRecordatorio).HasColumnName("CN_Frecuencia_recordatorio");
            entity.Property(e => e.CnIdUsuarioCreador).HasColumnName("CN_Id_usuario_creador");
            entity.Property(e => e.CtTextoRecordatorio)
                .IsUnicode(false)
                .HasColumnName("CT_Texto_recordatorio");

            entity.HasOne(d => d.CnIdUsuarioCreadorNavigation).WithMany(p => p.TFrecuenciaRecordatorios)
                .HasForeignKey(d => d.CnIdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Frecuencia_Recordatorio_T_Usuarios");
        });

        modelBuilder.Entity<TNotificacione>(entity =>
        {
            entity.HasKey(e => e.CnIdNotificacion).HasName("PK__T_Notifi__2A4DC86BF3CD497D");

            entity.ToTable("T_Notificaciones");

            entity.Property(e => e.CnIdNotificacion)
                .ValueGeneratedNever()
                .HasColumnName("CN_Id_notificacion");
            entity.Property(e => e.CfFechaNotificacion)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_notificacion");
            entity.Property(e => e.CfFechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_registro");
            entity.Property(e => e.CnIdRecordatorio).HasColumnName("CN_Id_recordatorio");
            entity.Property(e => e.CnTipoNotificacion).HasColumnName("CN_Tipo_notificacion");
            entity.Property(e => e.CtCorreoOrigen)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CT_Correo_origen");
            entity.Property(e => e.CtTextoNotificacion)
                .IsUnicode(false)
                .HasColumnName("CT_Texto_notificacion");
            entity.Property(e => e.CtTituloNotificacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CT_Titulo_notificacion");

            entity.HasOne(d => d.CnIdRecordatorioNavigation).WithMany(p => p.TNotificaciones)
                .HasForeignKey(d => d.CnIdRecordatorio)
                .HasConstraintName("FK_T_Notificaciones_T_Frecuencia_Recordatorio");
        });

        modelBuilder.Entity<TOficina>(entity =>
        {
            entity.HasKey(e => e.CnCodigoOficina).HasName("PK__T_Oficin__8C4BCAE7F75F846F");

            entity.ToTable("T_Oficinas");

            entity.Property(e => e.CnCodigoOficina)
                .ValueGeneratedNever()
                .HasColumnName("CN_Codigo_oficina");
            entity.Property(e => e.CnOficinaEncargada).HasColumnName("CN_Oficina_encargada");
            entity.Property(e => e.CtNombreOficina)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CT_Nombre_oficina");
        });

        modelBuilder.Entity<TPantalla>(entity =>
        {
            entity.HasKey(e => e.CnIdPantalla).HasName("PK__T_Pantal__9B53558E423C9C80");

            entity.ToTable("T_Pantallas");

            entity.Property(e => e.CnIdPantalla).HasColumnName("CN_Id_pantalla");
            entity.Property(e => e.CtDescripcion)
                .HasColumnType("text")
                .HasColumnName("CT_Descripcion");
        });

        modelBuilder.Entity<TPermiso>(entity =>
        {
            entity.HasKey(e => e.CnIdPermiso).HasName("PK__T_Permis__8E2BCD0F78973544");

            entity.ToTable("T_Permisos");

            entity.Property(e => e.CnIdPermiso)
                .ValueGeneratedNever()
                .HasColumnName("CN_Id_permiso");
            entity.Property(e => e.CfFechaHoraFinPermiso)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_fin_permiso");
            entity.Property(e => e.CfFechaHoraInicioPermiso)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_inicio_permiso");
            entity.Property(e => e.CfFechaHoraRegistro)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_registro");
            entity.Property(e => e.CnIdEstado).HasColumnName("CN_Id_estado");
            entity.Property(e => e.CnUsuarioCreador).HasColumnName("CN_Usuario_creador");
            entity.Property(e => e.CtDescripcionPermiso)
                .HasColumnType("text")
                .HasColumnName("CT_Descripcion_permiso");
            entity.Property(e => e.CtDescripcionRechazo)
                .HasColumnType("text")
                .HasColumnName("CT_Descripcion_rechazo");
            entity.Property(e => e.CtTituloPermiso)
                .HasColumnType("text")
                .HasColumnName("CT_Titulo_permiso");

            entity.HasOne(d => d.CnIdEstadoNavigation).WithMany(p => p.TPermisos)
                .HasForeignKey(d => d.CnIdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Permiso__CN_Es__151B244E");

            entity.HasOne(d => d.CnUsuarioCreadorNavigation).WithMany(p => p.TPermisos)
                .HasForeignKey(d => d.CnUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Permisos_T_Usuarios");
        });

        modelBuilder.Entity<TPrioridade>(entity =>
        {
            entity.HasKey(e => e.CnIdPrioridad).HasName("PK__T_priori__2ABD997461BA968F");

            entity.ToTable("T_Prioridades");

            entity.Property(e => e.CnIdPrioridad).HasColumnName("CN_Id_prioridad");
            entity.Property(e => e.CtDescripcionPrioridad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CT_Descripcion_prioridad");
            entity.Property(e => e.CtNombrePrioridad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CT_Nombre_prioridad");
        });

        modelBuilder.Entity<TRole>(entity =>
        {
            entity.HasKey(e => e.CnIdRol).HasName("PK__T_Roles__2F1CAF9C9A184373");

            entity.ToTable("T_Roles");

            entity.Property(e => e.CnIdRol)
                .ValueGeneratedNever()
                .HasColumnName("CN_Id_rol");
            entity.Property(e => e.CnJerarquia).HasColumnName("CN_Jerarquia");
            entity.Property(e => e.CtNombreRol)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CT_Nombre_rol");
        });

        modelBuilder.Entity<TTarea>(entity =>
        {
            entity.HasKey(e => e.CnIdTarea).HasName("PK__T_Tareas__B4C8A433377D274A");

            entity.ToTable("T_Tareas");

            entity.Property(e => e.CnIdTarea).HasColumnName("CN_Id_tarea");
            entity.Property(e => e.CfFechaAsignacion)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_asignacion");
            entity.Property(e => e.CfFechaFinalizacion)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_finalizacion");
            entity.Property(e => e.CfFechaLimite)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_limite");
            entity.Property(e => e.CnIdComplejidad).HasColumnName("CN_Id_complejidad");
            entity.Property(e => e.CnIdEstado).HasColumnName("CN_Id_estado");
            entity.Property(e => e.CnIdPrioridad).HasColumnName("CN_Id_prioridad");
            entity.Property(e => e.CnNumeroGis)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CN_Numero_GIS");
            entity.Property(e => e.CnTareaOrigen).HasColumnName("CN_Tarea_origen");
            entity.Property(e => e.CnUsuarioAsignado).HasColumnName("CN_Usuario_asignado");
            entity.Property(e => e.CnUsuarioCreador).HasColumnName("CN_Usuario_creador");
            entity.Property(e => e.CtDescripcionEspera)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CT_Descripcion_espera");
            entity.Property(e => e.CtDescripcionTarea)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CT_Descripcion_tarea");
            entity.Property(e => e.CtTituloTarea)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CT_Titulo_tarea");

            entity.HasOne(d => d.CnIdComplejidadNavigation).WithMany(p => p.TTareas)
                .HasForeignKey(d => d.CnIdComplejidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Tareas_T_Complejidades");

            entity.HasOne(d => d.CnIdEstadoNavigation).WithMany(p => p.TTareas)
                .HasForeignKey(d => d.CnIdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Tareas_T_Estados");

            entity.HasOne(d => d.CnIdPrioridadNavigation).WithMany(p => p.TTareas)
                .HasForeignKey(d => d.CnIdPrioridad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Tareas_T_Prioridades");

            entity.HasOne(d => d.CnTareaOrigenNavigation).WithMany(p => p.InverseCnTareaOrigenNavigation)
                .HasForeignKey(d => d.CnTareaOrigen)
                .HasConstraintName("FK_Tarea_TareaOrigen");

            entity.HasOne(d => d.CnUsuarioAsignadoNavigation).WithMany(p => p.TTareaCnUsuarioAsignadoNavigations)
                .HasForeignKey(d => d.CnUsuarioAsignado)
                .HasConstraintName("FK_T_Tareas_T_Usuarios1");

            entity.HasOne(d => d.CnUsuarioCreadorNavigation).WithMany(p => p.TTareaCnUsuarioCreadorNavigations)
                .HasForeignKey(d => d.CnUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Tareas_T_Usuarios");
        });

        modelBuilder.Entity<TTareasIncumplimiento>(entity =>
        {
            entity.HasKey(e => e.CnIdTareaIncumplimiento);

            entity.ToTable("T_Tareas_Incumplimientos");

            entity.Property(e => e.CnIdTareaIncumplimiento)
                .ValueGeneratedNever()
                .HasColumnName("CN_Id_tarea_incumplimiento");
            entity.Property(e => e.CfFechaIncumplimiento)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_incumplimiento");
            entity.Property(e => e.CnIdTarea).HasColumnName("CN_Id_tarea");
            entity.Property(e => e.CtJustificacionIncumplimiento)
                .IsUnicode(false)
                .HasColumnName("CT_Justificacion_incumplimiento");

            entity.HasOne(d => d.CnIdTareaNavigation).WithMany(p => p.TTareasIncumplimientos)
                .HasForeignKey(d => d.CnIdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Tareas_Incumplimientos_T_Tareas");
        });

        modelBuilder.Entity<TTareasJustificacionRechazo>(entity =>
        {
            entity.HasKey(e => e.CnIdTareaRechazo).HasName("PK__T_Tareas__4CCBC4E74B715DA3");

            entity.ToTable("T_Tareas_Justificacion_Rechazo");

            entity.Property(e => e.CnIdTareaRechazo)
                .ValueGeneratedNever()
                .HasColumnName("CN_Id_tarea_rechazo");
            entity.Property(e => e.CfFechaHoraRechazo)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_hora_rechazo");
            entity.Property(e => e.CnIdTarea).HasColumnName("CN_Id_tarea");
            entity.Property(e => e.CtDescripcionRechazo)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("CT_Descripcion_rechazo");

            entity.HasOne(d => d.CnIdTareaNavigation).WithMany(p => p.TTareasJustificacionRechazos)
                .HasForeignKey(d => d.CnIdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Tareas_Justificacion_Rechazo_T_Tareas");
        });

        modelBuilder.Entity<TTareasSeguimiento>(entity =>
        {
            entity.HasKey(e => e.CnIdSeguimiento).HasName("PK__T_Tareas__E99107436F445187");

            entity.ToTable("T_Tareas_Seguimiento");

            entity.Property(e => e.CnIdSeguimiento).HasColumnName("CN_Id_seguimiento");
            entity.Property(e => e.CfFechaSeguimiento)
                .HasColumnType("datetime")
                .HasColumnName("CF_Fecha_seguimiento");
            entity.Property(e => e.CnIdTarea).HasColumnName("CN_Id_tarea");
            entity.Property(e => e.CtComentario)
                .IsUnicode(false)
                .HasColumnName("CT_Comentario");

            entity.HasOne(d => d.CnIdTareaNavigation).WithMany(p => p.TTareasSeguimientos)
                .HasForeignKey(d => d.CnIdTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tareas_Seguimiento_Tareas");
        });

        modelBuilder.Entity<TTiposDocumento>(entity =>
        {
            entity.HasKey(e => e.CnIdTipoDocumento).HasName("PK__T_Tipos___13090706AA1FEBD2");

            entity.ToTable("T_Tipos_documentos");

            entity.HasIndex(e => e.CtNombreTipoDocumento, "UQ__T_Tipos___58671C0FE1B9AC83").IsUnique();

            entity.Property(e => e.CnIdTipoDocumento).HasColumnName("CN_Id_tipo_documento");
            entity.Property(e => e.CtNombreTipoDocumento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CT_Nombre_tipo_documento");
        });

        modelBuilder.Entity<TUsuario>(entity =>
        {
            entity.HasKey(e => e.CnIdUsuario).HasName("PK__T_Usuari__043A50354BA4D783");

            entity.ToTable("T_Usuarios");

            entity.Property(e => e.CnIdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("CN_Id_usuario");
            entity.Property(e => e.CbEstadoUsuario).HasColumnName("CB_Estado_usuario");
            entity.Property(e => e.CfFechaCreacionUsuario).HasColumnName("CF_Fecha_creacion_usuario");
            entity.Property(e => e.CfFechaModificacionUsuario).HasColumnName("CF_Fecha_modificacion_usuario");
            entity.Property(e => e.CfFechaNacimiento).HasColumnName("CF_Fecha_nacimiento");
            entity.Property(e => e.CnIdRol).HasColumnName("CN_Id_rol");
            entity.Property(e => e.CtContrasenna)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CT_Contrasenna");
            entity.Property(e => e.CtCorreoUsuario)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CT_Correo_usuario");
            entity.Property(e => e.CtNombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CT_Nombre_usuario");

            entity.HasOne(d => d.CnIdRolNavigation).WithMany(p => p.TUsuarios)
                .HasForeignKey(d => d.CnIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Usuarios_T_Roles");

            entity.HasMany(d => d.CnCodigoOficinas).WithMany(p => p.CnIdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "TiUsuarioXOficina",
                    r => r.HasOne<TOficina>().WithMany()
                        .HasForeignKey("CnCodigoOficina")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TI_Usuari__CN_Co__403A8C7D"),
                    l => l.HasOne<TUsuario>().WithMany()
                        .HasForeignKey("CnIdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Usuario_Oficina"),
                    j =>
                    {
                        j.HasKey("CnIdUsuario", "CnCodigoOficina").HasName("PK__TI_Usuar__6CFEEC9B0E12B31B");
                        j.ToTable("TI_Usuario_X_Oficina");
                        j.IndexerProperty<int>("CnIdUsuario").HasColumnName("CN_Id_usuario");
                        j.IndexerProperty<int>("CnCodigoOficina").HasColumnName("CN_Codigo_oficina");
                    });
        });

        modelBuilder.Entity<TiNotificacionesXUsuario>(entity =>
        {
            entity.HasKey(e => new { e.CnIdNotificacion, e.CnIdUsuario }).HasName("PK__T_Notifi__6A0E6D680FB34932");

            entity.ToTable("TI_Notificaciones_X_Usuarios");

            entity.Property(e => e.CnIdNotificacion).HasColumnName("CN_Id_notificacion");
            entity.Property(e => e.CnIdUsuario).HasColumnName("CN_Id_usuario");
            entity.Property(e => e.CtCorreoDestino)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("CT_Correo_destino");

            entity.HasOne(d => d.CnIdNotificacionNavigation).WithMany(p => p.TiNotificacionesXUsuarios)
                .HasForeignKey(d => d.CnIdNotificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Notific__CN_Id__10566F31");

            entity.HasOne(d => d.CnIdUsuarioNavigation).WithMany(p => p.TiNotificacionesXUsuarios)
                .HasForeignKey(d => d.CnIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Notificaciones_X_Usuarios_T_Usuarios");
        });

        modelBuilder.Entity<TiRolXPantallaXAccion>(entity =>
        {
            entity.HasKey(e => new { e.CnIdPantalla, e.CnIdAccion, e.CnIdRol });

            entity.ToTable("TI_Rol_X_Pantalla_X_Accion");

            entity.Property(e => e.CnIdPantalla)
                .ValueGeneratedOnAdd()
                .HasColumnName("CN_Id_pantalla");
            entity.Property(e => e.CnIdAccion).HasColumnName("CN_Id_accion");
            entity.Property(e => e.CnIdRol).HasColumnName("CN_Id_rol");

            entity.HasOne(d => d.CnIdAccionNavigation).WithMany(p => p.TiRolXPantallaXAccions)
                .HasForeignKey(d => d.CnIdAccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TI_Rol_X_Pantalla_X_Accion_T_Acciones");

            entity.HasOne(d => d.CnIdPantallaNavigation).WithMany(p => p.TiRolXPantallaXAccions)
                .HasForeignKey(d => d.CnIdPantalla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TI_Rol_X_Pantalla_X_Accion_T_Pantallas");

            entity.HasOne(d => d.CnIdRolNavigation).WithMany(p => p.TiRolXPantallaXAccions)
                .HasForeignKey(d => d.CnIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TI_Rol_X_Pantalla_X_Accion_T_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
