namespace AuthService.Controller
{
    using AuthService.Business;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController : ControllerBase
    {
        IConfiguration _configuration;
        public AuthsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login(string userName, string password)
        {
            TokenHandler._configuration = _configuration;
            return Ok(userName == "admin" && password == "admin" ? TokenHandler.CreateAccessToken() : new UnauthorizedResult());
        }
    }
}
