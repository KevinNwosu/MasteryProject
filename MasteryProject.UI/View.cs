using MasteryProject.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.UI
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for (int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                io.PrintLine($"{i}. {option.ToLabel()}");
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max}]: ";
            return options[io.ReadInt(message, min, max)];
        }
        public DateOnly GetReservationStartDate()
        {
            string message = "Enter reservation start date (MM/DD/YYYY): ";
            return io.ReadDate(message);
        }
        public DateOnly GetReservationEndDate()
        {
            string message = "Enter reservation end date (MM/DD/YYYY): ";
            return io.ReadDate(message);
        }

        public string GetHostEmailPrefix()
        {
            return io.ReadRequiredString("Host Email starts with: ");
        }

        public string GetGuestEmailPrefix()
        {
            return io.ReadRequiredString("Guest Email starts with: ");
        }

        public Host ChooseHost(List<Host> hosts)
        {
            if (hosts == null || hosts.Count == 0)
            {
                io.PrintLine("No hosts found");
                return null;
            }

            int index = 1;
            foreach (Host host in hosts.Take(25))
            {
                io.PrintLine($"{index++}: {host.Email} {host.State} {host.City} {host.RegRate} {host.WeekendRate}");
            }
            index--;
            
            if (hosts.Count > 25)
            {
                io.PrintLine("More than 25 hosts found. Showing first 25. Please refine your search.");
            }
            io.PrintLine("0: Exit");
            string message = $"Select a host by their index [0-{index}]: ";

            index = io.ReadInt(message, 0, index);
            if (index <= 0)
            {
                return null;
            }
            return hosts[index - 1];
        }
        public Guest ChooseGuest(List<Guest> guests)
        {
            if (guests == null || guests.Count == 0)
            {
                io.PrintLine("No guests found");
                return null;
            }

            int index = 1;
            foreach (Guest guest in guests.Take(25))
            {
                io.PrintLine($"{index++}: {guest.Id} {guest.Email} {guest.State}");
            }
            index--;

            if (guests.Count > 25)
            {
                io.PrintLine("More than 25 guests found. Showing first 25. Please refine your search.");
            }
            io.PrintLine("0: Exit");
            string message = $"Select a guest by their index [0-{index}]: ";

            index = io.ReadInt(message, 0, index);
            if (index <= 0)
            {
                return null;
            }
            return guests[index - 1];
        }

        public Reservation BuildReservation(Host host, Guest guest)
        {
            Reservation reservation = new();
            reservation.Host = host;
            reservation.Guest = guest;
            reservation.StartDate = io.ReadDate("Start date [mm/dd/yyyy]: ");
            reservation.EndDate = io.ReadDate("End date [mm/dd/yyyy]: ");

            return reservation;
        }
        public Reservation EditReservation(Reservation reservation, Host host)
        {
            reservation.Host = host;
            string startDate = io.ReadString($"Start date [mm/dd/yyyy] {reservation.StartDate}: ");
            string endDate = io.ReadString($"End date [mm/dd/yyyy] {reservation.EndDate}: ");
            if (!string.IsNullOrEmpty(startDate))
            {
                reservation.StartDate = DateOnly.Parse(startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                reservation.EndDate = DateOnly.Parse(endDate);
            }
            return reservation;
        }
        public string GetState()
        {
            return io.ReadRequiredString("State: ");
        }

        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }
        public bool ConfirmReservation()
        {
            return io.ReadBool("Is this okay? [Y/N]: ");
        }

        // display only
        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }

        internal int SelectReservation()
        {
            return io.ReadInt("Select reservation: ");
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }

        public void DisplayStatus(bool success, string message)
        {
            DisplayStatus(success, new List<string>() { message });
        }

        public void DisplayStatus(bool success, List<string> messages)
        {
            DisplayHeader(success ? "Success" : "Error");
            foreach (string message in messages)
            {
                io.PrintLine(message);
            }
        }

        public void DisplayReservations(List<Reservation> reservations, Host host)
        {
            DisplayHeader("Reservations");
            if (reservations == null || reservations.Count == 0)
            {
                io.PrintLine("No reservations found");
                return;
            }
            var orderedReservations = reservations.OrderBy(r => r.StartDate);
            DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            foreach (Reservation reservation in orderedReservations)
            {
                io.PrintLine(
                    string.Format("{0} {1} to {2} {3} Cost: ${4:0.00}",
                    reservation.ReservationId,
                    reservation.StartDate.ToString("MM/dd/yyyy"),
                    reservation.EndDate.ToString("MM/dd/yyyy"),
                    reservation.Guest.Email,
                    reservation.Cost));
            }
        }
        public void DisplayGuests(List<Guest> guests)
        {
            DisplayHeader("Guests");
            if (guests == null || guests.Count == 0)
            {
                io.PrintLine("No guests found");
                return;
            }
            foreach (Guest guest in guests)
            {
                io.PrintLine(
                    string.Format("{0} {1} {2}",
                    guest.Id,
                    guest.Email,
                    guest.State));
            }
        }
        public void DisplayHosts(List<Host> hosts)
        {
            DisplayHeader("Hosts");
            if (hosts == null || hosts.Count == 0)
            {
                io.PrintLine("No hosts found");
                return;
            }
            foreach (Host host in hosts)
            {
                io.PrintLine(
                    string.Format("{0} {1} {2} ${3:0.00} ${4:0.00}",
                    host.Email,
                    host.State,
                    host.City,
                    host.RegRate,
                    host.WeekendRate));
            }
        }
        public void DisplaySummary(Reservation reservation)
        {
            DisplayHeader("Reservation Summary");
            io.PrintLine($"Start Date: {reservation.StartDate.ToString("MM/dd/yyyy")}");
            io.PrintLine($"End Date: {reservation.EndDate.ToString("MM/dd/yyyy")}");
            io.PrintLine($"Cost: ${reservation.Cost:0.00}");
        }
    }
}
