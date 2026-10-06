using Azure;
using Microsoft.AspNetCore.Mvc;

namespace CleanTodo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController: ControllerBase
    {

        [HttpGet]
        public IActionResult Ping()
        {

            return Ok(new {"Pong"});
        }
    }
}
