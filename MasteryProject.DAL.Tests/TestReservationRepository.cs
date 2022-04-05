using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace MasteryProject.DAL.Tests
{
    public class TestReservationRepository
    {
        const string SEED_FILE_PATH = @"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\SeedFile.csv";
        const string TEST_FILE_PATH =
            @"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\reservations_test_data\2e72f86c-b8fe-4265-b4f1-304dea8762db.csv";

        string DataDirectory { get; set; }

        IReservationRepository repo;

        const int RESERVATIONCOUNT = 12;

        string hostId = "2e72f86c-b8fe-4265-b4f1-304dea8762db";
        DateTime StartDate = new DateTime(2022, 01, 20);
        DateTime EndDate = new DateTime(2022, 01, 30);

        [SetUp]
        public void SetUp()
        {
            DataDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/data/reservations_test_data";
            File.Copy(SEED_FILE_PATH, TEST_FILE_PATH, true);
            repo = new ReservationRepository(DataDirectory);
        }
        [Test]
        public void VerifyTestDirectoryExistsOnCreation()
        {
            Assert.IsTrue(Directory.Exists(DataDirectory));
            Console.WriteLine(DataDirectory);
        }
        [Test]
        public void ShouldFindByHostId()
        {
            List<Reservation> reservations = repo.GetReservationsByHost(hostId);
            Assert.AreEqual(RESERVATIONCOUNT, reservations.Count);
        }
        [Test]
        public void ShouldAddReservationToReservationFile()
        {
            Reservation reservation = new Reservation();
            reservation.StartDate = StartDate.Date;
            reservation.EndDate = EndDate.Date;
            reservation.Cost = 2400M;

            Guest guest = new Guest();
            guest.Id = 50;
            reservation.Guest = guest;

            Host host = new Host();
            host.Id = hostId;
            reservation.Host = host;

            reservation = repo.AddReservation(reservation);

            List<Reservation> reservations = repo.GetReservationsByHost(hostId);

            Assert.AreEqual(RESERVATIONCOUNT + 1, reservations.Count);
        }
        [Test]
        public void ShouldMakeNewReservationFileForNewHost()
        {
            Reservation reservation = new Reservation();
            reservation.StartDate = StartDate.Date;
            reservation.EndDate = EndDate.Date;
            reservation.Cost = 2400M;

            Guest guest = new Guest();
            guest.Id = 50;
            reservation.Guest = guest;

            Host host = new Host();
            host.Id = "478472-428642hrj646-24w73";
            reservation.Host = host;

            reservation = repo.AddReservation(reservation);

            List<Reservation> reservations = repo.GetReservationsByHost(host.Id);

            Assert.AreEqual(1, reservations.Count);
        }
        [Test]
        public void ShouldUpdateExistingReservation()
        {
            Reservation reservation = new Reservation();
            reservation.StartDate = new DateTime(2022, 1, 5).Date;
            reservation.EndDate = new DateTime(2022, 1, 12).Date;
            reservation.Cost = 1500M;
            reservation.ReservationId = 12;

            Guest guest = new Guest();
            guest.Id = 735;
            reservation.Guest = guest;

            Host host = new Host();
            host.Id = hostId;
            reservation.Host = host;

            bool status = repo.UpdateReservation(reservation);
            List<Reservation> reservations = repo.GetReservationsByHost(hostId);
            Assert.IsTrue(status);
            Assert.AreEqual(reservations[11].Cost, 1500);

        }
        [Test]
        public void ShouldNotUpdateNonExistingReservation()
        {
            Reservation reservation = new Reservation();
            reservation.StartDate = new DateTime(2022, 1, 5).Date;
            reservation.EndDate = new DateTime(2022, 1, 12).Date;
            reservation.Cost = 1500M;
            reservation.ReservationId = 25;

            Guest guest = new Guest();
            guest.Id = 735;
            reservation.Guest = guest;

            Host host = new Host();
            host.Id = hostId;
            reservation.Host = host;

            bool status = repo.UpdateReservation(reservation);
            Assert.IsTrue(!status);
        }
        [Test]
        public void ShouldDeleteReservation()
        {
            Reservation reservation = new Reservation();
            reservation.StartDate = new DateTime(2022, 1, 5).Date;
            reservation.EndDate = new DateTime(2022, 1, 10).Date;
            reservation.Cost = 1100M;
            reservation.ReservationId = 12;

            Guest guest = new Guest();
            guest.Id = 735;
            reservation.Guest = guest;

            Host host = new Host();
            host.Id = hostId;
            reservation.Host = host;

            bool status = repo.DeleteReservation(reservation);
            List<Reservation> reservations = repo.GetReservationsByHost(hostId);
            Assert.IsTrue(status);
            Assert.AreEqual(reservations.Count, RESERVATIONCOUNT - 1);

        }
    }
    
}
