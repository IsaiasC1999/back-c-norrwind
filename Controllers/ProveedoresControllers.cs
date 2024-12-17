using Microsoft.AspNetCore.Mvc;

namespace ef_nortwith.Controllers;


[ApiController]
[Route("[controller]")]
public class ProveedoresControllers : ControllerBase
{
    private readonly RepositorioProveedores repo;

    public ProveedoresControllers(RepositorioProveedores repo)
     {
        this.repo = repo;
    }

    [HttpGet("/proveedores/to-list")]
    public async  Task<ActionResult<List<SuppliersDTO>>> GetListProveedores()
    {

        var listado = await repo.GetSupplierList();

        var listadoDto = Mappers.SuppliersEntityToSuppliersDTO(listado);

        return Ok(listadoDto);    
    }

    [HttpPost("/proveedor/add")]
    public async Task<ActionResult> AddProveedor(SuppliersDTO suppliersDTO)
    {
        var s = Mappers.SupplierDTOtoSupplierEntity(suppliersDTO);  
        var resu =  await repo.AddSupplier(s);
        if (resu)
        {
            return Ok("Se agrego correntamente en provvedor");
        }    

        return BadRequest("No se pudo agregar");
    }


    [HttpPut("/proveedor")]
    public async Task<ActionResult> UpdateProveedor(SuppliersDTO suppliersDTO)
    {
        var s = Mappers.SupplierDTOtoSupplierEntity(suppliersDTO);  
        var resu =  await repo.UpdateSupplier(s);
        if (resu)
        {
            return Ok("se modifico correctamente");
        }    

        return BadRequest("el id de proveedor no existe");
    }


}