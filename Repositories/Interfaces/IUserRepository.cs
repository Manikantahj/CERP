using CERP.Entity.Users;
using CERP.ModelDataTransferObjects.Users.UserInputs;
using System.Data;

namespace CERP.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int?> UserAdd(UserAddInput input, IDbConnection connection, IDbTransaction transaction,string hashed_password);
        Task<UserDetailsById_Result> UserDetailsById(int user_id);
        Task<bool> UserUpdate(UserUpdateInput input, IDbConnection connection, IDbTransaction transaction);
        Task<List<UserListResult>> UserList(UserListInput input);
        Task<bool> UserDelete(UserDeleteInput input, IDbConnection connection, IDbTransaction transaction);
        Task<UserLogin_Result> UserLogin(string username, string password);
        Task<string> UserPasswordByLoginName(string username);
    }
}
