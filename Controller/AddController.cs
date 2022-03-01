using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parking_System.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {
        [Authorize(Roles ="CUSTOMER")]
        [HttpPost("compute")]
        public IActionResult Sum([FromQuery(Name = "Value1")] int value1, [FromQuery(Name = "Value2")] int value2)
        {
            var result = value1 + value2;
            return Ok(result);
        }
    }
}
