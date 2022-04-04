using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;

namespace MasteryProject.DAL
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string directory;

        public ReservationRepository(string directory)
        {
            this.directory = directory;
        }
        public Reservation AddReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation DeleteReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsByHost()
        {
            throw new NotImplementedException();
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
