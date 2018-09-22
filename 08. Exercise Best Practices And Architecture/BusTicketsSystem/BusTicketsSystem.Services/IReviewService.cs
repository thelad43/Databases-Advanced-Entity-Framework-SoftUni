namespace BusTicketsSystem.Services
{
    using BusTicketsSystem.Models;

    using Models;
    using System.Collections.Generic;

    public interface IReviewService
    {
        Review Publish(Customer customer, double grade, Company company, string content);

        IEnumerable<ReviewCompanyModel> ReviewsForCompany(int companyId);
    }
}