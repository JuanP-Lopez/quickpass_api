using Microsoft.EntityFrameworkCore;
using Quickpass.Api.Data;
using Quickpass.Api.DTOs;
using Quickpass.Api.Models;

namespace Quickpass.Api.Services;

public class SlotService : ISlotService
{
    private readonly QuickPassContext _context;

    public SlotService(QuickPassContext context)
    {
        _context = context;
    }

    public async Task<bool> ReservarSlot (int IdSlot, int IdUsuario )
    {
        var slot = await _context.Slots.FirstOrDefaultAsync(s => s.id == IdSlot);
        
        if (slot == null)
        {
            return false;
        }

        if (slot.estado != "Disponible")
        {
            return false;
        }

        bool yaTieneTurno = await _context.Slots.AnyAsync(s =>
            s.Id_Usuario == IdUsuario && s.id_evento == slot.id_evento
        );

        if (yaTieneTurno)
        {
            return false;
        }

        slot.Id_Usuario = IdUsuario;
        slot.estado = "En espera";

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<TurnoResponse>> ObtenerTurnosUsuario (int IdUsuario )
    {
        return await _context.Slots
            .Where(s => s.Id_Usuario == IdUsuario)
            .Include(s => s.Evento)
            .OrderBy(s => s.Evento.Fecha)
            .ThenBy(s => s.Evento.Hora_Inicio)
            .Select(s => new TurnoResponse
            {
                IdEvento = s.Evento.Id,
                NombreEvento = s.Evento.Nombre,
                Descripcion = s.Evento.Descripcion,
                Fecha = s.Evento.Fecha,
                Estado = s.estado
            })
            .ToListAsync();
    }

    public async Task<List<SolicitudTurnoResponse>> ObtenerSolicitudes(int idAdministrador)
    {
        return await _context.Slots
            .Include(s => s.Evento)
            .Include(s => s.usuario)
            .Where(
                s => s.Evento.Id_Administrador == idAdministrador && s.estado == "En espera"
            )
            .OrderBy(s => s.Evento.Fecha)
            .ThenBy(s => s.hora_inicio)
            .Select(s => new SolicitudTurnoResponse
            {
                IdSlot = s.id,
                IdEvento = s.Evento.Id,
                Evento = s.Evento.Nombre,
                Fecha = s.Evento.Fecha,
                Hora_Inicio = s.Evento.Hora_Inicio,
                Hora_Final =  s.Evento.Hora_Final,
                IdUsuario = s.usuario.Id,
                NombreUsuario = s.usuario.Nombre,
                ApellidoUsuario = s.usuario.Apellido,
                Correo = s.usuario.Correo
            }
            )
            .ToListAsync();
    }

    public async Task<bool> AceptarSlot (int IdSlot)
    {
        var slot = await _context.Slots.FirstOrDefaultAsync(
            s => s.id == IdSlot
        );

        if (slot == null)
        {
            return false;
        }

        slot.estado = "Asignado";

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RechazarSlot (int IdSlot)
    {
        var slot = await _context.Slots.FirstOrDefaultAsync(
            s => s.id == IdSlot
        );

        if (slot == null)
        {
            return false;
        }

        slot.estado = "Disponible";
        slot.Id_Usuario = null;

        await _context.SaveChangesAsync();

        return true;
    }
}