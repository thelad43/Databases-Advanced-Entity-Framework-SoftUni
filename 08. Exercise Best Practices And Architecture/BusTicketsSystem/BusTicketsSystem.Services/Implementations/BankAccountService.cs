namespace BusTicketsSystem.Services.Implementations
{
    using Data;
    using System;
    using System.Linq;

    public class BankAccountService : IBankAccountService
    {
        private readonly BusTicketsSystemDbContext db;

        public BankAccountService(BusTicketsSystemDbContext db)
        {
            this.db = db;
        }

        public void Deposit(int bankAccountId, decimal money)
        {
            if (money <= 0)
            {
                throw new InvalidOperationException("Cannot deposit negative amount of money!");
            }

            var bankAccount = this.db
                .BankAccounts
                .Single(a => a.Id == bankAccountId);

            bankAccount.Balance += money;

            this.db.SaveChanges();
        }

        public void Withdraw(int bankAccountId, decimal money)
        {
            if (money <= 0)
            {
                throw new InvalidOperationException("Cannot withdraw negative amount of money!");
            }

            var bankAccount = this.db
                .BankAccounts
                .Single(a => a.Id == bankAccountId);

            if (bankAccount.Balance - money < 0)
            {
                throw new InvalidOperationException("Insufficient funds!");
            }

            bankAccount.Balance -= money;

            this.db.SaveChanges();
        }
    }
}