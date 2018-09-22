namespace BusTicketsSystem.Services.Models
{
    using System;

    public class TripTownsDepartureTimeModel
    {
        public string OriginBusStationTownName { get; set; }

        public string DestinationBusStationTownName { get; set; }

        public DateTime DepartureTime { get; set; }
    }
}