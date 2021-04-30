using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kapitalist.UserManagement.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("account")]
        public IActionResult GetAccount()
        {
            return Ok("{'status': 'ok'}");
        }
    }
}
