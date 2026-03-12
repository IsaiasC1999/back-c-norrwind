using Microsoft.AspNetCore.Mvc;
using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.repositorio;

namespace ef_nortwith.Controllers;

[ApiController]
[Route("api/Productos")]
public class ProductosControllers : ControllerBase
{
    private readonly ProductsServices productsServices;
    private readonly IRepositorioProdcutos repo;

    public ProductosControllers(ProductsServices productsServices, IRepositorioProdcutos repo)
    {
        this.productsServices = productsServices;
        this.repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProducts([FromQuery] ProductFilter filter)
    {
        var hasFilter = !string.IsNullOrEmpty(filter.Category) || 
                        filter.PriceMin.HasValue || 
                        filter.PriceMax.HasValue || 
                        !string.IsNullOrEmpty(filter.Supplier);

        if (!hasFilter)
        {
            var response = await productsServices.ListProducts();
            return Ok(response);
        }

        var responseFiltered = await productsServices.GetProducts(filter);
        
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
    public async Task<ActionResult> GetProductById(int id)
    {
        var response = await productsServices.GetProductById(id);

        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    [HttpGet("categorias")]
    public async Task<ActionResult> GetAllCategories()
    {
        var response = await productsServices.GetAllCategories();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> PostAddProduct(ProducAddDTO proDTO)
    {
        if (string.IsNullOrWhiteSpace(proDTO.ProductName))
        {
            return BadRequest(new ResponseServices 
            { 
                Success = false, 
                Error = "El nombre del producto es obligatorio",
                Result = null
            });
        }

        if (proDTO.UnitPrice.HasValue && proDTO.UnitPrice < 0)
        {
            return BadRequest(new ResponseServices 
            { 
                Success = false, 
                Error = "El precio no puede ser negativo",
                Result = null
            });
        }

        var map = Mappers.ProductDtoByProducEntity(proDTO);

        var resu = await repo.AddProducts(map);

        if (resu)
        {
            return Ok(new ResponseServices 
            { 
                Success = true, 
                Result = "Producto agregado correctamente",
                Error = ""
            });
        }
        
        return BadRequest(new ResponseServices 
        { 
            Success = false, 
            Error = "Error al agregar el producto. Verifique que la categoría y proveedor existan, y que el ID no esté en uso.",
            Result = null
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutUpdateProduct(int id, ProducAddDTO proDTO)
    {
        if (string.IsNullOrWhiteSpace(proDTO.ProductName))
        {
            return BadRequest(new ResponseServices 
            { 
                Success = false, 
                Error = "El nombre del producto es obligatorio",
                Result = null
            });
        }

        if (proDTO.UnitPrice.HasValue && proDTO.UnitPrice < 0)
        {
            return BadRequest(new ResponseServices 
            { 
                Success = false, 
                Error = "El precio no puede ser negativo",
                Result = null
            });
        }

        proDTO.ProductId = (short)id;
        
        var map = Mappers.ProductDtoByProducEntity(proDTO);
        var resu = await repo.UpdateProduct(map);

        if (resu)
        {
            return Ok(new ResponseServices 
            { 
                Success = true, 
                Result = "Producto actualizado correctamente",
                Error = ""
            });
        }

        return BadRequest(new ResponseServices 
        { 
            Success = false, 
            Error = "Error al actualizar el producto. Verifique que el producto exista.",
            Result = null
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var response = await productsServices.DeleteProduct(id);

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
