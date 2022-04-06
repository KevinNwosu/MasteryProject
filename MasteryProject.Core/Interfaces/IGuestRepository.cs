using MasteryProject.Core.DTOs;

namespace MasteryProject.Core.Interfaces
{
    public interface IGuestRepository
    {
        List<Guest> GetAllGuests();
        Guest GetGuestsByID(int id);
        List<Guest> GetGuestsByState(string stateAbbr);
        Guest GetGuestsByEmail(string email);

    }
}
