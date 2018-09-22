namespace BusTicketsSystem.Services
{
    public interface IBankAccountService
    {
        void Deposit(int bankAccountId, decimal money);

        void Withdraw(int bankAccountId, decimal money);
    }
}