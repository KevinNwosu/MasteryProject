using MasteryProject.Core.DTOs;
using MasteryProject.Core.Exceptions;
using MasteryProject.Core.Interfaces;

namespace MasteryProject.DAL
{
    public class ReservationRepository : IReservationRepository
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string directory;

        public ReservationRepository(string directory)
        {
            this.directory = directory;
        }
        public Reservation AddReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Reservation DeleteReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetReservationsByHost(string hostId)
        {
            var reservations = new List<Reservation>();
            var path = GetFilePath(hostId);
            if (!File.Exists(path))
            {
                return reservations;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read forages", ex);
            }


            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields, hostId);
                if (reservation != null)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
        private string GetFilePath(string hostId)
        {
            return Path.Combine(directory, $"{hostId}.csv");
        }
        private Reservation Deserialize(string[] fields, string hostId)
        {
            if (fields.Length != 5)
            {
                return null;
            }

            Reservation result = new Reservation();
            result.ReservationId = int.Parse(fields[0]);
            result.StartDate = DateTime.Parse(fields[1]);
            result.EndDate = DateTime.Parse(fields[2]);
            result.Cost = decimal.Parse(fields[4]);

            Host host = new Host();
            host.Id = hostId;
            result.Host = host;

            Guest guest = new Guest();
            guest.Id = int.Parse(fields[3]);
            result.Guest = guest;
            return result;
        }
    }
}
