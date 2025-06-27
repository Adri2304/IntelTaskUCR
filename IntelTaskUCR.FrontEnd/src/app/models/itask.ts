export interface ITask {
    cnIdTarea: number;
    cnTareaOrigen: number | null;
    ctTituloTarea: string;
    ctDescripcionTarea: string;
    ctDescripcionEspera: string | null;
    cnIdComplejidad: number;
    cnIdEstado: number;
    cnIdPrioridad: number;
    cnNumeroGis: string;
    cfFechaAsignacion: Date | null;       // Fecha en formato ISO
    cfFechaLimite: Date;           // Fecha en formato ISO
    cfFechaFinalizacion: Date | null;     // Fecha en formato ISO
    cnUsuarioCreador: number;
    cnUsuarioAsignado: number | null;

    // Agregadas para lógica de permisos
    // esCreador?: boolean;
    // esAsignado?: boolean;
    puedeEditarEstado?: boolean;
    estadosDisponibles?: number[];
}