namespace BillsPaymentSystem.Services.Implementations
{
    using BillsPaymentSystem.Models;
    using BillsPaymentSystem.Models.Enums;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Common.GlobalConstants;

    public class UserService : IUserService
    {
        private readonly BillsPaymentDbContext db;

        public UserService(BillsPaymentDbContext db)
        {
            this.db = db;
        }

        public UserWithPaymentMethodsModel ByIdWithPaymentMethods(int id)
        {
            var user = this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserWithPaymentMethodsModel
                {
                    Name = $"{u.FirstName} {u.LastName}",
                    BankAccounts = u.PaymentMethods
                        .Where(m => m.Type == PaymentMethodType.BankAccount)
                        .Select(m => m.BankAccount)
                        .ToList(),
                    CreditCards = u.PaymentMethods
                        .Where(m => m.Type == PaymentMethodType.CreditCard)
                        .Select(m => m.CreditCard)
                        .ToList()
                })
                .FirstOrDefault();

            if (user == null)
            {
                throw new NullReferenceException($"There is no user with id: {id}");
            }

            return user;
        }

        public void PayBills(int userId, decimal amount)
        {
            var user = this.db
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new UserWithPaymentMethodsModel
                {
                    BankAccounts = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                        .Select(pm => pm.BankAccount)
                        .ToArray(),
                    CreditCards = u.PaymentMethods
                        .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                        .Select(pm => pm.CreditCard)
                        .ToArray()
                })
                .FirstOrDefault();

            if (user == null)
            {
                throw new NullReferenceException($"There is no user with id: {userId}");
            }

            if (!CanPay(user.BankAccounts, user.CreditCards, amount))
            {
                throw new InvalidOperationException(CannotAffordPaymentExceptionMessage);
            }

            amount = PayWithBankAsMuchAsPossible(user.BankAccounts, amount, this.db);

            if (amount > 0)
            {
                PayWithCreditCards(amount, user.CreditCards, this.db);
            }

            this.db.SaveChanges();
            Console.WriteLine("Successfully payed!");
        }

        public bool CanPay(IEnumerable<BankAccount> bankAccounts, IEnumerable<CreditCard> creditCards, decimal amount)
        {
            return amount <= (bankAccounts.Select(a => a.Balance).Sum() + creditCards.Select(c => c.LimitLeft).Sum());
        }

        public decimal PayWithBankAsMuchAsPossible(IEnumerable<BankAccount> bankAccounts, decimal amount, BillsPaymentDbContext db)
        {
            foreach (var account in bankAccounts)
            {
                db.Entry(account).State = EntityState.Unchanged;

                if (account.Balance >= amount)
                {
                    account.Withdraw(amount);
                    amount = 0;
                    break;
                }

                amount -= account.Balance;
                account.Withdraw(account.Balance);
            }

            return amount;
        }

        public void PayWithCreditCards(decimal amount, IEnumerable<CreditCard> creditCards, BillsPaymentDbContext db)
        {
            if (creditCards.Select(c => c.LimitLeft).Sum() < amount)
            {
                throw new InvalidOperationException(AmountGreaterThanPossibilities);
            }

            foreach (var card in creditCards)
            {
                db.Entry(card).State = EntityState.Unchanged;

                if (card.LimitLeft >= amount)
                {
                    card.Withdraw(amount);
                    return;
                }

                amount -= card.LimitLeft;
                card.Withdraw(card.LimitLeft);
            }
        }
    }
}