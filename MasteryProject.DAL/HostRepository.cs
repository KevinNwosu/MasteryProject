using MasteryProject.Core.DTOs;
using MasteryProject.Core.Exceptions;
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
        private readonly string filePath;

        public HostRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public List<Host> GetAllHosts()
        {
            var hosts = new List<Host>();
            if (!File.Exists(filePath))
            {
                return hosts;
            }
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read guests", ex);
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = Deserialize(fields);
                if (host != null)
                {
                    hosts.Add(host);
                }
            }
            return hosts;
        }
        public Host GetHostsById(string id)
        {
            var host = new Host();
            try
            {
                host = GetAllHosts().FirstOrDefault(i => i.Id == id);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("No Guest with that Id", ex);
            }
            return host;
        }
        public List<Host> GetHostsByState(string stateAbbr)
        {
            return GetAllHosts().Where(i => i.State == stateAbbr).ToList();
        }
        public Host GetHostsByEmail(string email)
        {
            var hosts = GetAllHosts();
            return hosts.FirstOrDefault(g => g.Email == email);
        }
        public Host Deserialize(string[] fields)
        {
            if (fields.Length != 10)
            {
                return null;
            }

            Host result = new Host();
            result.Id = fields[0];
            result.LastName = fields[1];
            result.Email = fields[2];
            result.PhoneNumber = fields[3];
            result.Address = fields[4];
            result.City = fields[5];
            result.State = fields[6];
            result.PostalCode = int.Parse(fields[7]);
            result.RegRate = decimal.Parse(fields[8]);
            result.WeekendRate = decimal.Parse(fields[9]);
            return result;
        }
    }
}
