using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;
using ef_nortwith.dbContext;
using Microsoft.IdentityModel.Tokens;

namespace ef_nortwith.services;

public class UsuarioService
{
    private readonly IUsuarioRepository usuarioRepository;
    private const string Key = "kn5ln23nm4jn5kj43n1kn43325nkj6543";

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

        var token = CreateToken(user.Username, user.Role);

        var userDTO = new UserDTO
        {
            Id = user.Id,
            EmployeeId = user.EmployeeId,
            Username = user.Username,
            Role = user.Role,
            IsActive = user.IsActive,
            Token = token
        };

        return new ResponseServices
        {
            Success = true,
            Result = userDTO,
            Error = ""
        };
    }

    private string CreateToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var byteKey = Encoding.UTF8.GetBytes(Key);
        var tokenDes = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(byteKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDes);
        return tokenHandler.WriteToken(token);
    }
}
