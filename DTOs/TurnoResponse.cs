namespace Quickpass.Api.DTOs;

public class TurnoResponse
{
    public int IdEvento { get; set; }
    public string NombreEvento { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public DateOnly Fecha { get; set; }
    public string Estado { get; set; } = "";
}