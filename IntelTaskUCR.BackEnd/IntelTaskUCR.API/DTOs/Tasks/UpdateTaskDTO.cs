namespace IntelTaskUCR.API.DTOs.Tasks
{
    public class UpdateTaskDTO
    {
        public string? CtTituloTarea { get; set; }
        public string CtDescripcionTarea { get; set; } = null!;
        public byte CnIdComplejidad { get; set; }
        public byte CnIdPrioridad { get; set; }
        public string? CnNumeroGis { get; set; }
        public DateTime CfFechaLimite { get; set; }

        public Dictionary<string, object?> GetAtributesDictionary()
        {
            var result = new Dictionary<string, object?>();

            foreach (var prop in typeof(UpdateTaskDTO).GetProperties())
            {
                result[prop.Name] = prop.GetValue(this);
            }
            return result;
        }
    }
}
