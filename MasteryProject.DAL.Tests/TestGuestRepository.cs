using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasteryProject.Core.DTOs;
using NUnit.Framework;

namespace MasteryProject.DAL.Tests
{
    public class TestGuestRepository
    {
        [Test]
        public void ShouldGetAllGuests()
        {
            GuestRepository repo = new GuestRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\guests.csv");
            List<Guest> all = repo.GetAllGuests();
            Assert.AreEqual(1000, all.Count);
        }
        [Test]
        public void ShouldGetGuestsInVT()
        {
            GuestRepository repo = new GuestRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\guests.csv");
            List<Guest> all = repo.GetGuestsByState("VT");
            Assert.AreEqual(1, all.Count);
        }
        [Test]
        public void ShouldGetGuestsInTX()
        {
            GuestRepository repo = new GuestRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\guests.csv");
            List<Guest> all = repo.GetGuestsByState("TX");
            Assert.AreEqual(107, all.Count);
        }
        [Test]
        public void ShouldFindByID()
        {
            GuestRepository repo = new GuestRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\guests.csv");
            Guest guest = repo.GetGuestsByID(96);
            Assert.AreEqual("CA", guest.State);
        }
    }
}
