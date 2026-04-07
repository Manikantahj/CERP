using CERP.Common;
using CERP.Data;
using CERP.Entity.Users;
using CERP.ModelDataTransferObjects.Users.UserInputs;
using CERP.ModelDataTransferObjects.Users.UserOutputs;
using CERP.Repositories.Interfaces;
using CERP.Services.Interfaces;
using CERP.UnitOfWork.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CERP.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IUnitOfWork _uow;
        public UserService(IUserRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }
        public async Task<BaseApiResponse> UserAdd(UserAddInput input)
        {
            BaseApiResponse res = new BaseApiResponse();                
            try
            {
                //======= Transaction start
                _uow.BeginTransaction(); 

                //====Used Hashed password concept here....
                input.user_password = BCrypt.Net.BCrypt.HashPassword(input.user_password);

                
                //====USER_INSET_OPERATION===================
                int? inserted_id = await _repo.UserAdd(input, 
                                                      _uow.Connection, 
                                                      _uow.Transaction,
                                                       input.user_password);

                //===IF USER ALREADY EXIST IT WILL RETURN DIRECTLY==========
                if (!inserted_id.HasValue || inserted_id <= 0)
                {
                    _uow.Rollback();
                    res.is_success = false;
                    res.msg = ApiMessages.MSG_ALREADY_EXIST;
                    return res;
                }

                _uow.Commit();
                res.is_success = true;
                res.msg = ApiMessages.MSG_SUCCESSFULLY_SAVED;
                res.data = inserted_id?.ToString();
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                res.is_success = false;
                res.msg = ex.Message;
            }
            return res;
        }
        public async Task<BaseApiResponse> UserDelete(UserDeleteInput input)
        {
            BaseApiResponse res = new BaseApiResponse();
            try
            {
                _uow.BeginTransaction();

                var result = await _repo.UserDelete(input, 
                                                    _uow.Connection,
                                                    _uow.Transaction);
                if (!result)
                {
                    res.is_success = false;
                    res.msg = ApiMessages.MSG_NO_DATA_FOUND;
                }
                _uow.Commit();
                res.is_success = true;
                res.msg = ApiMessages.MSG_SUCCESSFULLY_DELETED;
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                res.is_success = false;
                res.msg = ex.Message;
            }
            return res;
        }
        public async Task<UserDetailsOutput> UserDetailsById(int user_id)
        {
            UserDetailsOutput res = new UserDetailsOutput();

            UserDetailsById_Result data = await _repo.UserDetailsById(user_id);

                if (data == null)
                {                  
                    res.is_success = false;
                    res.msg = ApiMessages.MSG_NO_DATA_FOUND;
                    return res;
                }
                res.userDetails = data;
                res.is_success = true;
                res.msg = ApiMessages.MSG_DATA_FOUND;
           
            return res;
        }
        public async Task<UserListOutput> UserList(UserListInput input)
        {
            UserListOutput res = new UserListOutput();

          
                var data = await _repo.UserList(input);

                res.is_success = true;
                res.msg = data.Any() ? "User list fetched successfully" : "No records found";
                res.list = data;
            
            return res;
        }
        public async Task<UserLoginOutput> UserLogin(UserLoginInput input)
        {
            UserLoginOutput res = new UserLoginOutput();

            string user_password = await _repo.UserPasswordByLoginName(input.username);

            if (user_password == null)
            {                
                res.is_success = false;
                res.msg = ApiMessages.MSG_NO_DATA_FOUND;
                return res;
            }

            // 
            bool isValid = BCrypt.Net.BCrypt.Verify(input.password, user_password);

            if (!isValid)
            {               
                res.is_success = false;
                res.msg = "Invalid username or password";
                return res;
            }

            UserLogin_Result user_info = await _repo.UserLogin(input.username, user_password);

            if (user_info == null)
            {
                res.user_info = user_info;
                res.is_success = false;
                res.msg = ApiMessages.MSG_NO_DATA_FOUND;
                return res;
            }
            res.user_info = user_info;
            res.is_success = true;
            res.msg = ApiMessages.MSG_DATA_FOUND;

            return res;
        }
        public async Task<BaseApiResponse> UserUpdate(UserUpdateInput input)
        {
            BaseApiResponse res = new BaseApiResponse();
            try
            {
                _uow.BeginTransaction();

                var result = await _repo.UserUpdate(input, _uow.Connection, _uow.Transaction);

                if (!result)
                {
                    _uow.Rollback();
                    res.is_success = false;
                    res.msg = "Update failed";
                    return res;
                }

                _uow.Commit();
                res.is_success = result;
                res.msg = result ? "User updated successfully" : "Update failed";
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                res.is_success = false;
                res.msg = ex.Message;
            }
            return res;
        }
    }
}
