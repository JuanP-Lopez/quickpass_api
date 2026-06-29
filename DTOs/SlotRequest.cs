namespace Quickpass.Api.DTOs;

public class SlotRequest
{
    public int Id { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int Id_Evento { get; set; }
}