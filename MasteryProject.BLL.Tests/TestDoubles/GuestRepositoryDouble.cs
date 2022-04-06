using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MasteryProject.BLL.Tests.TestDoubles
{
    public class GuestRepositoryDouble : IGuestRepository
    {
        public readonly static Guest GUEST = MakeGuest();

        private readonly List<Guest> guests = new List<Guest>();

        public GuestRepositoryDouble()
        {
            guests.Add(GUEST);
        }
        public List<Guest> GetAllGuests()
        {
            return guests;
        }

        public Guest GetGuestsByEmail(string email)
        {
            return guests.FirstOrDefault(i => i.Email == email);
        }

        public Guest GetGuestsByID(int id)
        {
            return guests.FirstOrDefault(i => i.Id == id);
        }

        public List<Guest> GetGuestsByState(string stateAbbr)
        {
            return guests.Where(i => i.State.Equals(stateAbbr, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        private static Guest MakeGuest()
        {
            Guest guest = new Guest();
            guest.Id = 1;
            guest.FirstName = "Fakeperson";
            guest.LastName = "PhonyGuy";
            guest.Email = "fakeyp@notreal.gov";
            guest.PhoneNumber = "(999) 999 9999";
            guest.State = "AZ";
            return guest; 
        }
    }
}
