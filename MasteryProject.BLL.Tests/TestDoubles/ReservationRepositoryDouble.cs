using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.BLL.Tests.TestDoubles
{
    public class ReservationRepositoryDouble : IReservationRepository
    {   
        private readonly List<Reservation> reservations = new List<Reservation>();
        public const decimal COST = 3700M;
        DateOnly StartDate = new DateOnly(2022, 1, 15);
        DateOnly EndDate = new DateOnly(2022, 1, 30);

        public ReservationRepositoryDouble()
        {
            Reservation reservation = new Reservation();
            reservation.ReservationId = 1;
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = StartDate;
            reservation.EndDate = EndDate;
            reservation.Cost = 3500M;
            reservations.Add(reservation);
        }
        public Reservation AddReservation(Reservation reservation)
        {
            List<Reservation> all = GetReservationsByHost(reservation.Host.Id);
            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.ReservationId)) + 1;
            reservation.ReservationId = nextId;
            reservations.Add(reservation);
            return reservation;
        }

        public bool DeleteReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsByHost(string hostId)
        {
            return reservations.Where(i => i.Host.Id == hostId).ToList();
        }

        public bool UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
