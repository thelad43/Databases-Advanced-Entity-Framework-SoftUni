namespace BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.GlobalConstants;

    public class CreditCard
    {
        public int Id { get; set; }

        public decimal MoneyOwed { get; set; }

        public decimal Limit { get; set; }

        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException(NegativeDepositExceptionMessage);
            }

            if (this.MoneyOwed < amount)
            {
                throw new InvalidOperationException(DepositTooMuchExceptionMessage);
            }

            this.MoneyOwed -= amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException(NegativeWithdrawExceptionMessage);
            }

            if (this.LimitLeft < amount)
            {
                throw new InvalidOperationException(InsufficientLimitExceptionMessage);
            }

            this.MoneyOwed += amount;
        }
    }
}