namespace Quickpass.Api.Models;

public class Evento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora_Inicio { get; set; }
    public TimeOnly Hora_Final { get; set; }
    public int Id_Administrador { get; set; }
    public ICollection<Slot> Slots { get; set; } = new List<Slot>();

}