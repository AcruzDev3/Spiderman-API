using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        public AlertsController() {

        }

        [HttpGet("GetAlerts")]
        public async Task<IActionResult> GetAlerts() {
            try {

            } catch(Exception ex) {
                return BadRequest($"{ex.Message}");
            }
            return Ok(new { message = "This is a placeholder for AlertsController." });
        }
    }
}
