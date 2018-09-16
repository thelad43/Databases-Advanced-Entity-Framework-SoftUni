namespace BillsPaymentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(MaxLengthPassword)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(MaxLengthEmail)]
        public string Email { get; set; }

        public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
    }
}