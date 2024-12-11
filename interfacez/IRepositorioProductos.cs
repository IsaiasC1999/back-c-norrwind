using ef_nortwith.dbContext;

public interface IRepositorioProdcutos 
{
    public Task <List<Product>> GetAllProducts();
    public Task<Product> GetProductByID(int idProducto);
    public Task<bool>  AddProducts(Product product);
    public  Task<List<Product>> GetAllProductCategory(string category);
    
    public Task<List<Product>> GetAllProductByPrice(int priceInital , int priceFinal);

     public Task<List<Product>> GetAllProductBySuppliers(string proveedor);
}