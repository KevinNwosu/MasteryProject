using MasteryProject.Core.DTOs;

namespace MasteryProject.Core.Interfaces
{
    public interface IGuestRepository
    {
        List<Guest> GetAllGuests();
        List<Guest> GetGuestsByID(int id);
        List<Guest> GetGuestsByName(string name);

    }
}
