using MasteryProject.BLL;
using MasteryProject.Core.Exceptions;
using MasteryProject.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.UI
{
    public class Controller
    {
        private readonly ReservationService reservationService;
        private readonly View view;

        public Controller(ReservationService reservationService, View view)
        {
            this.reservationService = reservationService;
            this.view = view;
        }

        public void Run()
        {
            view.DisplayHeader("Welcome to Don't Wreck My House!");
            try
            {
                RunAppLoop();
            }
            catch (RepositoryException ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservationsforHost:
                        ViewByHost();
                        break;
                    case MainMenuOption.MakeAReservation:
                        AddReservation();
                        break;
                    case MainMenuOption.EditAReservation:
                        EditReservation();
                        break;
                    case MainMenuOption.CancelAReservation:
                        CancelReservation();
                        break;
                }
            } while (option != MainMenuOption.Exit);
        }

        // top level menu
        private void ViewByHost()
        {
            view.DisplayHeader(MainMenuOption.ViewReservationsforHost.ToLabel());
            Host host = GetHost();
            List<Reservation> reservations = reservationService.GetReservationByHostId(host.Id);
            view.DisplayReservations(reservations, host);
            view.EnterToContinue();
        }

        private void AddReservation()
        {
            view.DisplayHeader(MainMenuOption.MakeAReservation.ToLabel());
            Host host = GetHost();
            if (host == null)
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }
            var reservations = reservationService.GetReservationByHostId(host.Id).Where(x => x.StartDate > DateOnly.FromDateTime(DateTime.Now)).ToList();
            view.DisplayReservations(reservations, host);
            Reservation reservation = view.BuildReservation(host, guest);
            Result<Reservation> result = reservationService.MakeReservation(reservation);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                view.DisplaySummary(result.Data);
                bool status = view.ConfirmReservation();
                if (status)
                {
                    reservationService.AddReservation(result.Data);
                    string successMessage = $"Reservation {result.Data.ReservationId} created.";
                    view.DisplayStatus(true, successMessage);
                }
                else
                {
                    string errorMessage = $"Reservation {result.Data.ReservationId} not created.";
                    view.DisplayStatus(false, errorMessage);
                }
            }
        }
        private void EditReservation()
        {
            Host host = GetHost();
            if (host == null)
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }
            var reservations = reservationService.GetAllReservationsForSpecificHostAndGuest(host.Id, guest.Id).
                Where(x => x.StartDate > DateOnly.FromDateTime(DateTime.Now)).ToList(); 
            if (reservations.Count == 0)
            {
                view.DisplayStatus(false, "No reservations found.");
                return;
            }
            view.DisplayReservations(reservations, host);
            int reservationId = view.SelectReservation();
            Result<Reservation> reservationCheckResult = reservationService.GetReservationById(reservationId, reservations);
            if (!reservationCheckResult.Success)
            {
                view.DisplayStatus(false, reservationCheckResult.Messages);
            }
            else
            {
                Reservation reservation = view.EditReservation(reservationCheckResult.Data, host);
                Result<Reservation> result = reservationService.UpdateReservation(reservation);
                if (!result.Success)
                {
                    view.DisplayStatus(false, result.Messages);
                }
                else
                {
                    view.DisplaySummary(result.Data);
                    bool status = view.ConfirmReservation();
                    if (status)
                    {
                        reservationService.ReplaceReservation(result.Data);
                        string successMessage = $"Reservation {result.Data.ReservationId} updated.";
                        view.DisplayStatus(true, successMessage);
                    }
                    else
                    {
                        string errorMessage = $"Reservation {result.Data.ReservationId} not updated.";
                        view.DisplayStatus(false, errorMessage);
                    }
                }
            }
        }
        private void CancelReservation()
        {
            Host host = GetHost();
            if (host == null)
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }
            var reservations = reservationService.GetAllReservationsForSpecificHostAndGuest(
                host.Id, guest.Id).Where(x => x.StartDate > DateOnly.FromDateTime(DateTime.Now)).ToList();
            if (reservations.Count == 0)
            {
                view.DisplayStatus(false, "No reservations found.");
                return;
            }
            view.DisplayReservations(reservations, host);
            int reservationId = view.SelectReservation();
            Result<Reservation> reservationCheckResult = reservationService.GetReservationById(reservationId, reservations);
            if (!reservationCheckResult.Success)
            {
                view.DisplayStatus(false, reservationCheckResult.Messages);
            }
            else
            {
                Reservation reservation = reservationCheckResult.Data;
                Result<Reservation> result = reservationService.DeleteReservation(reservation);
                if (!result.Success)
                {
                    view.DisplayStatus(false, result.Messages);
                }
                else
                {
                    string successMessage = $"Reservation {result.Data.ReservationId} cancelled.";
                    view.DisplayStatus(true, successMessage);
                }
            }
        }

        // support methods
        private Host GetHost()
        {
            string emailPrefix = view.GetHostEmailPrefix();
            List<Host> hosts = reservationService.GetHostsByEmail(emailPrefix);
            return view.ChooseHost(hosts);
        }
        private Guest GetGuest()
        {
            string emailPrefix = view.GetGuestEmailPrefix();
            List<Guest> guests = reservationService.GetGuestsByEmail(emailPrefix);
            return view.ChooseGuest(guests);
        }
    }
}
