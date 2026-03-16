using ef_nortwith.dbContext;

namespace ef_nortwith.interfacez;

public interface IUsuarioRepository
{
    Task<User?> GetByUsername(string username);
    Task<User?> ValidateCredentials(string username, string password);
}
