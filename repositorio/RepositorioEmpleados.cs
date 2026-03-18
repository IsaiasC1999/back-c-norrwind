using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;
using Microsoft.EntityFrameworkCore;

public class RepositorioEmpleados : IRepositorioEmpleados
{
    private readonly NorthwindContext db;

    public RepositorioEmpleados(NorthwindContext db)
    {
        this.db = db;
    }

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await db.Employees
            .Include(e => e.Users)
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<List<Employee>> GetEmployees(EmployeeFilter filter)
    {
        var query = db.Employees
            .Include(e => e.Users)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.Nombre))
        {
            var nombreLower = filter.Nombre.ToLower();
            query = query.Where(e => 
                e.FirstName!.ToLower().Contains(nombreLower) || 
                e.LastName!.ToLower().Contains(nombreLower));
        }

        if (filter.FechaIngreso.HasValue)
        {
            query = query.Where(e => e.HireDate == filter.FechaIngreso.Value);
        }

        return await query
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeById(short id)
    {
        return await db.Employees
            .Include(e => e.Users)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        db.Employees.Add(employee);
        await db.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        var existing = await db.Employees.FindAsync(employee.EmployeeId);
        if (existing == null)
        {
            throw new Exception("Empleado no encontrado");
        }
        
        db.Entry(existing).CurrentValues.SetValues(employee);
        await db.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> DeleteEmployee(short id)
    {
        var employee = await db.Employees.FindAsync(id);
        if (employee == null)
        {
            return false;
        }

        db.Employees.Remove(employee);
        await db.SaveChangesAsync();
        return true;
    }
}