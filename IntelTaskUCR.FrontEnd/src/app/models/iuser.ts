export interface IUser {
  cnIdUsuario: number;
  ctNombreUsuario: string;
  ctCorreoUsuario: string;
  cfFechaNacimiento: string;
  ctContrasenna: string | null;
  cbEstadoUsuario: boolean;
  cfFechaCreacionUsuario: string | null;
  cfFechaModificacionUsuario: string | null;
  cnIdRol: number;
}
