namespace BusTicketsSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BusTicketsSystem.Models;
    using BusTicketsSystem.Models.Enums;
    using Data;
    using System;
    using System.Linq;

    public class TripService : ITripService
    {
        private readonly BusTicketsSystemDbContext db;

        public TripService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public Trip ById(int id)
            => this.db
                .Trips
                .Where(t => t.Id == id)
                .ProjectTo<Trip>()
                .FirstOrDefault();

        public void ChangeStatus(int id, Status status)
        {
            var trip = ById(id);
            trip.Status = status;
            this.db.Update(trip);
            this.db.SaveChanges();
        }

        public void AddArrivedTrip(BusStation originBusStation, BusStation destinationBusStation, int passengersCount)
        {
            var trip = new ArrivedTrip
            {
                ActualArrivalTime = DateTime.Now,
                OriginBusStation = originBusStation,
                DestinationBusStation = destinationBusStation,
                PassengersCount = passengersCount
            };

            this.db.Add(trip);
            this.db.SaveChanges();
        }

        public TModel ById<TModel>(int id)
            => this.db
                .Trips
                .Where(t => t.Id == id)
                .ProjectTo<TModel>()
                .FirstOrDefault();
    }
}