using MasteryProject.Core.DTOs;

namespace MasteryProject.Core.Interfaces
{
    public interface IHostRepository
    {
        List<Host> GetAllHosts();
        List<Host> GetHostsById(string id);
        List<Host> GetHostsByName(string name);
    }
}
