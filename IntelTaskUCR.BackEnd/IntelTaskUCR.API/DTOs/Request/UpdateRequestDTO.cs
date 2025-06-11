namespace IntelTaskUCR.API.DTOs.Request
{
    public class UpdateRequestDTO
    {
        public required string CtTituloPermiso { get; set; }
        public string CtDescripcionPermiso { get; set; } = null!;
        public DateTime CfFechaHoraInicioPermiso { get; set; }

        public DateTime CfFechaHoraFinPermiso { get; set; }

        public Dictionary<string, object?> GetAtributesDictionary()
        {
            var result = new Dictionary<string, object?>();

            foreach (var property in typeof(UpdateRequestDTO).GetProperties())
            {
                result.Add(property.Name, property.GetValue(this));
            }
            return result;
        }
    }
}
