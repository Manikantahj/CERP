using CERP.Common;
using CERP.Entity.Users;

namespace CERP.ModelDataTransferObjects.Users.UserOutputs
{
    public class UserDetailsOutput : BaseApiResponse
    {
       public UserDetailsById_Result userDetails { set; get; }
    }
}
