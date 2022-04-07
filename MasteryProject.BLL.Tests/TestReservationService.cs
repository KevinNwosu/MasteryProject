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
            reservation.StartDate = new DateOnly(2022, 05, 14);
            reservation.EndDate = new DateOnly(2022, 05, 30);

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
            Assert.NotNull(result.Data);
            Assert.AreEqual(2, reservations.Count);
            Assert.AreEqual(2, reservation.ReservationId);
        }
        [Test]
        public void ShouldNotUpdate()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 05, 14);
            reservation.EndDate = new DateOnly(2022, 05, 30);
            reservation.Cost = ReservationRepositoryDouble.COST;

            Result<Reservation> result = service.UpdateReservation(reservation);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual(ReservationRepositoryDouble.COST, result.Data.Cost);
        }
    }
