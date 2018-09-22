namespace BusTicketsSystem.Services
{
    using BusTicketsSystem.Models;

    public interface ICompanyService
    {
        Company ByName(string name);
    }
}