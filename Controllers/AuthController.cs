using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Quickpass.Api.Data;
using Quickpass.Api.Models;
using Quickpass.Api.DTOs;

namespace Quickpass.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly QuickPassContext _context;

    public AuthController(QuickPassContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        Console.WriteLine("Entró al método Register");

        Console.WriteLine(request == null);

        Console.WriteLine(request?.Nombre);
        Console.WriteLine(request?.Apellido);
        Console.WriteLine(request?.Correo);
        Console.WriteLine(request?.Password);

        bool existe = await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo);

        if (existe)
        {
            return BadRequest(new
            {
                mensaje = "Este correo ya esta registrado"
            });
        }

        Usuario usuario = new Usuario
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Correo = request.Correo,
            Password = request.Password,

            RolId = 2,
            Activo = true
        };

        _context.Usuarios.Add(usuario);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje = "Usuario creado con exito"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        Console.WriteLine("Entró al método Login");

        Console.WriteLine(request == null);
        Console.WriteLine(request?.Correo);
        Console.WriteLine(request?.Password);

        Usuario? usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Correo == request.Correo && u.Password == request.Password);

        if (usuario is null)
        {
            return BadRequest(new {
                mensaje = "No hay usuario con este correo o contraseña"
            });
        }

        return Ok(new {
            success = true,
            id = usuario.Id,
            nombre = usuario.Nombre,
            apellido = usuario.Apellido,
            correo = usuario.Correo,
            rol = usuario.Rol?.RolNombre
        });


    }
}