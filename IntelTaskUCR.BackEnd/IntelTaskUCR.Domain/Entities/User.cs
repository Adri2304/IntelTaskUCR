using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class User
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

        public User(int idUser, string userName, string mail, DateOnly? birthDate, bool status, int idRole)
        {
            CnIdUsuario = idUser;
            CtNombreUsuario = userName;
            CtCorreoUsuario = mail;
            CfFechaNacimiento = birthDate;
            CbEstadoUsuario = status;
            CnIdRol = idRole;
        }

        public User(int idUser, string userName, string mail, DateOnly birthDate, string password, int idRole)
        {
            CnIdUsuario = idUser;
            CtNombreUsuario = userName;
            CtCorreoUsuario = mail;
            CfFechaNacimiento = birthDate;
            CtContrasenna = password;
            CbEstadoUsuario = true;
            CnIdRol = idRole;
            CfFechaCreacionUsuario = DateOnly.FromDateTime(DateTime.Now);
            CfFechaModificacionUsuario = null;
        }

        //public User(int idUser, string userName, string mail, DateOnly birthDate)
        //{
        //    CnIdUsuario = idUser;
        //    CtNombreUsuario = userName;
        //    CtCorreoUsuario = mail;
        //    CfFechaNacimiento = birthDate;
        //}

        //public Dictionary<string, object> GetAtrubutesDictionary()
        //{
        //    var diccionario = new Dictionary<string, object>();

        //    if (CnIdUsuario != 0) diccionario["CnIdUsuario"] = CnIdUsuario;
        //    if (!string.IsNullOrEmpty(CtNombreUsuario)) diccionario["CtNombreUsuario"] = CtNombreUsuario;
        //    if (!string.IsNullOrEmpty(CtCorreoUsuario)) diccionario["CtCorreoUsuario"] = CtCorreoUsuario;
        //    if (CfFechaNacimiento.HasValue) diccionario["CfFechaNacimiento"] = CfFechaNacimiento.Value;
        //    if (!string.IsNullOrEmpty(CtContrasenna)) diccionario["CtContrasenna"] = CtContrasenna;
        //    diccionario["CbEstadoUsuario"] = CbEstadoUsuario;
        //    if (CfFechaCreacionUsuario.HasValue) diccionario["CfFechaCreacionUsuario"] = CfFechaCreacionUsuario.Value;
        //    if (CfFechaModificacionUsuario.HasValue) diccionario["CfFechaModificacionUsuario"] = CfFechaModificacionUsuario.Value;
        //    if (CnIdRol != 0) diccionario["CnIdRol"] = CnIdRol;

        //    return diccionario;
        //}
    }
}
