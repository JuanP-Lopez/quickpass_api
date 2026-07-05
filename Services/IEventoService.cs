using Quickpass.Api.DTOs;
using Quickpass.Api.Models;

namespace Quickpass.Api.Services;

public interface IEventoService
{
    Task<Evento> CrearEventoAsync(EventoRequest request);

    Task<List<EventoResponse>> ObtenerEventosAdministrador(int id);
}