namespace BusTicketsSystem.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using Models.Enums;
    using Services;
    using Services.Models;
    using System;
    using System.Linq;
    using System.Text;

    using static Common.ExceptionMessages;
    using static Common.GlobalConstants;
    using static Common.SuccessMessages;

    public class ChangeTripStatusCommand : ICommand
    {
        private readonly ITripService trips;
        private readonly ITicketService tickets;

        public ChangeTripStatusCommand(ITripService trips, ITicketService tickets)
        {
            this.trips = trips;
            this.tickets = tickets;
        }

        // ChangeTripStatus {TripId} {NewStatus}
        public string Execute(params string[] arguments)
        {
            var id = int.Parse(arguments[1]);
            var status = (Status)Enum.Parse(typeof(Status), arguments[2]);
            var trip = this.trips.ById(id);

            Validator.ThrowExceptionIfTripIsNull(trip, id);

            var oldStatus = trip.Status;

            if (oldStatus == status)
            {
                throw new InvalidOperationException(string.Format(StatusIsAlreadySetExceptionMessage, status));
            }

            if (oldStatus == Status.Arrived)
            {
                throw new InvalidOperationException(TripIsAlreadyArrivedExceptionMessage);
            }

            this.trips.ChangeStatus(id, status);

            var tripModel = this.trips.ById<TripTownsDepartureTimeModel>(id);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Trip from {tripModel.OriginBusStationTownName} to {tripModel.DestinationBusStationTownName} on {tripModel.DepartureTime.ToString(DateFormat)}");
            stringBuilder.AppendLine(string.Format(ChangeTripStatusCommandSuccessMessage, oldStatus, status));

            // if new status is arrived, add
            if (status == Status.Arrived)
            {
                var passengersCount = this.tickets
                    .TicketsByTripId(trip.Id)
                    .ToList()
                    .Count;

                this.trips.AddArrivedTrip(trip.OriginBusStation, trip.DestinationBusStation, passengersCount);

                stringBuilder.AppendLine($"On {trip.ArrivalTime} - {passengersCount} passengers arrived at {tripModel.DestinationBusStationTownName} from {tripModel.OriginBusStationTownName}");
            }

            return stringBuilder.ToString();
        }
    }
}