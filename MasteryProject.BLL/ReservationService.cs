using MasteryProject.Core.Interfaces;
using MasteryProject.Core.DTOs;

namespace MasteryProject.BLL
{
    public class ReservationService 
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IHostRepository hostRepository;
        private readonly IGuestRepository guestRepository;

        public ReservationService(IReservationRepository reservationRepository, IHostRepository hostRepository, IGuestRepository guestRepository)
        {
            this.reservationRepository = reservationRepository;
            this.hostRepository = hostRepository;
            this.guestRepository = guestRepository;
        }
        public List<Reservation> GetReservationByHostId(string hostId)
        {
            Dictionary<string, Host> hostMap = hostRepository.GetAllHosts().ToDictionary(i => i.Id);
            Dictionary<int, Guest> guestMap = guestRepository.GetAllGuests().ToDictionary(i => i.Id);

            List<Reservation> reservationList = reservationRepository.GetReservationsByHost(hostId);
            foreach(var reservation in reservationList)
            {
                reservation.Host = hostMap[reservation.Host.Id];
                reservation.Guest = guestMap[reservation.Guest.Id];
            }

            return reservationList;
        }
        public Result<Reservation> MakeReservation(Reservation reservation)
        {
            Result<Reservation> result = Validate(reservation);
            List<Reservation> reservations = reservationRepository.GetReservationsByHost(reservation.Host.Id);

            var currentReservations = reservations.FirstOrDefault(
                x => (x.StartDate < reservation.EndDate && x.EndDate > reservation.EndDate)
                ||(x.StartDate < reservation.StartDate && x.EndDate > reservation.StartDate));

            if (currentReservations != null)
            {
                result.AddMessage($"Reservation overlaps.");
                return result;
            }
            reservation.Cost = reservation.GetCost();
            result.Data = reservationRepository.AddReservation(reservation);
            
            return result;
        }
        private Result<Reservation> Validate(Reservation reservation)
        {
            Result<Reservation> result = ValidateNulls(reservation);
            if (!result.Success)
            {
                return result;
            }

            ValidateFields(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateChildrenExist(reservation, result);

            return result;
        }

        private Result<Reservation> ValidateNulls(Reservation reservation)
        {
            var result = new Result<Reservation>();

            if (reservation == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if (reservation.Host == null)
            {
                result.AddMessage("Host is required.");
            }

            if (reservation.Guest == null)
            {
                result.AddMessage("Guest is required.");
            }

            return result;
        }

        private void ValidateFields(Reservation reservation, Result<Reservation> result)
        {
            // No past dates.
            if (reservation.StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                result.AddMessage("Reservation start date cannot be in the past.");
            }

            if (reservation.EndDate < reservation.StartDate)
            {
                result.AddMessage("Reservation end date cannot be before the start date");
            }
        }

        private void ValidateChildrenExist(Reservation reservation, Result<Reservation> result)
        {
            if (reservation.Host.Id == null
                    || hostRepository.GetHostsById(reservation.Host.Id) == null)
            {
                result.AddMessage("Forager does not exist.");
            }

            if (reservation.Guest.Id == null || guestRepository.GetGuestsByID(reservation.Guest.Id) == null)
            {
                result.AddMessage("Guest does not exist.");
            }
        }
    }
}
