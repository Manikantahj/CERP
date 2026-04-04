using Asp.Versioning;
using CERP.Common;
using CERP.Data;
using CERP.Logics;
using CERP.ModelDataTransferObjects.Users.UserInputs;
using CERP.ModelDataTransferObjects.Users.UserOutputs;
using CERP.Models.Users;
using CERP.Services.Interfaces.Users;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CERP.Controllers.Users
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableRateLimiting("fixed")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        
        [HttpPost("UserAdd")]
        [MapToApiVersion("1.0")]
        public async Task<BaseApiResponse> UserAdd([FromBody] UserAddInput input)
        {
            return await _service.UserAdd(input);         
        }

        [HttpPost("UserAdd")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> UserAdd2(UserAddInput input)
        {        
            return Ok(await _service.UserAdd(input));
        }

        [Authorize]
        [HttpPost("UserList")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<UserListOutput> UserList(UserListInput input)
        {                   
            return await _service.UserList(input);          
                    
        }

        [Authorize]
        [HttpGet("UserDetailsById")]      
        public async Task<UserDetailsOutput> UserDetailsById(int user_id)
        {
           
            return await _service.UserDetailsById(user_id);

        }

        [Authorize]
        [HttpPost("UserLogin")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("2.0")]
        public async Task<UserLoginOutput> UserLogin(UserLoginInput input)
        {

            return await _service.UserLogin(input);

        }

        [Authorize]
        [HttpPut("UserUpdate")]
        public async Task<BaseApiResponse> UserUpdate(UserUpdateInput input)
        {
            BaseApiResponse res = new BaseApiResponse();
            res.is_success = false;
            res.msg = "No Data Found";

            if (input == null)
            {
                res.msg = "Invalid request";
                return res;
            }
            return await _service.UserUpdate(input);

        }

        [Authorize]
        [HttpDelete("UserDelete")]
        public async Task<BaseApiResponse> UserDelete(UserDeleteInput input)
        {
            BaseApiResponse res = new BaseApiResponse();
            res.is_success = false;
            res.msg = "No Data Found";

            if (input == null)
            {
                res.msg = "Invalid request";
                return res;
            }
            return await _service.UserDelete(input);

        }

    }
}














































/*private readonly DapperContext _db;

public UserController(DapperContext db)
{
    _db = db;
}

[HttpPost("UserAdd")]
public async Task<BaseResponse> UserAdd(UserAddInput input)
{
    BaseResponse res = new BaseResponse();
    res.is_success = false;
    res.msg = "NO Data Found";
    res.extra_info = null;


    if (input == null)
    {
        res.is_success = false;
        res.msg = "Invalid request data";
        res.extra_info = null;
        return res;
    }       
    try
    {
            using var connection = _db.CreateConnection();
            connection.Open();

                using var transaction = connection.BeginTransaction();

            try
            {
                    var parameters = new DynamicParameters();

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
                    parameters.Add("@user_email_id", input.user_email_id);
                    parameters.Add("@user_created_by", input.logged_in_user_id);
                    parameters.Add("@user_created_at", MUtils.getCurrentDateTime());


                    var insertedId = await connection.QueryFirstOrDefaultAsync<int>(
                                                   "UserAdd",
                                                   parameters,
                                                   transaction: transaction,
                                                   commandType: CommandType.StoredProcedure);

                    transaction.Commit();
                    res.is_success = true;
                    res.msg = "User created successfully";
                    res.extra_info = insertedId.ToString();

                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    res.is_success = false;
                    res.msg = e.Message;
                }                                    
    }
    catch (Exception ex)
    {              
        res.msg = ex.Message;
        res.extra_info = ex.Message;                
    }
    return res;
}

//===============================================================

[HttpGet("UserDetailsById/{user_id}")]
public async Task<UserDetailsOutput> UserDetailsById(int user_id)
{
    UserDetailsOutput res = new UserDetailsOutput();
    res.is_success = false;
    res.msg = "NO Data Found";
    res.extra_info = null;

    try
    {         
        var parameters = new DynamicParameters();

        parameters.Add("@user_user_id", user_id);

        using var connection = _db.CreateConnection();

        var user_details = await connection.QueryFirstOrDefaultAsync<UserDetailsById_Result>(
                                                                    "UserDetailsById",
                                                                    parameters,
                                                                    commandType: CommandType.StoredProcedure);


        if (user_details == null)
        {
            res.is_success = false;
            res.msg = "User not found";
            return res;                                         
        }

        res.is_success = true;
        res.msg = "User details fetched successfully";
        res.userDetails = user_details;                
    }
    catch (Exception ex)
    {
        res.is_success = false;
        res.msg = ex.Message;                              
    }
    return res;
}


//=====================================================================

[HttpPut("UserUpdate")]
public async Task<UserUpdateOutput> UserUpdate(UserUpdateInput input)
{
    UserUpdateOutput res= new UserUpdateOutput();
    if (input == null)
    {
      res.is_success = false;
      res.msg = "Invalid request data";
      return res;               
    }

    try
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

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync("UserUpdate", parameters, commandType: CommandType.StoredProcedure);

        res.is_success = true;
        res.msg = "User updated successfully";                                
    }
    catch (Exception ex)
    {
        res.is_success = false;
        res.msg = ex.Message;                               
    }
    return res;
}

[HttpPost("UserList")]
public async Task<UserListOutput> UserList(UserListInput input)
{
    UserListOutput res = new UserListOutput();
    res.is_success = false;
    res.msg = "No Data Found";

    if (input == null)
    {
        res.msg = "Invalid request";
        return res;
    }

    try
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@search_text", input.search_text);
                parameters.Add("@list_limit", input.list_limit);
                parameters.Add("@current_size", input.current_size);

        var users_list = await connection.QueryAsync<UserListResult>(
                                                            "UserList",
                                                            parameters,
                                                            commandType: CommandType.StoredProcedure);

        var userList = users_list.ToList();

        res.is_success = true;
        res.msg = userList.Any() ? "User list fetched successfully" : "No records found";                           
        res.list = userList;
        return res;
    }
    catch (Exception ex)
    {
        res.msg = "Internal server error";
        res.extra_info = ex.Message;
        return res;
    }
}*/

