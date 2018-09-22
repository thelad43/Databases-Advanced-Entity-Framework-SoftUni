namespace BusTicketsSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public bool? IsMale { get; set; }

        public int HomeTownId { get; set; }

        public Town HomeTown { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}