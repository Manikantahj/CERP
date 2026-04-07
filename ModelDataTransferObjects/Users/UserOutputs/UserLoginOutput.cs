using CERP.Common;
using CERP.Entity.Users;

namespace CERP.ModelDataTransferObjects.Users.UserOutputs
{
    public class UserLoginOutput :BaseApiResponse
    {
        public UserLogin_Result user_info { set; get; }
    }
}
