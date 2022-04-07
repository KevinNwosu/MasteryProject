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
        DateOnly StartDate = new DateOnly(2022, 6, 11);
        DateOnly EndDate = new DateOnly(2022, 6, 26);
        DateOnly StartDate1 = new DateOnly(2022, 7, 11);
        DateOnly EndDate1 = new DateOnly(2022, 7, 26);
        

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

            Reservation reservation1 = new Reservation();
            reservation1.ReservationId = 2;
            reservation1.Guest = GuestRepositoryDouble.GUEST;
            reservation1.Host = HostRepositoryDouble.HOST;
            reservation1.StartDate = StartDate1;
            reservation1.EndDate = EndDate1;
            reservation1.Cost = 3400M;
            reservations.Add(reservation1);


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
            var all = reservations;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ReservationId == reservation.ReservationId)
                {
                    all.Remove(all[i]);
                    return true;
                }
            }
            return false;
        }

        public List<Reservation> GetReservationsByHost(string hostId)
        {
            return reservations.Where(i => i.Host.Id == hostId).ToList();
        }
        public bool UpdateReservation(Reservation reservation)
        {
            var all = reservations;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ReservationId == reservation.ReservationId)
                {
                    all[i] = reservation;
                    return true;
                }
            }
            return false;
        }
    }
}
