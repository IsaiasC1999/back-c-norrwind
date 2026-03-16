using ef_nortwith.DTOs;
using ef_nortwith.interfacez;
using ef_nortwith.dbContext;

public class UsuarioService
{
    private readonly IUsuarioRepository usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        this.usuarioRepository = usuarioRepository;
    }

    public async Task<ResponseServices> Login(UserLoginDTO loginDTO)
    {
        if (string.IsNullOrWhiteSpace(loginDTO.Username) || string.IsNullOrWhiteSpace(loginDTO.Password))
        {
            return new ResponseServices
            {
                Success = false,
                Error = "Usuario y contraseña son requeridos",
                Result = null
            };
        }

        var user = await usuarioRepository.ValidateCredentials(loginDTO.Username, loginDTO.Password);

        if (user == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "Usuario o contraseña incorrectos",
                Result = null
            };
        }

        var userDTO = new UserDTO
        {
            Id = user.Id,
            EmployeeId = user.EmployeeId,
            Username = user.Username,
            Role = user.Role,
            IsActive = user.IsActive
        };

        return new ResponseServices
        {
            Success = true,
            Result = userDTO,
            Error = ""
        };
    }
}
