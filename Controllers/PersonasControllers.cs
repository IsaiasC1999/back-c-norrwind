using Microsoft.AspNetCore.Mvc;

namespace ef_nortwith.Controllers;


public class Persona
{
    public string? Name { get; set; }    
    public int Age { get; set; }

    public string? email { get; set; }
}


[ApiController]
[Route("[controller]")]
public class PersonasControllers : ControllerBase
{

    [HttpPost("/personas/add-person")]
    public string postMethod(Persona per)
    {
            return per.Name;
    }

    

}