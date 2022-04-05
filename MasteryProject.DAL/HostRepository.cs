using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.DAL
{
    public class HostRepository : IHostRepository
    {
        public List<Host> GetAllHosts()
        {
            throw new NotImplementedException();
        }
        public List<Host> GetHostsById(string id)
        {
            throw new NotImplementedException();
        }
        public List<Host> GetHostsByName(string name)
        {
            throw new NotSupportedException();
        }
    }
}
