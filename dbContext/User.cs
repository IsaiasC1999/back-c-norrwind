using System;

namespace ef_nortwith.dbContext;

public class User
{
    public int Id { get; set; }

    public short EmployeeId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = "usuario";

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual Employee? Employee { get; set; }
}
