namespace Quickpass.Api.Models;

public class Slot
{
    public int Id { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFinal { get; set; }
    public string Estado { get; set; } = "";
    public int IdEvento { get; set; }
    public Evento? Evento { get; set; }

}