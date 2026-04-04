using CERP.Common;
using CERP.Models.Users;

namespace CERP.ModelDataTransferObjects.Users.UserOutputs
{
    public class UserLoginOutput :BaseApiResponse
    {
        public UserLogin_Result user_info { set; get; }
    }
}
