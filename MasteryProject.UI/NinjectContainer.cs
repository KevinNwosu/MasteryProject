using MasteryProject.BLL;
using MasteryProject.Core.Interfaces;
using MasteryProject.DAL;
using Ninject;

namespace MasteryProject.UI
{
    public class NinjectContainer
    {
        public static StandardKernel kernel { get; set; }
        public static void Configure()
        {
            kernel = new StandardKernel();

            kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            kernel.Bind<View>().To<View>();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationDirectory = Path.Combine(projectDirectory, "data", "reservations");
            string guestFilePath = Path.Combine(projectDirectory, "data", "guests.csv");
            string hostFilePath = Path.Combine(projectDirectory, "data", "hosts.csv");

            kernel.Bind<IReservationRepository>().To<ReservationRepository>().WithConstructorArgument(reservationDirectory);
            kernel.Bind<IGuestRepository>().To<GuestRepository>().WithConstructorArgument(guestFilePath);
            kernel.Bind<IHostRepository>().To<HostRepository>().WithConstructorArgument(hostFilePath);

            kernel.Bind<ReservationService>().To<ReservationService>();
        }
    }
}
