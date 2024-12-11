using ef_nortwith.dbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ef_nortwith.Controllers;




[ApiController]
[Route("[controller]")]
public class ClientesControllers : ControllerBase 
{
    private readonly NorthwindContext db;

    public ClientesControllers(NorthwindContext db)
    {
        this.db = db;
    } 

    [HttpGet("/clientes/list")]
    public async Task<ActionResult<ICollection<Customer>>> ObtenerClientes(){

        return await db.Customers.ToListAsync();

    }

     

}
