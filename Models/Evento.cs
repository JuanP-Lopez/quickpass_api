namespace Quickpass.Api.Models;

public class Evento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripion { get; set; } = "";
    public DateOnly Fecha { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFinal { get; set; }
    public int IdAdministrador { get; set; }
    public ICollection<Slot> Slots { get; set; } = [];

}