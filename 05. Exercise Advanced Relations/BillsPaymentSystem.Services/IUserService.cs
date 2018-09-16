namespace BillsPaymentSystem.Services
{
    using BillsPaymentSystem.Models;
    using Data;
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        UserWithPaymentMethodsModel ByIdWithPaymentMethods(int id);

        void PayBills(int userId, decimal amount);

        bool CanPay(IEnumerable<BankAccount> bankAccounts, IEnumerable<CreditCard> creditCards, decimal amount);

        decimal PayWithBankAsMuchAsPossible(IEnumerable<BankAccount> bankAccounts, decimal amount, BillsPaymentDbContext db);
    }
}