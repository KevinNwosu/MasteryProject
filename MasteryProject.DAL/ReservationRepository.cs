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
            List<Reservation> all = GetReservationsByHost(reservation.Host.Id);
            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.ReservationId)) + 1;
            reservation.ReservationId = nextId;
            all.Add(reservation);
            Write(all, reservation.Host.Id);
            return reservation;
        }

        public bool DeleteReservation(Reservation reservation)
        {
            List<Reservation> all = GetReservationsByHost(reservation.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ReservationId == reservation.ReservationId)
                {
                    all.Remove(all[i]);
                    Write(all, reservation.Host.Id);
                    return true;
                }
            }
            return false;
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
                throw new RepositoryException("could not read reservations", ex);
            }


            for (int i = 1; i < lines.Length; i++) 
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

        public bool UpdateReservation(Reservation reservation)
        {
            List<Reservation> all = GetReservationsByHost(reservation.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].ReservationId == reservation.ReservationId)
                {
                    all[i] = reservation;
                    Write(all, reservation.Host.Id);
                    return true;
                }
            }
            return false;
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
            result.StartDate = DateOnly.Parse(fields[1]);
            result.EndDate = DateOnly.Parse(fields[2]);
            result.Cost = decimal.Parse(fields[4]);

            Host host = new Host();
            host.Id = hostId;
            result.Host = host;

            Guest guest = new Guest();
            guest.Id = int.Parse(fields[3]);
            result.Guest = guest;
            return result;
        }
        private string Serialize(Reservation item)
        {
            return string.Format("{0},{1},{2},{3},{4}",
                    item.ReservationId,
                    item.StartDate,
                    item.EndDate,
                    item.Guest.Id,
                    item.Cost);
        }
        private void Write(List<Reservation> reservations, string hostId)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(hostId));
                writer.WriteLine(HEADER);

                if (reservations == null)
                {
                    return;
                }

                foreach (var reservation in reservations)
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not write reservations", ex);
            }
        }
    }
}
