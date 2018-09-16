namespace BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class BankAccount
    {
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(MaxLengthName)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(MaxLengthSwiftCode)]
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException(NegativeDepositExceptionMessage);
            }

            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException(NegativeWithdrawExceptionMessage);
            }

            if (amount > this.Balance)
            {
                throw new InvalidOperationException(InsufficientFundsExceptionMessage);
            }

            this.Balance -= amount;
        }
    }
}