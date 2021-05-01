using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kapitalist.UserManagement.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> logger;

        public UsersController(ILogger<UsersController> logger)
        {
            this.logger = logger;
        }
        
        [HttpGet("account")]
        public IActionResult GetAccount()
        {
            return Ok("{'status': 'ok'}");
        }
    }
}
