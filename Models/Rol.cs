namespace Quickpass.Api.Models;

public class Rol
{
    public int Id { get; set; }
    public string RolNombre { get; set; } = "";
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}