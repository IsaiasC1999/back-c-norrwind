using ef_nortwith.dbContext;
using ef_nortwith.interfacez;
using Microsoft.EntityFrameworkCore;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly NorthwindContext db;

    public UsuarioRepository(NorthwindContext db)
    {
        this.db = db;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await db.Users
            .Include(u => u.Employee)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> ValidateCredentials(string username, string password)
    {
        return await db.Users
            .Include(u => u.Employee)
            .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password && u.IsActive);
    }
}
