using MasteryProject.Core.DTOs;
using MasteryProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.BLL.Tests.TestDoubles
{
    public class HostRepositoryDouble : IHostRepository
    {
        public readonly static Host HOST = MakeHost();

        private readonly List<Host> hosts = new List<Host>();
        public HostRepositoryDouble()
        {
            hosts.Add(HOST);
        }
        public List<Host> GetAllHosts()
        {
            return hosts;
        }

        public Host GetHostsByEmail(string email)
        {
            return hosts.FirstOrDefault(i => i.Email == email);
        }

        public Host GetHostsById(string id)
        {
            return hosts.FirstOrDefault(i => i.Id == id);
        }

        public List<Host> GetHostsByState(string stateAbbr)
        {
            return hosts.Where(i => i.State.Equals(stateAbbr, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        private static Host MakeHost()
        {
            Host host = new Host();
            host.Id = "kdsccd7cdcd-sf7dsdbdd-fbdfhnf";
            host.LastName = "Fakehosty";
            host.Email = "fostyeyh@notreal.gov";
            host.PhoneNumber = "(989) 989 9889";
            host.Address = "555 Not A Real Street";
            host.City = "NotCity";
            host.State = "TX";
            host.PostalCode = 12345;
            host.RegRate = 200M;
            host.WeekendRate = 250M;
            return host;
        }
    }
}
