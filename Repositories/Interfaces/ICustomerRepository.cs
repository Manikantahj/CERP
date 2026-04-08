using CERP.ModelDataTransferObjects.Customers;
using System.Data;

namespace CERP.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CustomerAdd(CustomerAddInput input, IDbConnection connection, IDbTransaction transaction);
        Task<int> CustomerAddressAdd(int customerId, CustomerAddressAdd input);
       
    }
}
