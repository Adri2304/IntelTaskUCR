namespace IntelTaskUCR.API.DTOs.Tasks
{
    public class FilterTaskDTO
    {
        public int[] States { get; set; } = [];
        public bool Descending { get; set; }
    }
}
