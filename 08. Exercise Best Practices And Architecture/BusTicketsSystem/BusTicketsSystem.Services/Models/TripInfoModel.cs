namespace BusTicketsSystem.Services.Models
{
    using System;

    public class TripInfoModel
    {
        public string BusStation { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string Status { get; set; }
    }
}