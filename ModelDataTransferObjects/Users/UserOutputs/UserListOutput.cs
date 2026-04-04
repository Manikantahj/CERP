using CERP.Common;
using CERP.Models.Users;

namespace CERP.ModelDataTransferObjects.Users.UserOutputs
{
    public class UserListOutput :BaseApiResponse
    {
        public List<UserListResult> list { get; set; }
    }
}
