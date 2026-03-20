using ef_nortwith.dbContext;
using ef_nortwith.DTOs;
using ef_nortwith.interfacez;

namespace ef_nortwith.services;

public class ClientesServices
{
    private readonly IRepositorioClientes repositorioClientes;

    public ClientesServices(IRepositorioClientes repositorioClientes)
    {
        this.repositorioClientes = repositorioClientes;
    }

    public async Task<ResponseServices> GetAll(CustomerFilter filter)
    {
        var customers = await repositorioClientes.GetAll(filter);
        var total = await repositorioClientes.GetTotalCount(filter);

        return new ResponseServices
        {
            Success = true,
            Result = new
            {
                data = Mappers.CustomerEntitiesToCustomerDTOs(customers),
                total
            },
            Error = ""
        };
    }

    public async Task<ResponseServices> GetById(string id)
    {
        var customer = await repositorioClientes.GetById(id);

        if (customer == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El cliente no existe"
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.CustomerEntityToCustomerDTO(customer),
            Error = ""
        };
    }

    public async Task<ResponseServices> GetByIdWithOrders(string id)
    {
        var customer = await repositorioClientes.GetByIdWithOrders(id);

        if (customer == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El cliente no existe"
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.CustomerEntityToCustomerOrdersDTO(customer),
            Error = ""
        };
    }

    public async Task<ResponseServices> Add(CustomerAddDTO customerDTO)
    {
        if (string.IsNullOrEmpty(customerDTO.CustomerId) || customerDTO.CustomerId.Length != 5)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El ID del cliente debe tener exactamente 5 caracteres"
            };
        }

        if (string.IsNullOrEmpty(customerDTO.CompanyName))
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El nombre de la empresa es obligatorio"
            };
        }

        var customer = Mappers.CustomerAddDTOToEntity(customerDTO);
        var result = await repositorioClientes.Add(customer);

        if (!result)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El cliente ya existe o no se pudo crear"
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.CustomerEntityToCustomerDTO(customer),
            Error = ""
        };
    }

    public async Task<ResponseServices> Update(string id, CustomerAddDTO customerDTO)
    {
        var exists = await repositorioClientes.GetById(id);
        if (exists == null)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El cliente no existe"
            };
        }

        var customer = Mappers.CustomerAddDTOToEntity(customerDTO);
        customer.CustomerId = id;

        var result = await repositorioClientes.Update(id, customer);

        if (!result)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "No se pudo actualizar el cliente"
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = Mappers.CustomerEntityToCustomerDTO(customer),
            Error = ""
        };
    }

    public async Task<ResponseServices> Delete(string id)
    {
        var result = await repositorioClientes.Delete(id);

        if (!result)
        {
            return new ResponseServices
            {
                Success = false,
                Error = "El cliente no existe o no se pudo eliminar"
            };
        }

        return new ResponseServices
        {
            Success = true,
            Result = "Cliente eliminado correctamente",
            Error = ""
        };
    }
}
