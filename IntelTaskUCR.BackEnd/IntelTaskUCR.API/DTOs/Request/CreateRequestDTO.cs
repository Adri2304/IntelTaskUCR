namespace IntelTaskUCR.API.DTOs.Request
{
    public class CreateRequestDTO
    {

        public string? CtTituloPermiso { get; set; }

        public string CtDescripcionPermiso { get; set; } = null!;

        public byte CnIdEstado { get; set; }

        public string? CtDescripcionRechazo { get; set; }

        public DateTime CfFechaHoraRegistro { get; set; }

        public DateTime CfFechaHoraInicioPermiso { get; set; }

        public DateTime CfFechaHoraFinPermiso { get; set; }

        public int CnUsuarioCreador { get; set; }

        public Dictionary<string, object?> GetAtributesDictionary()
        {
            var result = new Dictionary<string, object?>();

            foreach (var property in typeof(CreateRequestDTO).GetProperties())
            {
                result.Add(property.Name, property.GetValue(this));
            }
            return result;
        }
    }
}
