namespace BusTicketsSystem.App.Core.Commands
{
    using Interfaces;
    using Services;
    using System.Text;

    public class PrintReviewsCommand : ICommand
    {
        private readonly IReviewService reviews;

        public PrintReviewsCommand(IReviewService reviews)
        {
            this.reviews = reviews;
        }

        // PrintReviews {BusCompanyId}
        public string Execute(params string[] arguments)
        {
            var companyId = int.Parse(arguments[1]);

            var reviews = this.reviews.ReviewsForCompany(companyId);

            var stringBuilder = new StringBuilder();

            foreach (var review in reviews)
            {
                stringBuilder.AppendLine($"{review.BusCompanyId} {review.Grade} {review.PublishDate}");
                stringBuilder.AppendLine(review.CustomerName);
                stringBuilder.AppendLine(review.Content);
            }

            return stringBuilder.ToString();
        }
    }
}