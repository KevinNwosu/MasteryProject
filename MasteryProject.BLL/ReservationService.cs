using MasteryProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasteryProject.BLL
{
    public class ReservationService 
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IHostRepository hostRepository;
        private readonly IGuestRepository guestRepository;

        public ReservationService(IReservationRepository reservationRepository, IHostRepository hostRepository, IGuestRepository guestRepository)
        {
            this.reservationRepository = reservationRepository;
            this.hostRepository = hostRepository;
            this.guestRepository = guestRepository;
        }
    }
}
