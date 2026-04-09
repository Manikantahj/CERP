using CERP.Data;
using CERP.Logics;
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

            foreach (CustomerAddressAdd ads in input.cust_address)
            {
                DynamicParameters addressParameters = new DynamicParameters();
                addressParameters.Add("@customer_id", customer_id);
                addressParameters.Add("@customer_address_type", ads.customer_address_type);
                addressParameters.Add("@customer_address_contact_name", ads.customer_address_contact_name);
                addressParameters.Add("@customer_address_country_code", ads.customer_address_country_code);
                addressParameters.Add("@customer_address_contact_number", ads.customer_address_contact_number);
                addressParameters.Add("@customer_address_address_line1", ads.customer_address_address_line1);
                addressParameters.Add("@customer_address_address_line2", ads.customer_address_address_line2);
                addressParameters.Add("@customer_address_address_line3", ads.customer_address_address_line3);
                addressParameters.Add("@customer_address_state_id", ads.customer_address_state_id);
                addressParameters.Add("@customer_address_country_id", ads.customer_address_country_id);
                addressParameters.Add("@customer_address_postal_code", ads.customer_address_postal_code);
                addressParameters.Add("@customer_address_email_address", ads.customer_address_email_address);
                addressParameters.Add("@customer_address_gstn", ads.customer_address_gstn);
                addressParameters.Add("@customer_address_vat", ads.customer_address_vat);
                addressParameters.Add("@customer_address_city", ads.customer_address_city);
                addressParameters.Add("@customer_address_created_by", ads.logged_in_user_id);
                addressParameters.Add("@customer_address_created_at", MUtils.getCurrentDateTime());
                

                await connection.ExecuteAsync("CustomerAddressAdd",
                                              addressParameters,
                                              transaction: transaction,
                                              commandType: CommandType.StoredProcedure);
            }

            return customer_id ?? 0;
        }
      
        public async Task CustomerAddressAdd(int? customer_id, CustomerAddressAdd input, IDbConnection connection, IDbTransaction transaction)
        {
            DynamicParameters addressParameters = new DynamicParameters();
                addressParameters.Add("@customer_id", customer_id);
                addressParameters.Add("@customer_address_type", input.customer_address_type);
                addressParameters.Add("@customer_address_contact_name", input.customer_address_contact_name);
                addressParameters.Add("@customer_address_country_code", input.customer_address_country_code);
                addressParameters.Add("@customer_address_contact_number", input.customer_address_contact_number);
                addressParameters.Add("@customer_address_address_line1", input.customer_address_address_line1);
                addressParameters.Add("@customer_address_address_line2", input.customer_address_address_line2);
                addressParameters.Add("@customer_address_address_line3", input.customer_address_address_line3);
                addressParameters.Add("@customer_address_state_id", input.customer_address_state_id);
                addressParameters.Add("@customer_address_country_id", input.customer_address_country_id);
                addressParameters.Add("@customer_address_postal_code", input.customer_address_postal_code);
                addressParameters.Add("@customer_address_email_address", input.customer_address_email_address);
                addressParameters.Add("@customer_address_gstn", input.customer_address_gstn);
                addressParameters.Add("@customer_address_vat", input.customer_address_vat);
                addressParameters.Add("@customer_address_city", input.customer_address_city);
                addressParameters.Add("@customer_address_created_by", input.logged_in_user_id);
                addressParameters.Add("@customer_address_created_at", MUtils.getCurrentDateTime());
                

                await connection.ExecuteAsync("CustomerAddressAdd",
                                              addressParameters,
                                              transaction: transaction,
                                              commandType: CommandType.StoredProcedure);
        }
    }
}
