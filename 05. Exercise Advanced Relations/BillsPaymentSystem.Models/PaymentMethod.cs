namespace BillsPaymentSystem.Models
{
    using Enums;

    public class PaymentMethod
    {
        public PaymentMethod()
        {
        }

        public PaymentMethod(User user, BankAccount bankAccount)
        {
            this.User = user;
            this.BankAccount = bankAccount;
        }

        public PaymentMethod(User user, CreditCard creditCard)
        {
            this.User = user;
            this.CreditCard = creditCard;
        }

        public int Id { get; set; }

        public int? BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public PaymentMethodType Type { get; set; }
    }
}