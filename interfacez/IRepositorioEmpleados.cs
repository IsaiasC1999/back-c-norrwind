using ef_nortwith.dbContext;
using ef_nortwith.DTOs;

namespace ef_nortwith.interfacez;

public interface IRepositorioEmpleados
{
    Task<List<Employee>> GetAllEmployees();
    Task<List<Employee>> GetEmployees(EmployeeFilter filter);
    Task<Employee?> GetEmployeeById(short id);
    Task<Employee> CreateEmployee(Employee employee);
    Task<Employee> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(short id);
}