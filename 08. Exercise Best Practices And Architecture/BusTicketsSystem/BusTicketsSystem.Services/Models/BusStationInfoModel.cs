namespace BusTicketsSystem.Services.Models
{
    using System.Collections.Generic;

    public class BusStationInfoModel
    {
        public string Name { get; set; }

        public string Town { get; set; }

        public List<TripInfoModel> DeparturesTrips { get; set; } = new List<TripInfoModel>();

        public List<TripInfoModel> ArrivingTrips { get; set; } = new List<TripInfoModel>();
    }
}