using CERP.ModelDataTransferObjects.Users.UserInputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CERP.Token
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService authService;
        public TokenController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("login")]    
        public async Task<IActionResult> GetToken(UserLoginInput input)
        {
            if (input == null || string.IsNullOrEmpty(input.username) || string.IsNullOrEmpty(input.password))
            {
                return BadRequest("Invalid input");
            }

            var token = await authService.Login(input);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid username or password");
            }
            //return Unauthorized();

            return Ok(new
            {
                access_token = token
            });

        }
    }
}
