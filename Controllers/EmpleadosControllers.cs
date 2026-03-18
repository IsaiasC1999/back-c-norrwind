using Microsoft.AspNetCore.Mvc;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;
using ef_nortwith.services;

namespace ef_nortwith.Controllers;

[ApiController]
[Route("api/Empleados")]
public class EmpleadosControllers : ControllerBase
{
    private readonly EmpleadosServices empleadosServices;

    public EmpleadosControllers(EmpleadosServices empleadosServices)
    {
        this.empleadosServices = empleadosServices;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllEmployees([FromQuery] EmployeeFilter? filter)
    {
        if (filter == null || (string.IsNullOrEmpty(filter.Nombre) && !filter.FechaIngreso.HasValue))
        {
            var response = await empleadosServices.GetAllEmployees();
            return Ok(response);
        }

        var responseFiltered = await empleadosServices.GetEmployees(filter);

        if (responseFiltered.Success)
        {
            return Ok(responseFiltered);
        }
        else
        {
            return NotFound(responseFiltered);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetEmployeeById(short id)
    {
        var response = await empleadosServices.GetEmployeeById(id);

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
    public async Task<ActionResult> CreateEmployee(EmployeeAddDTO employeeDTO)
    {
        if (string.IsNullOrWhiteSpace(employeeDTO.LastName))
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El apellido es obligatorio",
                Result = null
            });
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.FirstName))
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El nombre es obligatorio",
                Result = null
            });
        }

        var response = await empleadosServices.CreateEmployee(employeeDTO);

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
    public async Task<ActionResult> UpdateEmployee(short id, EmployeeAddDTO employeeDTO)
    {
        if (string.IsNullOrWhiteSpace(employeeDTO.LastName))
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El apellido es obligatorio",
                Result = null
            });
        }

        if (string.IsNullOrWhiteSpace(employeeDTO.FirstName))
        {
            return BadRequest(new ResponseServices
            {
                Success = false,
                Error = "El nombre es obligatorio",
                Result = null
            });
        }

        var response = await empleadosServices.UpdateEmployee(id, employeeDTO);

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
    public async Task<ActionResult> DeleteEmployee(short id)
    {
        var response = await empleadosServices.DeleteEmployee(id);

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