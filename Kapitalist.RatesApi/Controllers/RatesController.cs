using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kapitalist.RatesApi.Controllers
{
    [Route("/api/rates")]
    public class RatesController : Controller
    {
        private readonly ILogger<RatesController> logger;

        public RatesController(ILogger<RatesController> logger)
        {
            this.logger = logger;
        }
        
        /// <summary>
        /// Get All Rates.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetRates()
        {
            return Ok("{'status': 'ok'}");
        }
    }
}