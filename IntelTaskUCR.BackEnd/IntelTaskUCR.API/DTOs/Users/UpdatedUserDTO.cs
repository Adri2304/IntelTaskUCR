namespace IntelTaskUCR.API.DTOs.Users
{
    public class UpdatedUserDTO
    {
        public string CtNombreUsuario { get; set; } = null!;
        public string CtCorreoUsuario { get; set; } = null!;
        public DateOnly CfFechaNacimiento { get; set; }

        public Dictionary<string, object> GetAtrubutesDictionary()
        {
            return new Dictionary<string, object>
            {
                { "CtNombreUsuario", this.CtNombreUsuario },
                { "CtCorreoUsuario", this.CtCorreoUsuario },
                { "CfFechaNacimiento", this.CfFechaNacimiento }
            };
        }
    }
}
