namespace BusTicketsSystem.Common
{
    public static class SuccessMessages
    {
        public const string BuyTicketSuccessMessage = "Customer {0} {1} bought ticket for trip {2} for ${3:F2} on seat {4}";
        public const string PublishReviewSuccessMessage = "Customer {0} {1} published review for company {2}";
        public const string ChangeTripStatusCommandSuccessMessage = "Status changed from {0} to {1}";
    }
}