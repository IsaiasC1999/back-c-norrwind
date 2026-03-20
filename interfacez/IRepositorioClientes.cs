using ef_nortwith.dbContext;
using ef_nortwith.DTOs;

namespace ef_nortwith.interfacez;

public interface IRepositorioClientes
{
    Task<List<Customer>> GetAll(CustomerFilter filter);
    Task<Customer?> GetById(string id);
    Task<Customer?> GetByIdWithOrders(string id);
    Task<bool> Add(Customer customer);
    Task<bool> Update(string id, Customer customer);
    Task<bool> Delete(string id);
    Task<int> GetTotalCount(CustomerFilter filter);
}
