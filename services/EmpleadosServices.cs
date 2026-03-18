using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;

namespace ef_nortwith.services;

public class EmpleadosServices
{
    private readonly IRepositorioEmpleados repo;
    private readonly NorthwindContext dbContext;

    public EmpleadosServices(IRepositorioEmpleados repo, NorthwindContext dbContext)
    {
        this.repo = repo;
        this.dbContext = dbContext;
    }

    public async Task<ResponseServices> GetAllEmployees()
    {
        try
        {
            var employees = await repo.GetAllEmployees();
            var employeesDTO = employees.Select(e => Mappers.EmployeeEntityToEmployeeDTO(e)).ToList();
            return new ResponseServices
            {
                Success = true,
                Result = employeesDTO,
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseServices
            {
                Success = false,
                Result = null,
                Error = ex.Message
            };
        }
    }

    public async Task<ResponseServices> GetEmployees(EmployeeFilter filter)
    {
        try
        {
            var employees = await repo.GetEmployees(filter);
            var employeesDTO = employees.Select(e => Mappers.EmployeeEntityToEmployeeDTO(e)).ToList();
            return new ResponseServices
            {
                Success = true,
                Result = employeesDTO,
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseServices
            {
                Success = false,
                Result = null,
                Error = ex.Message
            };
        }
    }

    public async Task<ResponseServices> GetEmployeeById(short id)
    {
        try
        {
            var employee = await repo.GetEmployeeById(id);
            if (employee == null)
            {
                return new ResponseServices
                {
                    Success = false,
                    Result = null,
                    Error = "Empleado no encontrado"
                };
            }

            var employeeDTO = Mappers.EmployeeEntityToEmployeeDTO(employee);
            return new ResponseServices
            {
                Success = true,
                Result = employeeDTO,
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseServices
            {
                Success = false,
                Result = null,
                Error = ex.Message
            };
        }
    }

    public async Task<ResponseServices> CreateEmployee(EmployeeAddDTO dto)
    {
        try
        {
            var employee = Mappers.EmployeeAddDTOToEmployeeEntity(dto);
            var createdEmployee = await repo.CreateEmployee(employee);

            if (!string.IsNullOrEmpty(dto.Username) && !string.IsNullOrEmpty(dto.Password))
            {
                var role = string.IsNullOrEmpty(dto.Role) ? "usuario" : dto.Role;
                
                if (role != "admin" && role != "usuario")
                {
                    return new ResponseServices
                    {
                        Success = false,
                        Result = null,
                        Error = "El rol debe ser 'admin' o 'usuario'"
                    };
                }

                var user = new User
                {
                    EmployeeId = createdEmployee.EmployeeId,
                    Username = dto.Username,
                    PasswordHash = dto.Password,
                    Role = role,
                    IsActive = true
                };
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }

            var employeeWithUser = await repo.GetEmployeeById(createdEmployee.EmployeeId);
            return new ResponseServices
            {
                Success = true,
                Result = Mappers.EmployeeEntityToEmployeeDTO(employeeWithUser!),
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseServices
            {
                Success = false,
                Result = null,
                Error = ex.Message
            };
        }
    }

    public async Task<ResponseServices> UpdateEmployee(short id, EmployeeAddDTO dto)
    {
        try
        {
            var existingEmployee = await repo.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return new ResponseServices
                {
                    Success = false,
                    Result = null,
                    Error = "Empleado no encontrado"
                };
            }

            existingEmployee.LastName = dto.LastName;
            existingEmployee.FirstName = dto.FirstName;
            existingEmployee.Title = dto.Title;
            existingEmployee.TitleOfCourtesy = dto.TitleOfCourtesy;
            existingEmployee.BirthDate = dto.BirthDate;
            existingEmployee.HireDate = dto.HireDate;
            existingEmployee.Address = dto.Address;
            existingEmployee.City = dto.City;
            existingEmployee.Region = dto.Region;
            existingEmployee.PostalCode = dto.PostalCode;
            existingEmployee.Country = dto.Country;
            existingEmployee.HomePhone = dto.HomePhone;
            existingEmployee.Extension = dto.Extension;
            existingEmployee.Notes = dto.Notes;
            existingEmployee.ReportsTo = dto.ReportsTo;

            var updatedEmployee = await repo.UpdateEmployee(existingEmployee);

            var existingUser = dbContext.Users.FirstOrDefault(u => u.EmployeeId == id);

            if (!string.IsNullOrEmpty(dto.Username) && !string.IsNullOrEmpty(dto.Password))
            {
                var role = string.IsNullOrEmpty(dto.Role) ? "usuario" : dto.Role;
                
                if (role != "admin" && role != "usuario")
                {
                    return new ResponseServices
                    {
                        Success = false,
                        Result = null,
                        Error = "El rol debe ser 'admin' o 'usuario'"
                    };
                }

                if (existingUser != null)
                {
                    existingUser.Username = dto.Username;
                    existingUser.PasswordHash = dto.Password;
                    existingUser.Role = role;
                }
                else
                {
                    var newUser = new User
                    {
                        EmployeeId = id,
                        Username = dto.Username,
                        PasswordHash = dto.Password,
                        Role = role,
                        IsActive = true
                    };
                    dbContext.Users.Add(newUser);
                }
                
                await dbContext.SaveChangesAsync();
            }

            var employeeWithUser = await repo.GetEmployeeById(id);
            return new ResponseServices
            {
                Success = true,
                Result = Mappers.EmployeeEntityToEmployeeDTO(employeeWithUser!),
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseServices
            {
                Success = false,
                Result = null,
                Error = ex.Message
            };
        }
    }

    public async Task<ResponseServices> DeleteEmployee(short id)
    {
        try
        {
            var employee = await repo.GetEmployeeById(id);
            if (employee == null)
            {
                return new ResponseServices
                {
                    Success = false,
                    Result = null,
                    Error = "Empleado no encontrado"
                };
            }

            var usersToDelete = dbContext.Users.Where(u => u.EmployeeId == id).ToList();
            if (usersToDelete.Any())
            {
                dbContext.Users.RemoveRange(usersToDelete);
            }

            var deleted = await repo.DeleteEmployee(id);
            if (!deleted)
            {
                return new ResponseServices
                {
                    Success = false,
                    Result = null,
                    Error = "Error al eliminar el empleado"
                };
            }

            await dbContext.SaveChangesAsync();

            return new ResponseServices
            {
                Success = true,
                Result = true,
                Error = ""
            };
        }
        catch (Exception ex)
        {
            return new ResponseServices
            {
                Success = false,
                Result = null,
                Error = ex.Message
            };
        }
    }
}