using MasteryProject.Core.DTOs;

namespace MasteryProject.Core.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservationsByHost(string hostId);
        Reservation AddReservation(Reservation reservation);
        Reservation UpdateReservation(Reservation reservation);
        Reservation DeleteReservation(Reservation reservation);
    }
}
