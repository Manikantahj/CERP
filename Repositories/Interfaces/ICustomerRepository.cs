using CERP.ModelDataTransferObjects.Customers;
using System.Data;

namespace CERP.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CustomerAdd(CustomerAddInput input, IDbConnection connection, IDbTransaction transaction);
        Task CustomerAddressAdd(int? customer_id, CustomerAddressAdd input, IDbConnection connection, IDbTransaction transaction);
       
    }
}
