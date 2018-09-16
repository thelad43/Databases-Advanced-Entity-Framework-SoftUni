namespace BillsPaymentSystem.Services.Models
{
    using BillsPaymentSystem.Models;
    using System.Collections.Generic;

    public class UserWithPaymentMethodsModel
    {
        public string Name { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public IEnumerable<CreditCard> CreditCards { get; set; }
    }
}