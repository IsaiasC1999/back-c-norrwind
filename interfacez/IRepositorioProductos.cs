using ef_nortwith.dbContext;
using ef_nortwith.DTOs;

public interface IRepositorioProdcutos 
{
    public Task <List<Product>> GetAllProducts();
    public Task<Product> GetProductByID(int idProducto);
    public Task<bool>  AddProducts(Product product);
    public  Task<List<Product>> GetAllProductCategory(string category);
    
    public Task<List<Product>> GetAllProductByPrice(int priceInital , int priceFinal);

     public Task<List<Product>> GetAllProductBySuppliers(string proveedor);


    public Task<bool> UpdateProduct(Product product);

    public Task<List<Product>> GetProducts(ProductFilter filter);

    public Task<bool> DeleteProduct(int id);

    public Task<List<Category>> GetAllCategories();
}