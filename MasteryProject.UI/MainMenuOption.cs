using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.UI
{
    public enum MainMenuOption
    {
        Exit,
        ViewReservationsforHost,
        MakeAReservation,
        EditAReservation,
        CancelAReservation
    }
    
    public static class MainMenuOptionExtensions
    {
        public static string ToLabel(this MainMenuOption option) => option switch
        {
            MainMenuOption.Exit => "Exit",
            MainMenuOption.ViewReservationsforHost => "View Reservations For Host",
            MainMenuOption.MakeAReservation => "Make a Reservation",
            MainMenuOption.EditAReservation => "Edit a Reservation",
            MainMenuOption.CancelAReservation => "Cancel a Reservation",
            _ => throw new NotImplementedException()
        };
    }
}
