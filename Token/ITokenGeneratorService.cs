using CERP.ModelDataTransferObjects.Users;

namespace CERP.Token
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(string user_name, string password);
    }
}
