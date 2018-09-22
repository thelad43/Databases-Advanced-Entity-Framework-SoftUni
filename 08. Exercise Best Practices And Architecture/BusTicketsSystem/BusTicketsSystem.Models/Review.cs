namespace BusTicketsSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, 10)]
        public double Grade { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime PublishDate { get; set; }
    }
}