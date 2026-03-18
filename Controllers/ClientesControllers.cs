using ef_nortwith.dbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ef_nortwith.Controllers;




[ApiController]
[Route("api/clientes")]
public class ClientesControllers : ControllerBase 
{
    private readonly NorthwindContext db;

    public ClientesControllers(NorthwindContext db)
    {
        this.db = db;
    }

    [Authorize]
    [HttpGet("list")]
    public async Task<ActionResult<ICollection<Customer>>> ObtenerClientes(){

        return await db.Customers.ToListAsync();

    }

     

}
