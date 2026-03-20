using Microsoft.AspNetCore.Mvc;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;

namespace ef_nortwith.Controllers;

[ApiController]
[Route("api/Ordenes")]
public class OrdenesControllers : ControllerBase
{
    private readonly OrdenesServices ordenesServices;
    private readonly IRepositorioOrdenes repo;

    public OrdenesControllers(OrdenesServices ordenesServices, IRepositorioOrdenes repo)
    {
        this.ordenesServices = ordenesServices;
        this.repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllOrders(
        [FromQuery] OrderFilter? filter,
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 15)
    {
        if (offset < 0)
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El offset no puede ser negativo",
                Result = null
            });
        }

        if (limit < 1 || limit > 100)
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El límite debe estar entre 1 y 100",
                Result = null
            });
        }

        var response = await ordenesServices.GetOrdersPaginated(filter, offset, limit);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        var response = await ordenesServices.GetOrderById(id);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpGet("{id}/detalle")]
    public async Task<ActionResult> GetOrderDetail(int id)
    {
        var response = await ordenesServices.GetOrderDetail(id);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderAddDTO orderDTO)
    {
        if (string.IsNullOrWhiteSpace(orderDTO.CustomerId))
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El cliente es obligatorio",
                Result = null
            });
        }

        if (orderDTO.EmployeeId <= 0)
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El empleado es obligatorio",
                Result = null
            });
        }

        var response = await ordenesServices.CreateOrder(orderDTO);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, OrderAddDTO orderDTO)
    {
        var response = await ordenesServices.UpdateOrder(id, orderDTO);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var response = await ordenesServices.DeleteOrder(id);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }
}
