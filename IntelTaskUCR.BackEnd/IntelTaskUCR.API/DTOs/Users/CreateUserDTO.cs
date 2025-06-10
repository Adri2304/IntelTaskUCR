namespace IntelTaskUCR.API.DTOs.Users
{
    public class CreateUserDTO
    {   
        public int IdUser { get; set; }
        public string UserName { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }
    }
}
