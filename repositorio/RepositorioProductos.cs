

using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
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

    public async Task<List<Product>> GetProducts(ProductFilter filter)
    {
        var query = db.Products.Include(p => p.Supplier).Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrEmpty(filter.Category))
        {
            query = query.Where(p => p.Category != null && p.Category.CategoryName == filter.Category);
        }

        if (filter.PriceMin.HasValue)
        {
            query = query.Where(p => p.UnitPrice >= filter.PriceMin.Value);
        }

        if (filter.PriceMax.HasValue)
        {
            query = query.Where(p => p.UnitPrice <= filter.PriceMax.Value);
        }

        if (!string.IsNullOrEmpty(filter.Supplier))
        {
            query = query.Where(p => p.Supplier != null && p.Supplier.CompanyName == filter.Supplier);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await db.Products.FindAsync(id);
        
        if (product == null)
        {
            return false;
        }

        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await db.Categories.ToListAsync();
    }


    public async Task<bool> AddProducts(Product pro)
    {   
           var verifyIdProduct = await db.Products.AnyAsync(p => p.ProductId == pro.ProductId); 
           var verifyIdSupplier = await db.Suppliers.AnyAsync(p => p.SupplierId == pro.SupplierId); 
           var verifyIdCategory = await db.Categories.AnyAsync(p => p.CategoryId == pro.CategoryId);

           

           if(verifyIdCategory == false ||  verifyIdSupplier == false || verifyIdProduct == true ) 
           {

               return false; 

           } 

           db.Products.Add(pro);
           await db.SaveChangesAsync();
           return true;   
           
    }

    public async Task<bool> UpdateProduct(Product prod)
    {   
        var producto = await db.Products
                     .FindAsync(prod.ProductId); 

        if(producto == null)
        {
            return false;
        }

        db.Entry(producto).CurrentValues.SetValues(prod); 
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