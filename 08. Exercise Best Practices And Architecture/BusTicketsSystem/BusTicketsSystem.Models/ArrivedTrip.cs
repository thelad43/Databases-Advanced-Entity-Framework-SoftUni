namespace BusTicketsSystem.Models
{
    using System;

    public class ArrivedTrip
    {
        public int Id { get; set; }

        public DateTime ActualArrivalTime { get; set; }

        public BusStation OriginBusStation { get; set; }

        public BusStation DestinationBusStation { get; set; }

        public int PassengersCount { get; set; }
    }
}