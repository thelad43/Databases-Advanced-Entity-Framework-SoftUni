namespace BusTicketsSystem.Services
{
    using BusTicketsSystem.Models;

    public interface ICustomerService
    {
        Customer ById(int id);
    }
}