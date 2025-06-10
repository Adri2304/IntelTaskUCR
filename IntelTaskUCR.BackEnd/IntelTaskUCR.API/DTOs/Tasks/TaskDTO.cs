using System.Reflection;

namespace IntelTaskUCR.API.DTOs.Tasks
{
    public class TaskDTO
    {
        public int? CnTareaOrigen { get; set; }
        public string? CtTituloTarea { get; set; }
        public string CtDescripcionTarea { get; set; } = null!;
        public string? CtDescripcionEspera { get; set; }
        public byte CnIdComplejidad { get; set; }
        public byte CnIdEstado { get; set; }
        public byte CnIdPrioridad { get; set; }
        public string? CnNumeroGis { get; set; }
        public DateTime CfFechaAsignacion { get; set; }
        public DateTime CfFechaLimite { get; set; }
        public DateTime CfFechaFinalizacion { get; set; }
        public int CnUsuarioCreador { get; set; }
        public int? CnUsuarioAsignado { get; set; }

        public Dictionary<string, object?> GetAtributesDictionary()
        {
            var result = new Dictionary<string, object?>();

            foreach (var prop in typeof(TaskDTO).GetProperties())
            {
                result[prop.Name] = prop.GetValue(this);
            }
            return result;
        }
    }
}
