using Microsoft.AspNetCore.Mvc;

namespace Quickpass.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            mensaje = "Funcionamiento correcto",
            fecha = DateTime.Now
        });
    }
}