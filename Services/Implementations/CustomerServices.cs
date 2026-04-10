using CERP.Common;
using CERP.ModelDataTransferObjects.Customers;
using CERP.Repositories.Interfaces;
using CERP.Services.Interfaces;
using CERP.UnitOfWork.Interfaces;

namespace CERP.Services.Implementations
{
    public class CustomerServices : ICustomerServices
    {
        ICustomerRepository _cp;
        IUnitOfWork _uow;
        public CustomerServices(ICustomerRepository customerRepository, IUnitOfWork uow)
        {
            _cp = customerRepository;
            _uow = uow;
        }

        public async Task<BaseApiResponse> CustomerAdd(CustomerAddInput input)
        {
            BaseApiResponse res = new BaseApiResponse();
            try
            {
                //============= Validation logic here =============
                _uow.BeginTransaction();

                    int? customer_id   = await _cp.CustomerAdd(input,
                                                            _uow.Connection, 
                                                            _uow.Transaction);
                if (customer_id <=0)
                {
                    _uow.Rollback();
                    res.is_success = false;
                    res.msg = ApiMessages.MSG_ALREADY_EXIST;
                    return res;
                }


                if (input.cust_address != null && input.cust_address.Count() > 0)
                {
                    foreach (var address in input.cust_address)
                    {
                        await _cp.CustomerAddressAdd(customer_id, address, _uow.Connection, _uow.Transaction);

                    }
                }
                        
                _uow.Commit();
                res.is_success = true;
                res.msg = ApiMessages.MSG_SUCCESSFULLY_SAVED;
                res.data = customer_id?.ToString();
            }
            catch(Exception ex)
            {
                _uow.Rollback();
                res.is_success = false;
                res.msg = ex.Message;
            }
            return res;
        }
    }
}
