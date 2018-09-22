namespace BusTicketsSystem.Models
{
    using Enums;
    using System;

    public class Trip
    {
        public int Id { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public Status Status { get; set; }

        public int OriginBusStationId { get; set; }

        public BusStation OriginBusStation { get; set; }

        public int DestinationStationId { get; set; }

        public BusStation DestinationBusStation { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}