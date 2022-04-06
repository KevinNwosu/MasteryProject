using MasteryProject.Core.DTOs;

namespace MasteryProject.Core.Interfaces
{
    public interface IHostRepository
    {
        List<Host> GetAllHosts();
        Host GetHostsById(string id);
        List<Host> GetHostsByState(string stateAbbr);
        Host GetHostsByEmail(string email);
    }
}
