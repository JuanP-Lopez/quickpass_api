namespace Quickpass.Api.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Correo { get; set; } = "";
    public string Password { get; set; } = "";
    public int RolId { get; set; }
    public Rol? Rol { get; set; }
    public DateTime Fecha { get; set; }
    public bool Activo { get; set; }
}