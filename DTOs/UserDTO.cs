namespace ef_nortwith.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    public short EmployeeId { get; set; }

    public string Username { get; set; } = "";

    public string Role { get; set; } = "usuario";

    public bool IsActive { get; set; }

    public string? Token { get; set; }
}
