using ef_nortwith.dbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ef_nortwith.Controllers;


[ApiController]
[Route("[controller]")]
public class OrdenesControllers : ControllerBase
{
    private readonly NorthwindContext db;
    private readonly RepositorioOrdenes repositorioOrdenes;

    public OrdenesControllers(NorthwindContext db, RepositorioOrdenes repositorioOrdenes) 
    {
        this.db = db;
        this.repositorioOrdenes = repositorioOrdenes;
    }

    [HttpGet("/order/to-list")]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        var listaOrder = await db.Orders.ToArrayAsync();
        return Ok(listaOrder);

    }

    [HttpGet("/order/{cliente}")]
    public async Task<ActionResult<List<Order>>> getOrderbyId(string cliente)
    {
        var resu = await db.Customers.AnyAsync(cl => cl.CustomerId == cliente);

        if (!resu)
        {
            return NotFound("No existe el id");
        }

        var listadoOrderbyId = await db.Orders.Where(or => or.CustomerId == cliente).ToArrayAsync();

        return Ok(listadoOrderbyId);
    }



    [HttpGet("/order-limit")]
    public async Task<ActionResult<List<OrderListDTO>>> GetAllOrdersDos()
    {

        var resu =  await repositorioOrdenes.GetAllOrders();
         
        var resuFinal = Mappers.OrderEntitiesToOrderListDTO(resu); 

        return Ok(resuFinal);

    }


    [HttpGet("/order/{idOrder}/detalle")]
    public async Task<ActionResult<OrderDetailsDTO>> GetOrderDetails(int idOrder){

            var resu =  await repositorioOrdenes.GetOrderDetail(idOrder);

            var orderDeteailDto = Mappers.OrderDetailEntityToOrderDetailsDTO(resu);

            return orderDeteailDto;

    }

    [HttpGet("/order/filter-date")]
    public async Task<ActionResult<List<OrderDetailsDTO>>> GetOrderDetailsDate([FromQuery]DateTime startDate , [FromQuery] DateTime endDate)
    {

        var resu = await repositorioOrdenes.GetAllOrdersDate(startDate , endDate);
        
        return Ok(resu);
    }

}




