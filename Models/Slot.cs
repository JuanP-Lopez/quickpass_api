namespace Quickpass.Api.Models;

public class Slot
{
    public int id { get; set; }
    public TimeOnly hora_inicio { get; set; }
    public TimeOnly hora_final { get; set; }
    public string estado { get; set; } = "";
    public int id_evento { get; set; }
    public Evento? Evento { get; set; }

}