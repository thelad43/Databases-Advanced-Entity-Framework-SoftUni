namespace BusTicketsSystem.Services
{
    using Models;

    public interface IBusStationService
    {
        BusStationInfoModel BusStationInfoModel(int id);
    }
}