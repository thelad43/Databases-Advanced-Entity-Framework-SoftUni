namespace BusTicketsSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System.Linq;

    public class BusStationService : IBusStationService
    {
        private readonly BusTicketsSystemDbContext db;

        public BusStationService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public BusStationInfoModel BusStationInfoModel(int id)
            => this.db
                .BusStations
                .Where(s => s.Id == id)
                .ProjectTo<BusStationInfoModel>()
                .FirstOrDefault();
    }
}