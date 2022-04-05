using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.DAL
{
    public class GuestRepository : IGuestRepository
    {
        public List<Guest> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetGuestsByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Guest> GetGuestsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
