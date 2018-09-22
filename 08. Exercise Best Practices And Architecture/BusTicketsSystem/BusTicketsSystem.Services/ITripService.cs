namespace BusTicketsSystem.Services
{
    using BusTicketsSystem.Models;
    using BusTicketsSystem.Models.Enums;

    public interface ITripService
    {
        Trip ById(int id);

        TModel ById<TModel>(int id);

        void ChangeStatus(int id, Status status);

        void AddArrivedTrip(BusStation originBusStation, BusStation destinationBusStation, int passengersCount);
    }
}