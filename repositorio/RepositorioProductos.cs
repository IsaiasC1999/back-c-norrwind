

using ef_nortwith.dbContext;
using Microsoft.EntityFrameworkCore;

namespace ef_nortwith.repositorio;

public class RepositorioProductos : IRepositorioProdcutos
{
    private readonly NorthwindContext db;

    public RepositorioProductos(NorthwindContext db)
    {
        this.db = db;
    }

    public Task<bool> AddProducts(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> GetAllProducts()
    {
           return await db.Products.Include(p => p.Category).Include(p => p.Category).ToListAsync();
    }
    
    public async Task<Product> GetProductByID(int idProducto)
    {
        var resu = await db.Products.FirstOrDefaultAsync(ele => ele.ProductId == idProducto); 
        
        return resu;
    }

    public async Task<List<Product>> GetAllProductCategory(string category)
    {
            return await db.Products.Include(p => p.Category).Where(p => p.Category.CategoryName == category).ToListAsync();
    } 



    public async Task<List<Product>> GetAllProductByPrice(int priceInital , int priceFinal) 
    {
        var result  = await db.Products.Include(p => p.Category).Where(p => p.UnitPrice >= priceInital && p.UnitPrice <= priceFinal ).ToListAsync();

        return result;

    }

    public async Task<List<Product>> GetAllProductBySuppliers(string proveedor)
    {
         var result = await db.Products.Include(p=> p.Category).Where(p=> p.Supplier.CompanyName == proveedor).ToListAsync();
   
        return result;
    }

    // public async Task<bool> UpdateProduc(Product product){

        
               
    // } 
}