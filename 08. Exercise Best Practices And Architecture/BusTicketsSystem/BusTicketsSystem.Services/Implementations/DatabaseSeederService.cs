namespace BusTicketsSystem.Services.Implementations
{
    using BusTicketsSystem.Models;
    using BusTicketsSystem.Models.Enums;
    using Data;
    using System;

    public class DatabaseSeederService : IDatabaseSeederService
    {
        private const int Customers = 50;

        private readonly Random random;
        private readonly BusTicketsSystemDbContext db;

        public DatabaseSeederService(BusTicketsSystemDbContext db)
        {
            this.db = db;
            this.random = new Random();
        }

        public void SeedData()
        {
            var towns = SeedTowns();

            var customers = SeedCustomers(towns);

            var bankAccounts = SeedBankAccounts(customers);

            var busStations = SeedBusStations(towns);

            var companies = SeedCompanies();

            var trips = SeedTrips(busStations, companies);

            var reviews = SeedReviews(customers, busStations, companies);
        }

        private Town[] SeedTowns()
        {
            var towns = new Town[Customers];

            var townNames = new[]
            {
                "Plovdiv",
                "Varna",
                "Burgas",
                "Ruse",
                "Stara Zagora",
                "Pleven",
                "Dobrich",
                "Sliven",
                "Shumen",
                "Pernik",
                "Haskovo",
                "Yambol",
                "Pazardzhik",
                "Blagoevgrad",
                "Veliko Tarnovo",
                "Vratsa",
                "Gabrovo",
                "Asenovgrad",
                "Vidin",
                "Kazanlak",
                "Kyustendil",
                "Kardzhali",
                "Montana",
                "Dimitrovgrad",
                "Targovishte",
                "Lovech",
                "Silistra",
                "Dupnitsa",
                "Svishtov",
                "Razgrad",
                "Gorna Oryahovitsa",
                "Smolyan",
                "Petrich",
                "Sandanski",
                "Samokov",
                "Sevlievo",
                "Lom",
                "Karlovo",
                "Velingrad",
                "Nova Zagora",
                "Troyan",
                "Aytos",
                "Botevgrad",
                "Gotse Delchev",
                "Peshtera",
                "Harmanli",
                "Karnobat",
                "Svilengrad",
                "Panagyurishte",
                "Chirpan",
            };

            for (var i = 0; i < Customers; i++)
            {
                towns[i] = new Town
                {
                    Name = townNames[i],
                    Country = "Bulgaria"
                };
            }

            this.db.AddRange(towns);
            this.db.SaveChanges();

            return towns;
        }

        private Customer[] SeedCustomers(Town[] towns)
        {
            var firstNames = new[]
            {
                "AARON",
                "ABDUL",
                "ABE",
                "ABEL",
                "ABRAHAM",
                "ABRAM",
                "ADALBERTO",
                "ADAM",
                "ADAN",
                "ADOLFO",
                "ADOLPH",
                "ADRIAN",
                "AGUSTIN",
                "AHMAD",
                "AHMED",
                "AL",
                "ALAN",
                "ALBERT",
                "ALBERTO",
                "ALDEN",
                "ALDO",
                "ALEC",
                "ALEJANDRO",
                "ALEX",
                "ALEXANDER",
                "ALEXIS",
                "ALFONSO",
                "ALFONZO",
                "ALFRED",
                "ALFREDO",
                "ALI",
                "ALLAN",
                "ALLEN",
                "ALONSO",
                "ALONZO",
                "ALPHONSE",
                "ALPHONSO",
                "ALTON",
                "ALVA",
                "ALVARO",
                "ALVIN",
                "AMADO",
                "AMBROSE",
                "AMOS",
                "ANDERSON",
                "ANDRE",
                "ANDREA",
                "ANDREAS",
                "ANDRES",
                "ANDREW",
            };

            var lastNames = new[]
            {
                "JONATHAN",
                "JONATHON",
                "JORDAN",
                "JORDON",
                "JORGE",
                "JOSE",
                "JOSEF",
                "JOSEPH",
                "JOSH",
                "JOSHUA",
                "JOSIAH",
                "JOSPEH",
                "JOSUE",
                "JUAN",
                "JUDE",
                "JUDSON",
                "JULES",
                "JULIAN",
                "JULIO",
                "JULIUS",
                "JUNIOR",
                "JUSTIN",
                "KAREEM",
                "KARL",
                "KASEY",
                "KEENAN",
                "KEITH",
                "KELLEY",
                "KELLY",
                "KELVIN",
                "KEN",
                "KENDALL",
                "KENDRICK",
                "KENETH",
                "KENNETH",
                "KENNITH",
                "KENNY",
                "KENT",
                "KENTON",
                "KERMIT",
                "KERRY",
                "KEVEN",
                "KEVIN",
                "KIETH",
                "KIM",
                "KING",
                "KIP",
                "KIRBY",
                "KIRK",
                "KOREY",
            };

            var birthDates = new DateTime[Customers];

            var genders = new bool[Customers];

            for (var i = 0; i < Customers; i++)
            {
                birthDates[i] = new DateTime(this.random.Next(1950, 2019), this.random.Next(1, 13), this.random.Next(1, 29));
                genders[i] = GetRandomBool();
            }

            var customers = new Customer[Customers];

            for (var i = 0; i < Customers; i++)
            {
                customers[i] = new Customer
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    BirthDate = birthDates[i],
                    IsMale = genders[i],
                    HomeTown = towns[i],
                    HomeTownId = towns[i].Id,
                };
            }

            this.db.AddRange(customers);
            this.db.SaveChanges();

            return customers;
        }

        private BankAccount[] SeedBankAccounts(Customer[] customers)
        {
            var accountNumbers = new string[Customers];

            var balances = new decimal[Customers];

            for (var i = 0; i < Customers; i++)
            {
                accountNumbers[i] = Guid.NewGuid().ToString();
                balances[i] = this.random.Next(0, 123456);
            }

            var bankAccounts = new BankAccount[Customers];

            for (var i = 0; i < Customers; i++)
            {
                bankAccounts[i] = new BankAccount
                {
                    AccountNumber = accountNumbers[i],
                    Balance = balances[i],
                    Customer = customers[i],
                    CustomerId = customers[i].Id
                };
            }

            this.db.AddRange(bankAccounts);
            this.db.SaveChanges();

            // adding bank accounts to customers
            for (var i = 0; i < Customers; i++)
            {
                customers[i].BankAccount = bankAccounts[i];
                customers[i].BankAccountId = bankAccounts[i].Id;
            }

            this.db.SaveChanges();
            return bankAccounts;
        }

        private BusStation[] SeedBusStations(Town[] towns)
        {
            var names = new string[Customers]
            {
                "Rakusai Bus Terminal",
                "Nishisakaidanicho 3chome",
                "Sakaidani Center Mae",
                "Rakusai Koko Mae",
                "Shinbayashi Kodan Jutaku Mae",
                "Sakaidani Ohashi",
                "Obatagawa Koen Kitaguchi",
                "Kokudo Nakayama",
                "Sannomiya",
                "Kokaido Mae",
                "Katagihara",
                "Kawashima Awatacho",
                "Kawashimacho",
                "Katsura Eki Higashiguchi",
                "Katsura Shobosho Mae",
                "Shimokatsura",
                "Katsurarikyu Mae (Katsura Imperial Villa)",
                "Katsura Ohashi",
                "Higashigawacho",
                "Senshoji",
                "Daimoncho",
                "Tsukiyomibashi",
                "Nishioji Nanajo",
                "Nanajo Onmae Dori",
                "Nanajo Senbon",
                "Umekoji Koen Mae (Umekoji Park)",
                "Nanajo Mibugawa",
                "Nanajo Omiya/Kyoto Suizokukan Mae (Kyoto Aquarium)",
                "Nanajo Horikawa",
                "Shimogyoku Sogochosha Mae",
                "Kyoto Sta",
                "Nishigamo Shako Mae",
                "Jinkoin Mae",
                "Omiya Somonguchicho",
                "Omiya Tajiricho",
                "Kamigamo Misonobashi",
                "Kamogawa Chugaku Mae",
                "Shimogishicho",
                "Kamihorikawa",
                "Higashi Takanawacho",
                "Shimotoridacho",
                "Kitaoji Horikawa",
                "Kitaoji Shinmachi",
                "Kitaoji Bus Terminal (Subwey Kitaoji Sta.)",
                "Karasuma Kitaoji",
                "Shimofusacho",
                "Matsunoshitacho",
                "Izumojibashi",
                "Izumoji Tawaracho",
                "Izumoji Kaguracho",
            };

            var busStations = new BusStation[Customers];

            for (var i = 0; i < Customers; i++)
            {
                busStations[i] = new BusStation
                {
                    Name = names[i],
                    Town = towns[i],
                    TownId = towns[i].Id
                };
            }

            this.db.AddRange(busStations);
            this.db.SaveChanges();

            return busStations;
        }

        private Company[] SeedCompanies()
        {
            var names = new[]
            {
                "3Com Corp",
                "3M Company",
                "A.G. Edwards Inc.",
                "Abbott Laboratories",
                "Abercrombie & Fitch Co.",
                "ABM Industries Incorporated",
                "Ace Hardware Corporation",
                "ACT Manufacturing Inc.",
                "Acterna Corp.",
                "Adams Resources & Energy Inc.",
                "ADC Telecommunications Inc.",
                "Adelphia Communications Corporation",
                "Administaff Inc.",
                "Adobe Systems Incorporated",
                "Adolph Coors Company",
                "Advance Auto Parts Inc.",
                "Advanced Micro Devices Inc.",
                "AdvancePCS Inc.",
                "Advantica Restaurant Group Inc.",
                "The AES Corporation",
                "Aetna Inc.",
                "Affiliated Computer Services Inc.",
                "AFLAC Incorporated",
                "AGCO Corporation",
                "Agilent Technologies Inc.",
                "Agway Inc.",
                "Apartment Investment and Management Company",
                "Air Products and Chemicals Inc.",
                "Airborne Inc.",
                "Airgas Inc.",
                "AK Steel Holding Corporation",
                "Alaska Air Group Inc.",
                "Alberto-Culver Company",
                "Albertson's Inc.",
                "Alcoa Inc.",
                "Alleghany Corporation",
                "Allegheny Energy Inc.",
                "Allegheny Technologies Incorporated",
                "Allergan Inc.",
                "ALLETE Inc.",
                "Alliant Energy Corporation",
                "Allied Waste Industries Inc.",
                "Allmerica Financial Corporation",
                "The Allstate Corporation",
                "ALLTEL Corporation",
                "The Alpin ,Group Inc.",
                "Amazon.com Inc.",
                "AMC Entertainment Inc.",
                "American Power Conversion Corporation",
                "Amerada Hess Corporation",
            };

            var companies = new Company[Customers];

            for (var i = 0; i < Customers; i++)
            {
                companies[i] = new Company
                {
                    Name = names[i]
                };
            }

            this.db.AddRange(companies);
            this.db.SaveChanges();

            return companies;
        }

        private Trip[] SeedTrips(BusStation[] busStations, Company[] companies)
        {
            var departureTimes = new DateTime[Customers];
            var arrivalTimes = new DateTime[Customers];
            var validStatuses = new[] { 1, 2, 3, 4 };
            var statuses = new Status[Customers];
            var originBusStations = new BusStation[Customers];
            var destinationBusStations = new BusStation[Customers];

            for (var i = 0; i < Customers; i++)
            {
                departureTimes[i] = new DateTime(this.random.Next(1950, 2016), this.random.Next(1, 13), this.random.Next(1, 29));
                arrivalTimes[i] = new DateTime(this.random.Next(1950, 2016), this.random.Next(1, 13), this.random.Next(1, 29));
                statuses[i] = (Status)validStatuses[this.random.Next(0, 4)];
                originBusStations[i] = busStations[this.random.Next(0, Customers)];
                destinationBusStations[i] = busStations[this.random.Next(0, Customers)];
            }

            var trips = new Trip[Customers];

            for (var i = 0; i < Customers; i++)
            {
                trips[i] = new Trip
                {
                    DepartureTime = departureTimes[i],
                    ArrivalTime = arrivalTimes[i],
                    Status = statuses[i],
                    OriginBusStation = originBusStations[i],
                    OriginBusStationId = originBusStations[i].Id,
                    DestinationBusStation = destinationBusStations[i],
                    DestinationStationId = destinationBusStations[i].Id,
                    Company = companies[i],
                    CompanyId = companies[i].Id
                };
            }

            this.db.AddRange(trips);
            this.db.SaveChanges();

            return trips;
        }

        private Review[] SeedReviews(Customer[] customers, BusStation[] busStations, Company[] companies)
        {
            var reviews = new Review[Customers];

            for (var i = 0; i < Customers; i++)
            {
                reviews[i] = new Review
                {
                    Customer = customers[i],
                    CustomerId = customers[i].Id,
                    Company = companies[i],
                    CompanyId = companies[i].Id,
                    Content = Guid.NewGuid().ToString(),
                    Grade = this.random.Next(1, 11),
                    PublishDate = new DateTime(this.random.Next(1950, 2019), this.random.Next(1, 13), this.random.Next(1, 29))
                };
            }

            this.db.AddRange(reviews);
            this.db.SaveChanges();

            return reviews;
        }

        private bool GetRandomBool()
        {
            // two ways: 1 = false, 2 = true
            var number = this.random.Next(1, 3);
            var result = number % 2 == 0;
            return result;
        }
    }
}