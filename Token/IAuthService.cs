using CERP.ModelDataTransferObjects.Users.UserInputs;

namespace CERP.Token
{
    public interface IAuthService
    {
        //string Login(UserLoginInput input);
         Task<string> Login(UserLoginInput input);
    }
}
