using CERP.Common;
using CERP.Entity.Users;

namespace CERP.ModelDataTransferObjects.Users.UserOutputs
{
    public class UserListOutput :BaseApiResponse
    {
        public List<UserListResult> list { get; set; }
    }
}
