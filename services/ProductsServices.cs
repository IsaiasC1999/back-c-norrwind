using ef_nortwith.dbContext;
using ef_nortwith.repositorio;

public class ProductsServices
{
    private readonly IRepositorioProdcutos repositorioProductos;

    public ProductsServices(IRepositorioProdcutos repositorioProductos)
    {
        this.repositorioProductos = repositorioProductos;
    }

    public async Task<ResponseServices> ListProducts()
    {

        var listProducts = await repositorioProductos.GetAllProducts();

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.ProducEntitiesToProductDTOs(listProducts),
            Error = ""
        };

    }

    public async Task<ResponseServices> GetProductById(int idProducto)
    {

        var produc = await repositorioProductos.GetProductByID(idProducto);

        if (produc != null)
        {

            return new ResponseServices
            {
                Success = true,
                Result = Mappers.ProducEntityToProductoDTO(produc),
                Error = ""
            };

        }
        else
        {

            return new ResponseServices
            {
                Success = false,
                Error = "El producto con el id que ingreso no existe"
            };
        }
    }

    public async Task<ResponseServices> GetAllProductbyCategory(string category)
    {


        var product = await repositorioProductos.GetAllProductCategory(category);

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.ProducEntitiesToProductDTOs(product),
            Error = ""
        };

    }


    public async Task<ResponseServices> GetAllProductByPrice(int priceInital, int priceFinal)
    {


        var product = await repositorioProductos.GetAllProductByPrice(priceInital, priceFinal);

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.ProducEntitiesToProductDTOs(product),
            Error = ""
        };

    }

    public async Task<ResponseServices> GetAllProductSupliers(string proveerdor)
    {


        var product = await repositorioProductos.GetAllProductBySuppliers(proveerdor);

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.ProducEntitiesToProductDTOs(product),
            Error = ""
        };

    }

}