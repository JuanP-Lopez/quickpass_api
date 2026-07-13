namespace Quickpass.Api.DTOs;

public class SlotResponse
{
    public int Id { get; set; }

    public TimeOnly Hora_Inicio { get; set; }

    public TimeOnly Hora_Final { get; set; }

    public string Estado { get; set; } = "";
}