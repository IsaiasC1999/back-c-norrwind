using Microsoft.AspNetCore.Mvc;
using ef_nortwith.DTOs;

namespace ef_nortwith.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthControllers : ControllerBase
{
    private readonly UsuarioService usuarioService;

    public AuthControllers(UsuarioService usuarioService)
    {
        this.usuarioService = usuarioService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDTO loginDTO)
    {
        var response = await usuarioService.Login(loginDTO);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return Unauthorized(response);
        }
    }
}
