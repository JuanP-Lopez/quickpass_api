using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Quickpass.Api.Data;
using Quickpass.Api.Models;
using Quickpass.Api.DTOs;

namespace Quickpass.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly QuickPassContext _context;

    public EventoController(QuickPassContext context)
    {
        _context = context;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] EventoRequest request)
    {
        return Ok(new
        {
            mensaje = "Evento creado con exito"
        });
    }
}