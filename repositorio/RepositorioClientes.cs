using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;
using Microsoft.EntityFrameworkCore;

namespace ef_nortwith.repositorio;

public class RepositorioClientes : IRepositorioClientes
{
    private readonly NorthwindContext db;

    public RepositorioClientes(NorthwindContext db)
    {
        this.db = db;
    }

    public async Task<List<Customer>> GetAll(CustomerFilter filter)
    {
        var query = db.Customers.AsQueryable();

        if (filter == null)
        {
            return await query.ToListAsync();
        }

        if (!string.IsNullOrEmpty(filter.CompanyName))
        {
            query = query.Where(c => c.CompanyName != null && c.CompanyName.Contains(filter.CompanyName));
        }

        if (!string.IsNullOrEmpty(filter.ContactName))
        {
            query = query.Where(c => c.ContactName != null && c.ContactName.Contains(filter.ContactName));
        }

        if (!string.IsNullOrEmpty(filter.City))
        {
            query = query.Where(c => c.City != null && c.City.Contains(filter.City));
        }

        if (!string.IsNullOrEmpty(filter.Country))
        {
            query = query.Where(c => c.Country != null && c.Country.Contains(filter.Country));
        }

        if (!string.IsNullOrEmpty(filter.Region))
        {
            query = query.Where(c => c.Region != null && c.Region.Contains(filter.Region));
        }

        if (!string.IsNullOrEmpty(filter.PostalCode))
        {
            query = query.Where(c => c.PostalCode != null && c.PostalCode.Contains(filter.PostalCode));
        }

        if (filter.Offset.HasValue)
        {
            query = query.Skip(filter.Offset.Value);
        }

        if (filter.Limit.HasValue)
        {
            query = query.Take(filter.Limit.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<int> GetTotalCount(CustomerFilter filter)
    {
        var query = db.Customers.AsQueryable();

        if (filter == null)
        {
            return await query.CountAsync();
        }

        if (!string.IsNullOrEmpty(filter.CompanyName))
        {
            query = query.Where(c => c.CompanyName != null && c.CompanyName.Contains(filter.CompanyName));
        }

        if (!string.IsNullOrEmpty(filter.ContactName))
        {
            query = query.Where(c => c.ContactName != null && c.ContactName.Contains(filter.ContactName));
        }

        if (!string.IsNullOrEmpty(filter.City))
        {
            query = query.Where(c => c.City != null && c.City.Contains(filter.City));
        }

        if (!string.IsNullOrEmpty(filter.Country))
        {
            query = query.Where(c => c.Country != null && c.Country.Contains(filter.Country));
        }

        if (!string.IsNullOrEmpty(filter.Region))
        {
            query = query.Where(c => c.Region != null && c.Region.Contains(filter.Region));
        }

        if (!string.IsNullOrEmpty(filter.PostalCode))
        {
            query = query.Where(c => c.PostalCode != null && c.PostalCode.Contains(filter.PostalCode));
        }

        return await query.CountAsync();
    }

    public async Task<Customer?> GetById(string id)
    {
        return await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public async Task<Customer?> GetByIdWithOrders(string id)
    {
        return await db.Customers
            .Include(c => c.Orders)
                .ThenInclude(o => o.Employee)
            .Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == id);
    }

    public async Task<bool> Add(Customer customer)
    {
        try
        {
            var exists = await db.Customers.AnyAsync(c => c.CustomerId == customer.CustomerId);
            if (exists)
            {
                return false;
            }

            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Update(string id, Customer customer)
    {
        var existing = await db.Customers.FindAsync(id);
        if (existing == null)
        {
            return false;
        }

        db.Entry(existing).CurrentValues.SetValues(customer);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(string id)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer == null)
        {
            return false;
        }

        db.Customers.Remove(customer);
        await db.SaveChangesAsync();
        return true;
    }
}
