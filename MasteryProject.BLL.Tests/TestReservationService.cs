using MasteryProject.BLL.Tests.TestDoubles;
using MasteryProject.Core.DTOs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.BLL.Tests
{
    public class TestReservationService
    {
        ReservationService service = new ReservationService(
            new ReservationRepositoryDouble(),
            new HostRepositoryDouble(),
            new GuestRepositoryDouble());

        [Test]
        public void ShouldMakeWithCorrectCost()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 04, 16);
            reservation.EndDate = new DateOnly(2022, 05, 02);

            Result<Reservation> result = service.MakeReservation(reservation);

            Assert.IsTrue(result.Success);
            Assert.NotNull(result.Data);
            Assert.AreEqual(ReservationRepositoryDouble.COST, result.Data.Cost);
        }
        [Test]
        public void ShouldAdd()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 05, 14);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.Cost = ReservationRepositoryDouble.COST;

            Result<Reservation> result = service.AddReservation(reservation);
            List<Reservation> reservations = service.GetReservationByHostId(HostRepositoryDouble.HOST.Id);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(ReservationRepositoryDouble.COST, reservations[2].Cost);
            Assert.AreEqual(3, reservations.Count);
            Assert.AreEqual(3, reservation.ReservationId);
        }
        [Test]
        public void ShouldNotMake()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 01, 14);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.Cost = ReservationRepositoryDouble.COST;

            Result<Reservation> result = service.MakeReservation(reservation);

            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ShouldNotUpdate()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 01, 14);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.ReservationId = 1;

            Result<Reservation> result = service.UpdateReservation(reservation);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
        }
        [Test]
        public void ShouldUpdate()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 05, 16);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.ReservationId = 3;

            Result<Reservation> result = service.UpdateReservation(reservation);

            Assert.IsTrue(result.Success);
            Console.WriteLine(result.Messages);
            Assert.AreEqual(3200, result.Data.Cost);
        }
        [Test]
        public void ShouldReplace()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 05, 16);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.Cost = 3200M;
            reservation.ReservationId = 3;

            Result<Reservation> result = service.ReplaceReservation(reservation);
            List<Reservation> reservations = service.GetReservationByHostId(HostRepositoryDouble.HOST.Id);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(3, reservations.Count);
            Assert.AreEqual(reservation.Cost, reservations[2].Cost);
        }
        [Test]
        public void ShouldNotReplace()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 05, 16);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.Cost = 3200M;
            reservation.ReservationId = 5;

            Result<Reservation> result = service.ReplaceReservation(reservation);
            List<Reservation> reservations = service.GetReservationByHostId(HostRepositoryDouble.HOST.Id);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ReservationRepositoryDouble.COST, reservations[2].Cost);
        }
    }
}
