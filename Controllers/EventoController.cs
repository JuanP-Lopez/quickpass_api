using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Quickpass.Api.Data;
using Quickpass.Api.Models;
using Quickpass.Api.DTOs;
using Quickpass.Api.Services;

namespace Quickpass.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] EventoRequest request)
    {

        Console.WriteLine("Entrando al metodo crear evento");

        Console.WriteLine(request == null);

        Console.WriteLine(request?.Nombre);
        Console.WriteLine(request?.Descripcion);
        Console.WriteLine(request?.Fecha);
        Console.WriteLine(request?.Hora_Inicio);
        Console.WriteLine(request?.Hora_Final);
        Console.WriteLine(request?.Id_Administrador);

        if (request?.Hora_Inicio > request?.Hora_Final)
        {
            return BadRequest(new
            {
                mensaje = "La hora de inicio no puede ser despues de la hora final"
            });
        }

        if (request?.Fecha < DateOnly.FromDateTime(DateTime.Today))
        {
            return BadRequest(new
            {
                mensaje = "La fecha no puede ser anterior al día de hoy"
            });
        }

        var evento = await _eventoService.CrearEventoAsync(request);

        return Ok(new
        {
            mensaje = "Evento creado con exito"
        });
    }

    [HttpGet("administrador/{id}")]
    public async Task<IActionResult> ObtenerEventos(int id)
    {
        Console.WriteLine($"Id administrador recibido: {id}");

        var eventos = await _eventoService.ObtenerEventosAdministrador(id);

        return Ok(eventos);
    }

    [HttpGet("eventos")]
    public async Task<IActionResult> ObtenerEventos()
    {
        Console.WriteLine("===== OBTENER TODOS LOS EVENTOS =====");
        Console.WriteLine("Eventos consultados");
        var eventos = await _eventoService.ObtenerEventos();

        return Ok(eventos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerEvento(int id)
    {
        Console.WriteLine($"===== OBTENER EVENTO POR ID: {id} =====");
        var evento = await _eventoService.ObtenerEventoPorId(id);

        if (evento == null)
        {
            return NotFound();
        }
        return Ok(evento);
    }
}