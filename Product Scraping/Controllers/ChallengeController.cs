using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_Scraping.Controllers
{
    [Route("/")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Fullstack Challenge 20201026");
        }
    }
}
