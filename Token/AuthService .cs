using CERP.ModelDataTransferObjects.Users.UserInputs;
using CERP.Models.Users;
using CERP.Services.Interfaces.Users;

namespace CERP.Token
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly ITokenGeneratorService tokenService;
        public AuthService(IUserService userService, ITokenGeneratorService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        public async Task<string> Login(UserLoginInput input)
        {
            // Step 1: Validate user from DB
            var result = await userService.UserLogin(input);

            if (result == null || !result.is_success || result.user_info == null)
            {
                return null;
            }
                
            // Step 2: Generate token
            return tokenService.GenerateToken(input.username, input.password);
        }
    }
}
