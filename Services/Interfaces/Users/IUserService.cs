using CERP.Common;
using CERP.ModelDataTransferObjects.Users.UserInputs;
using CERP.ModelDataTransferObjects.Users.UserOutputs;
using CERP.Models.Users;

namespace CERP.Services.Interfaces.Users
{
    public interface IUserService
    {
        Task<BaseApiResponse> UserAdd(UserAddInput input);
        Task<UserDetailsOutput> UserDetailsById(int user_id);
        Task<BaseApiResponse> UserUpdate(UserUpdateInput input);
        Task<UserListOutput> UserList(UserListInput input);
        Task<BaseApiResponse> UserDelete(UserDeleteInput input);
        Task<UserLoginOutput> UserLogin(UserLoginInput input);
    }
}
