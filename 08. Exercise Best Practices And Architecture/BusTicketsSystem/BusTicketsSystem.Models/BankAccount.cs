namespace BusTicketsSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BankAccount
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}