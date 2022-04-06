namespace MasteryProject.Core.DTOs
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public Guest Guest { get; set; }
        public Host Host { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal Cost { get; set; }
        public decimal GetCost()
        {
            decimal weekendPrice = 0M;
            decimal weekdayPrice = 0M;
            for (var day = StartDate; day <= EndDate; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                {
                    weekendPrice = weekendPrice + Host.WeekendRate;
                }
                else
                {
                    weekdayPrice = weekdayPrice + Host.RegRate;
                }
            }
            decimal cost = weekdayPrice + weekendPrice;
            return cost;
        }
    }
}
