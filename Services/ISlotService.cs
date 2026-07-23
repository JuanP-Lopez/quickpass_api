using Quickpass.Api.DTOs;
using Quickpass.Api.Models;

namespace Quickpass.Api.Services;

public interface ISlotService
{
    Task<bool> ReservarSlot(int idSlot, int idUsuario);

    Task <List<TurnoResponse>> ObtenerTurnosUsuario (int idUsuario);

    Task<List<SolicitudTurnoResponse>> ObtenerSolicitudes (int idAdministrador);

}