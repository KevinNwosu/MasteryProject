using MasteryProject.Core.DTOs;
using NUnit.Framework;
using System.Collections.Generic;

namespace MasteryProject.DAL.Tests
{
    public class TestHostRepository
    {
        [Test]
        public void ShouldGetAllHosts()
        {
            HostRepository repo = new HostRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\hosts.csv");
            List<Host> all = repo.GetAllHosts();
            Assert.AreEqual(1000, all.Count);
        }
        [Test]
        public void ShouldGetHostsInVT()
        {
            HostRepository repo = new HostRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\hosts.csv");
            List<Host> all = repo.GetHostsByState("VT");
            Assert.AreEqual(1, all.Count);
        }
        [Test]
        public void ShouldGetHostsInTX()
        {
            HostRepository repo = new HostRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\hosts.csv");
            List<Host> all = repo.GetHostsByState("TX");
            Assert.AreEqual(103, all.Count);
        }
        [Test]
        public void ShouldFindByID()
        {
            HostRepository repo = new HostRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\hosts.csv");
            Host host = repo.GetHostsById("3edda6bc-ab95-49a8-8962-d50b53f84b15");
            Assert.AreEqual("TX", host.State);
        }
        [Test]
        public void ShouldFindByEmail()
        {
            HostRepository repo = new HostRepository(@"C:\Users\Chetanna\Code\MasteryProject\MasteryProject.DAL.Tests\data\hosts.csv");
            Host host = repo.GetHostsByEmail("vgaskenb@php.net");
            Assert.AreEqual("Indianapolis", host.City);
        }
    }
}
