using MasteryProject.Core.DTOs;

namespace MasteryProject.Core.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservationsByHost(string hostId);
        Reservation AddReservation(Reservation reservation);
        bool UpdateReservation(Reservation reservation);
        bool DeleteReservation(Reservation reservation);
    }
}
