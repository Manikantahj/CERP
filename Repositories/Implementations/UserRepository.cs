using CERP.Data;
using CERP.Entity.Users;
using CERP.Logics;
using CERP.ModelDataTransferObjects.Users.UserInputs;
using CERP.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace CERP.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _db;      
        public UserRepository(DapperContext db)
        {
            _db = db;
        }
        public async Task<int?> UserAdd(UserAddInput input, IDbConnection connection, IDbTransaction transaction, string hashed_password)
        {            
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@user_employee_name", input.user_employee_name);
                parameters.Add("@user_login_name", input.user_login_name);
                parameters.Add("@user_password", hashed_password);
                parameters.Add("@user_designation", input.user_designation);
                parameters.Add("@user_country_code", input.user_country_code);
                parameters.Add("@user_mobile_number", input.user_mobile_number);
                parameters.Add("@user_id_no", input.user_id_no);
                parameters.Add("@user_department_id", input.user_department_id);
                parameters.Add("@user_type", input.user_type);
                parameters.Add("@user_educational_qualification", input.user_educational_qualification);
                parameters.Add("@user_date_of_birth", input.user_date_of_birth);
                parameters.Add("@user_joining_date", input.user_joining_date);
                parameters.Add("@user_experience", input.user_experience);
                parameters.Add("@user_address", input.user_address);
                parameters.Add("@user_profile_photo", input.user_profile_photo);
                parameters.Add("@user_signature_photo", input.user_signature_photo);
                parameters.Add("@user_email_id", input.user_email_id);
                parameters.Add("@user_created_by", input.logged_in_user_id);
                parameters.Add("@user_created_at", MUtils.getCurrentDateTime());


                int? inserted_id = await connection.QueryFirstOrDefaultAsync<int?>(
                                                                            "UserAdd", 
                                                                            parameters,
                                                                            transaction: transaction,
                                                                            commandType: CommandType.StoredProcedure);
            
            return inserted_id;

           
        }

        public async Task<bool> UserDelete(UserDeleteInput input, IDbConnection connection, IDbTransaction transaction)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@user_user_id", input.user_user_id);

            var rows = await connection.QueryFirstOrDefaultAsync<int>(
                                                                "UserDelete",
                                                                parameters,
                                                                transaction: transaction,
                                                                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<UserDetailsById_Result> UserDetailsById(int user_id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@user_user_id", user_id);

            using var connection = _db.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<UserDetailsById_Result>(
                                                                                "UserDetailsById",
                                                                                    parameters,
                                                                                    commandType: CommandType.StoredProcedure);
        }

        public async Task<List<UserListResult>> UserList(UserListInput input)
        {
                        using var connection = _db.CreateConnection();

                            var parameters = new DynamicParameters();
                                    parameters.Add("@search_text", input.search_text);
                                    parameters.Add("@list_limit", input.list_limit);
                                    parameters.Add("@current_size", input.current_size);

                    var user_list = await connection.QueryAsync<UserListResult>(
                        "UserList",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    return user_list.ToList();
        }

        public async Task<UserLogin_Result> UserLogin(string username, string password)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@username", username);
            parameters.Add("@password", password);

            using var connection = _db.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<UserLogin_Result>( "UserLogin",
                                                                                 parameters,
                                                                                 commandType: CommandType.StoredProcedure);
        }

        public async Task<string> UserPasswordByLoginName(string username)
        {
            DynamicParameters p = new DynamicParameters();

            using var connection = _db.CreateConnection();

            p.Add("user_login_name", username);

          string password = await connection.QueryFirstOrDefaultAsync<string>("UserPasswordByLoginName",
                                                    p,
                                                    commandType: CommandType.StoredProcedure);
            return password;
        }

        public async Task<bool> UserUpdate(UserUpdateInput input, IDbConnection connection, IDbTransaction transaction)
        {
           
                var parameters = new DynamicParameters();

                parameters.Add("@user_user_id", input.user_user_id);
                parameters.Add("@user_employee_name", input.user_employee_name);
                parameters.Add("@user_login_name", input.user_login_name);
                parameters.Add("@user_password", input.user_password);
                parameters.Add("@user_designation", input.user_designation);
                parameters.Add("@user_country_code", input.user_country_code);
                parameters.Add("@user_mobile_number", input.user_mobile_number);
                parameters.Add("@user_id_no", input.user_id_no);
                parameters.Add("@user_department_id", input.user_department_id);
                parameters.Add("@user_type", input.user_type);
                parameters.Add("@user_educational_qualification", input.user_educational_qualification);
                parameters.Add("@user_date_of_birth", input.user_date_of_birth);
                parameters.Add("@user_joining_date", input.user_joining_date);
                parameters.Add("@user_experience", input.user_experience);
                parameters.Add("@user_address", input.user_address);
                parameters.Add("@user_profile_photo", input.user_profile_photo);
                parameters.Add("@user_signature_photo", input.user_signature_photo);
                parameters.Add("@user_note", input.user_note);
                parameters.Add("@user_email_id", input.user_email_id);
                parameters.Add("@user_role_id", input.user_role_id);
                parameters.Add("@user_updated_by", input.logged_in_user_id);
                parameters.Add("@user_updated_at", MUtils.getCurrentDateTime());



                var rows = await connection.ExecuteAsync("UserUpdate", 
                                                          parameters,
                                                          transaction: transaction,
                                                          commandType: CommandType.StoredProcedure);
                
                return rows > 0;
           
        }
    }
}
