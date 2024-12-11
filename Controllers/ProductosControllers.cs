using Microsoft.AspNetCore.Mvc;
using ef_nortwith.dbContext;
using Microsoft.EntityFrameworkCore;


namespace ef_nortwith.Controllers;


[ApiController]
[Route("[controllers]")]
public class ProductosControllers : ControllerBase
{
    private readonly ProductsServices productsServices;

    public ProductosControllers(ProductsServices productsServices)
    {
        this.productsServices = productsServices;
    }

    [HttpGet("/productos/list")]
    public async Task<ActionResult> GetAllProducts()
    {

        var response = await productsServices.ListProducts();

        if (response.Success)
        {

            return Ok(response);

        }
        else
        {
            return NotFound(response);
        }
    }


    // [HttpGet("/productos/{idProducto}")]
    // public async Task<ActionResult> GetProductById(int idProducto)
    // {
    //       var response = await productsServices.getProductById(idProducto);  

    //       if(response.Success){
    //         return Ok(response.Result);
    //       }else{
    //         return BadRequest(response);
    //       }
    // }

    // [HttpGet("/productos/list/categorias")]
    // public async Task<ActionResult<List<Category>>> GetAllCategorias(){

    //     return await 

    // }

    [HttpGet("/productos/filter-category/")]
    public async Task<ActionResult> GetAllProductCategory(string category)
    {
        var response = await productsServices.GetAllProductbyCategory(category);
        if (response.Success)
        {

            return Ok(response);

        }
        else
        {
            return NotFound(response);
        }
    }


    [HttpGet("/productos/filter-price/")]
    public async Task<ActionResult> GetAllProductByPrice(int priceInital , int priceFinal)
    {
        var response = await productsServices.GetAllProductByPrice(priceInital,priceFinal);
        if (response.Success)
        {

            return Ok(response);

        }
        else
        {
            return NotFound(response);
        }

    }

    [HttpGet("/productos/filter-supliers/")]
    public async Task<ActionResult> GetProductBySupliers(string proveerdor)
    {

        var response = await productsServices.GetAllProductSupliers(proveerdor);

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