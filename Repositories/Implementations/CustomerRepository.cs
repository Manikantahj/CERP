using CERP.Data;
using CERP.ModelDataTransferObjects.Customers;
using CERP.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CERP.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _db;

        public CustomerRepository(DapperContext db)
        {
            _db = db;
        }
        public async Task<int> CustomerAdd(CustomerAddInput input, IDbConnection connection, IDbTransaction transaction)
        {
            DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@customer_is_end_user", input.customer_is_end_user);
                    parameters.Add("@customer_name", input.customer_name);
                    parameters.Add("@customer_logo", input.customer_logo);
                    parameters.Add("@customer_signature", input.customer_signature);
                    parameters.Add("@customer_country_code", input.customer_country_code);
                    parameters.Add("@customer_contact_number", input.customer_contact_number);
                    parameters.Add("@customer_gstn", input.customer_gstn);
                    parameters.Add("@customer_vat", input.customer_vat);
                    parameters.Add("@customer_email_address", input.customer_email_address);
                    parameters.Add("@customer_address_line1", input.customer_address_line1);
                    parameters.Add("@customer_address_line2", input.customer_address_line2);
                    parameters.Add("@customer_state_id", input.customer_state_id);
                    parameters.Add("@customer_postal_code", input.customer_postal_code);
                    parameters.Add("@customer_country_id", input.customer_country_id);
                    parameters.Add("@customer_company_website", input.customer_company_website);
                    parameters.Add("@customer_currency", input.customer_currency);
                    parameters.Add("@customer_note", input.customer_note);
                    parameters.Add("@customer_created_by", input.customer_created_by);
                    parameters.Add("@customer_created_at", input.customer_created_at);

            int? customer_id =await connection.QueryFirstOrDefaultAsync<int>("CustomerAdd", 
                                                      parameters, 
                                                      transaction: transaction, 
                                                      commandType: CommandType.StoredProcedure);

            return customer_id ?? 0;
        }
    }
}
