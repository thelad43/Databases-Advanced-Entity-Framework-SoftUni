namespace BusTicketsSystem.App.Core.Commands
{
    using Infrastructure;
    using Interfaces;
    using Services;
    using System.Linq;

    using static Common.SuccessMessages;

    public class PublishReviewCommand : ICommand
    {
        private readonly IReviewService reviews;
        private readonly ICustomerService customers;
        private readonly ICompanyService companies;

        public PublishReviewCommand(IReviewService reviews, ICustomerService customers, ICompanyService companies)
        {
            this.reviews = reviews;
            this.customers = customers;
            this.companies = companies;
        }

        // PublishReview {CustomerId} {Grade} {Bus Company Name} {Content}
        public string Execute(params string[] arguments)
        {
            var customerId = int.Parse(arguments[1]);
            var grade = double.Parse(arguments[2]);
            var companyName = arguments[3];
            var content = arguments.Skip(4).ToArray();

            var customer = this.customers.ById(customerId);
            var company = this.companies.ByName(companyName);

            Validator.ThrowExceptionIfCustomerIsNull(customer, customerId);
            Validator.ThrowExceptionIfCompanyIsNull(company, companyName);

            this.reviews.Publish(customer, grade, company, string.Join(" ", content));

            return string.Format(PublishReviewSuccessMessage, customer.FirstName, customer.LastName, companyName);
        }
    }
}