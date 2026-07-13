using Microsoft.EntityFrameworkCore;
using Quickpass.Api.Data;
using Quickpass.Api.DTOs;
using Quickpass.Api.Models;

namespace Quickpass.Api.Services;

public class EventoService : IEventoService
{
    private readonly QuickPassContext _context;

    public EventoService(QuickPassContext context)
    {
        _context = context;
    }

    public async Task<Evento> CrearEventoAsync(EventoRequest request)
    {
        Evento evento = new Evento
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Fecha = request.Fecha,
            Hora_Inicio = request.Hora_Inicio,
            Hora_Final = request.Hora_Final,
            Id_Administrador = request.Id_Administrador
        };

        _context.Eventos.Add(evento);

        await _context.SaveChangesAsync();

        Console.WriteLine("Antes de generar slots");

        var slots = GenerarSlots(evento);

        Console.WriteLine("Después de generar slots");

        Console.WriteLine($"Slots generados: {slots.Count}");

        _context.Slots.AddRange(slots);

        Console.WriteLine(_context.ChangeTracker.Entries<Slot>().Count());

        var filas = await _context.SaveChangesAsync();

        Console.WriteLine($"Filas guardadas: {filas}");

        return evento;
    }

    private List<Slot> GenerarSlots(Evento evento)
    {

        Console.WriteLine($"Evento: {evento.Id}");
        Console.WriteLine($"Inicio: {evento.Hora_Inicio}");
        Console.WriteLine($"Fin: {evento.Hora_Final}");

        List<Slot> slots = new();

        TimeOnly horaActual = evento.Hora_Inicio;

        while (horaActual < evento.Hora_Final)
        {
            TimeOnly horaSiguiente = horaActual.AddMinutes(10);

            Console.WriteLine($"Creando slot {horaActual} - {horaSiguiente}");

            if (horaSiguiente > evento.Hora_Final)
            {
                horaSiguiente = evento.Hora_Final;
            }

            slots.Add(new Slot
            {
                id_evento = evento.Id,
                hora_inicio = horaActual,
                hora_final = horaSiguiente,
                estado = "Disponible"
            });

            horaActual = horaSiguiente;
        }

        return slots;
    }

    public async Task<List<EventoResponse>> ObtenerEventosAdministrador(int idAdministrador)
    {
        var total = await _context.Eventos.CountAsync();
        Console.WriteLine($"Total eventos: {total}");

        var eventosAdmin = await _context.Eventos
            .Where(e => e.Id_Administrador == idAdministrador)
            .CountAsync();

        Console.WriteLine($"Eventos del admin: {eventosAdmin}");

        return await _context.Eventos
            .Where(e => e.Id_Administrador == idAdministrador)
            .OrderBy(e => e.Fecha)
            .ThenBy(e => e.Hora_Inicio)
            .Select(e => new EventoResponse
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Descripcion = e.Descripcion,
                Fecha = e.Fecha,
                Hora_Inicio = e.Hora_Inicio,
                Hora_Final = e.Hora_Final,
                SlotsDisponibles = e.Slots.Count(s => s.estado == "Disponible")
            })
            .ToListAsync();
    }

    public async Task<List<EventoResponse>> ObtenerEventos()
    {
        Console.WriteLine("Eventos consultados");

        var total = await _context.Eventos.CountAsync();
        Console.WriteLine($"Eventos: {total}");

        // return await _context.Eventos
        //     .Select(e => new EventoResponse
        //     {
        //         Id = e.Id,
        //         Nombre = e.Nombre
        //     })
        //     .ToListAsync();

        return await _context.Eventos
        .OrderBy(e => e.Fecha)
        .ThenBy(e => e.Hora_Inicio)
        .Select(e => new EventoResponse
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Descripcion = e.Descripcion,
            Fecha = e.Fecha,
            Hora_Inicio = e.Hora_Inicio,
            Hora_Final = e.Hora_Final,
            SlotsDisponibles = e.Slots.Count(s => s.estado == "Disponible")
        })
        .ToListAsync();
    }

    public async Task<EventoDetallesResponse> ObtenerEventoPorId(int idEvento)
    {
        var evento = await _context.Eventos.Include(e => e.Slots).FirstOrDefaultAsync(e => e.Id == idEvento);

        if (evento == null)
        {
            return null;
        }

        return new EventoDetallesResponse
        {
          Id = evento.Id,
          Nombre = evento.Nombre,
          Descripcion = evento.Descripcion,
          Fecha = evento.Fecha,
          Hora_Inicio = evento.Hora_Inicio,
          Hora_Final = evento.Hora_Final,

          SlotsDisponibles = evento.Slots.Count(s => s.estado == "Disponible"),

          Slots = evento.Slots.OrderBy(s => s.hora_inicio)
            .Select(s => new SlotResponse
            {
                Id = s.id,
                Hora_Inicio = s.hora_inicio,
                Hora_Final = s.hora_final,
                Estado = s.estado
            })
            .ToList()
        };
    }
}