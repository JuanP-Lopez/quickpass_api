namespace Quickpass.Api.DTOs;

public class SolicitudTurnoResponse
{
    public int IdSlot { get; set; }
    public int IdEvento { get; set; }
    public string Evento { get; set; } = "";
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora_Inicio { get; set; }
    public TimeOnly Hora_Final { get; set; }
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; } = "";
    public string ApellidoUsuario { get; set; } = "";
    public string Correo { get; set; } = "";
}