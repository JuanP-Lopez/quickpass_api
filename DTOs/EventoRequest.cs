namespace Quickpass.Api.DTOs;

public class EventoRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora_Inicio { get; set; }
    public TimeOnly Hora_Final { get; set; }
    public int Id_Administrador { get; set; }
}