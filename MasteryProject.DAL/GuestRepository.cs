using MasteryProject.Core.DTOs;
using MasteryProject.Core.Exceptions;
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
        private readonly string filePath;

        public GuestRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public List<Guest> GetAllGuests()
        {
            var guests = new List<Guest>();
            if(!File.Exists(filePath))
            {
                return guests;
            }
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch(IOException ex)
            {
                throw new RepositoryException("could not read guests", ex);
            }

            for(int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);
                if(guest != null)
                {
                    guests.Add(guest);
                }
            }
            return guests;
        }

        public Guest GetGuestsByID(int id)
        {
            var guest = new Guest();
            try
            {
                guest = GetAllGuests().FirstOrDefault(i => i.Id == id);
            }
            catch(IOException ex)
            {
                throw new RepositoryException("No Guest with that Id", ex);
            }
            return guest;
        }

        public List<Guest> GetGuestsByState(string stateAbbr)
        {
            return GetAllGuests().Where(i => i.State == stateAbbr).ToList();
        }
        public Guest GetGuestsByEmail(string email)
        {
            var guests = GetAllGuests();
            return guests.FirstOrDefault(g => g.Email == email);
        }
        public Guest Deserialize(string[] fields)
        {
            if(fields.Length != 6)
            {
                return null;
            }

            Guest result = new Guest();
            result.Id = int.Parse(fields[0]);
            result.FirstName = fields[1];
            result.LastName = fields[2];
            result.Email = fields[3];
            result.PhoneNumber = fields[4];
            result.State = fields[5];
            return result;
        }
    }
}
