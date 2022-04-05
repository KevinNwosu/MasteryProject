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
        string DataDirectory { get; set; }

        IReservationRepository repo;

        const int RESERVATIONCOUNT = 12;

        string hostId = "2e72f86c-b8fe-4265-b4f1-304dea8762db";

        [SetUp]
        public void SetUp()
        {
            DataDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/data/reservations_test_data";
           
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
    }
    
}
