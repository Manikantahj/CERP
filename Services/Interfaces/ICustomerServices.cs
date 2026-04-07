using CERP.Common;
using CERP.ModelDataTransferObjects.Customers;

namespace CERP.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<BaseApiResponse> CustomerAdd(CustomerAddInput input);
    }
}
