namespace BusTicketsSystem.App.Core.Commands
{
    using Interfaces;
    using Services;
    using System.Text;

    using static Common.GlobalConstants;

    public class PrintBusStationInfoCommand : ICommand
    {
        private readonly IBusStationService busTickets;

        public PrintBusStationInfoCommand(IBusStationService busTickets)
        {
            this.busTickets = busTickets;
        }

        // PrintBusStationInfo {Id}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);

            var busStationInfoModel = this.busTickets.BusStationInfoModel(id);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{busStationInfoModel.Name}, {busStationInfoModel.Town}");
            stringBuilder.AppendLine("Arrivals:");

            foreach (var trip in busStationInfoModel.ArrivingTrips)
            {
                stringBuilder.AppendLine($"From {trip.BusStation} | Arrive at: {trip.ArrivalTime.ToString(DateFormat)} | Status: {trip.Status}");
            }

            stringBuilder.AppendLine("Departures:");

            foreach (var trip in busStationInfoModel.DeparturesTrips)
            {
                stringBuilder.AppendLine($"To {trip.BusStation} | Arrive at: {trip.DepartureTime.ToString(DateFormat)} | Status: {trip.Status}");
            }

            return stringBuilder.ToString();
        }
    }
}