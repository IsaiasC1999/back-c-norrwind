using ef_nortwith.DTOs;
using ef_nortwith.services;
using Microsoft.AspNetCore.Mvc;

namespace ef_nortwith.Controllers;

[ApiController]
[Route("api/Clientes")]
public class ClientesControllers : ControllerBase
{
    private readonly ClientesServices clientesServices;

    public ClientesControllers(ClientesServices clientesServices)
    {
        this.clientesServices = clientesServices;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllClientes([FromQuery] CustomerFilter filter)
    {
        var result = await clientesServices.GetAll(filter);

        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetClienteById(string id)
    {
        var result = await clientesServices.GetById(id);

        if (!result.Success)
        {
            return NotFound(result.Error);
        }

        return Ok(result);
    }

    [HttpGet("{id}/ordenes")]
    public async Task<ActionResult> GetClienteWithOrders(string id)
    {
        var result = await clientesServices.GetByIdWithOrders(id);

        if (!result.Success)
        {
            return NotFound(result.Error);
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> PostAddCliente(CustomerAddDTO customerDTO)
    {
        if (string.IsNullOrEmpty(customerDTO.CustomerId) || string.IsNullOrEmpty(customerDTO.CompanyName))
        {
            return BadRequest("El ID y el nombre de la empresa son obligatorios");
        }

        var result = await clientesServices.Add(customerDTO);

        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetClienteById), new { id = customerDTO.CustomerId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUpdateCliente(string id, CustomerAddDTO customerDTO)
    {
        var result = await clientesServices.Update(id, customerDTO);

        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCliente(string id)
    {
        var result = await clientesServices.Delete(id);

        if (!result.Success)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }
}
