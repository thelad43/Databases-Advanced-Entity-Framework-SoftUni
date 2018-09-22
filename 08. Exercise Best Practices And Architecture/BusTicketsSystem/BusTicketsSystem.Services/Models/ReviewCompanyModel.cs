namespace BusTicketsSystem.Services.Models
{
    using System;

    public class ReviewCompanyModel
    {
        public int BusCompanyId { get; set; }

        public double Grade { get; set; }

        public DateTime PublishDate { get; set; }

        public string CustomerName { get; set; }

        public string Content { get; set; }
    }
}