namespace Quickpass.Api.DTOs;

public class EventoRequest
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripion { get; set; } = string.Empty;
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora_Inicio { get; set; }
    public TimeOnly Hora_Final { get; set; }
    public int IdAdministrador { get; set; }
}