

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

  

    public async Task<List<Product>> GetAllProducts()
    {
           return await db.Products.Include(p => p.Supplier).Include(p => p.Category).ToListAsync();
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


    public async Task<bool> AddProducts(Product pro)
    {   
           var verifyIdProduct = await db.Products.AnyAsync(p => p.ProductId == pro.ProductId); 
           var verifyIdSupplier = await db.Products.AnyAsync(p => p.SupplierId == pro.SupplierId); 
           var verifyIdCategory = await db.Products.AnyAsync(p => p.CategoryId == pro.CategoryId);

           

           if(verifyIdCategory == false ||  verifyIdSupplier == false || verifyIdProduct == true ) 
           {

               return false; 

           } 

           db.Products.Add(pro);
           await db.SaveChangesAsync();
           return true;   
           
    } 

    // public async Task<List<Product>> GetListProductBySupplier(int idProveedor)
    // {
    //       return await db.Products.Where(p => p.SupplierId == idProveedor).ToListAsync();      
    // }


    // public async Task<bool> UpdateProduc(Product product){

        
               
    // } 
}