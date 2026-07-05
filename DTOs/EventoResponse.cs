namespace Quickpass.Api.DTOs;

public class EventoResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora_Inicio { get; set; }
    public TimeOnly Hora_Final { get; set; }
}