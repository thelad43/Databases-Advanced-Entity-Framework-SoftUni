namespace BusTicketsSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BusTicketsSystem.Models;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReviewService : IReviewService
    {
        private readonly BusTicketsSystemDbContext db;

        public ReviewService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public Review Publish(Customer customer, double grade, Company company, string content)
        {
            var review = new Review
            {
                Company = company,
                CompanyId = company.Id,
                Customer = customer,
                CustomerId = customer.Id,
                Grade = grade,
                PublishDate = DateTime.Now,
                Content = content
            };

            this.db.Add(review);
            this.db.SaveChanges();

            return review;
        }

        public IEnumerable<ReviewCompanyModel> ReviewsForCompany(int companyId)
            => this.db
                .Reviews
                .Where(r => r.CompanyId == companyId)
                .ProjectTo<ReviewCompanyModel>()
                .ToList();
    }
}