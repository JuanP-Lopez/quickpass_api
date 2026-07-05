namespace Quickpass.Api.DTOs;

public class SlotRequest
{
    public int id { get; set; }
    public string estado { get; set; } = string.Empty;
    public int Id_Evento { get; set; }
}