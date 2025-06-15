namespace IntelTaskUCR.API.DTOs.Tasks
{
    public class ChangeStatusDTO
    {
        public int CnIdTarea { get; set; }
        public int? CnIdUsuario { get; set; }
        public byte CnIdEstado { get; set; }
        public object? AdditionalData { get; set; }
    }
}
