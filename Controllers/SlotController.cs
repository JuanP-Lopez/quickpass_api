using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Quickpass.Api.Data;
using Quickpass.Api.Models;
using Quickpass.Api.DTOs;
using Quickpass.Api.Services;

namespace Quickpass.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SlotController : ControllerBase
{
    private readonly ISlotService _slotService;

    public SlotController(ISlotService slotService)
    {
        _slotService = slotService;
    }

    [HttpPost("reservar")]
    public async Task<IActionResult> Reservar([FromBody] ReservarSlotRequest request)
    {
        var reservado = await _slotService.ReservarSlot(
            request.IdSlot,
            request.IdUsuario
        );

        if (!reservado)
        {
            return BadRequest(new
            {
                mensaje = "No fue posible reservar el turno"
            });
        }

        return Ok(new
        {
            mensaje = "Turno reservado. Espere confirmacion."
        });
    }

    [HttpGet("usuario/{id}")]
    public async Task<IActionResult> ObtenerTurnosUsuario(int id)
    {
        var turnos = await _slotService.ObtenerTurnosUsuario(id);

        return Ok(turnos);
    }

    [HttpGet("solicitudes/{id}")]
    public async Task<IActionResult> ObtenerSolicitudes (int id)
    {
        var solicitudes = await _slotService.ObtenerSolicitudes(id);

        return Ok(solicitudes);
    }
}