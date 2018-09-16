namespace BillsPaymentSystem.Services.Implementations
{
    using BillsPaymentSystem.Models;
    using Data;
    using System;
    using static Common.GlobalConstants;

    public class DbSeeder : IDbSeeder
    {
        private readonly Random random;
        private readonly BillsPaymentDbContext db;

        private User[] users;
        private BankAccount[] bankAccounts;
        private CreditCard[] creditCards;

        public DbSeeder(BillsPaymentDbContext db)
        {
            this.db = db;
            this.random = new Random();
        }

        public void SeedData()
        {
            this.SeedUsers();
            this.SeedCreditCards();
            this.SeedBankAccounts();
            this.SeedPaymentMethods();
        }

        private void SeedUsers()
        {
            var userFirstNames = new[]
            {
                "Pesho",
                "Gosho",
                "Stamat",
                "Zdravko",
                "Emko",
                "Nasko",
                "Test",
                "Bobi",
                "Ivelin",
                "Ivan",
                "Cvetelina",
                "Kristina",
                "Maria",
            };

            var userLastNames = new[]
            {
                "Ivanov",
                "Petrov",
                "Stamatkov",
                "Borisov",
                "Iliev",
                "AtanaSkov",
                "TestTest",
                "Qnucev",
                "Anatoliev",
                "Todorov",
                "Krumova",
                "Peshova",
                "Ilieva",
            };

            var userPasswords = new[]
            {
                "tupaParola1",
                "tupaParola2",
                "tupaParola3",
                "tupaParola4",
                "tupaParola5",
                "tupaParola6",
                "tupaParola7",
                "tupaParola8",
                "tupaParola9",
                "tupaParola10",
                "tupaParola11",
                "tupaParola12",
                "tupaParola13",
            };

            var userEmails = new[]
            {
                "email1@email.com",
                "email2@email.com",
                "email3@email.com",
                "email4@email.com",
                "email5@email.com",
                "email6@email.com",
                "email7@email.com",
                "email8@email.com",
                "email9@email.com",
                "email10@email.com",
                "email11@email.com",
                "email12@email.com",
                "email13@email.com",
            };

            var users = new User[UsersCount];

            for (int i = 0; i < UsersCount; i++)
            {
                var user = new User
                {
                    FirstName = userFirstNames[i],
                    LastName = userLastNames[i],
                    Password = userPasswords[i],
                    Email = userEmails[i]
                };

                users[i] = user;
            }

            this.db.AddRange(users);
            this.db.SaveChanges();
            this.users = users;
        }

        private void SeedCreditCards()
        {
            var creditCards = new CreditCard[UsersCount];

            for (int i = 0; i < UsersCount; i++)
            {
                var year = this.random.Next(1900, 2019);
                var month = this.random.Next(1, 13);
                var date = this.random.Next(1, 32);

                var expirationDate = new DateTime(year, month, date);

                var limit = this.random.Next(1, 77777);
                var moneyOwed = this.random.Next(1, 77777);

                var creditCard = new CreditCard
                {
                    ExpirationDate = expirationDate,
                    Limit = limit,
                    MoneyOwed = moneyOwed
                };

                creditCards[i] = creditCard;
            }

            this.db.AddRange(creditCards);
            this.db.SaveChanges();
            this.creditCards = creditCards;
        }

        private void SeedBankAccounts()
        {
            var bankAccounts = new BankAccount[UsersCount];

            for (int i = 0; i < UsersCount; i++)
            {
                var balance = this.random.Next(1, 456789);
                var bankName = $"{this.random.Next(1, 50)} BankName";
                var swiftCode = $"4K-JSK{this.random.Next(555, 555555)}AH-JS";

                var bankAccount = new BankAccount
                {
                    Balance = balance,
                    BankName = bankName,
                    SwiftCode = swiftCode
                };

                bankAccounts[i] = bankAccount;
            }

            this.db.AddRange(bankAccounts);
            this.db.SaveChanges();
            this.bankAccounts = bankAccounts;
        }

        private void SeedPaymentMethods()
        {
            var paymentMethods = new PaymentMethod[UsersCount];

            for (int i = 0; i < UsersCount; i++)
            {
                var isBankAccount = this.random.Next(1, 3) % 2 == 0;

                try
                {
                    paymentMethods[i] = isBankAccount
                        ? new PaymentMethod(this.users[this.random.Next(0, UsersCount)], this.bankAccounts[i])
                        : new PaymentMethod(this.users[this.random.Next(0, UsersCount)], this.creditCards[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    i--;
                }
            }

            this.db.AddRange(paymentMethods);
            this.db.SaveChanges();
        }
    }
}