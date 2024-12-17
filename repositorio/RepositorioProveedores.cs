using ef_nortwith.dbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RepositorioProveedores
{
    private readonly NorthwindContext db;

    public RepositorioProveedores(NorthwindContext db)
    {
        this.db = db;
    }

    public async Task<List<Supplier>> GetSupplierList()
    {
        return await db.Suppliers.ToListAsync();
    }


    public async Task<bool> AddSupplier(Supplier supplier)
    {
        db.Suppliers.Add(supplier);
        await db.SaveChangesAsync();
        return true;
        
    }

    public async Task<bool> UpdateSupplier(Supplier supplier)
    {
        var resu = await db.Suppliers.AnyAsync(s => s.SupplierId == supplier.SupplierId);

        if(resu)
        {
            db.Update(supplier);
            await db.SaveChangesAsync();   
            return true;
        }
        return false;
    }

}