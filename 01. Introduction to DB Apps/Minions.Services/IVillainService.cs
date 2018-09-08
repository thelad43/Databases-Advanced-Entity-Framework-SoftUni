namespace Minions.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IVillainService
    {
        IEnumerable<VillainModel> VillainsAndNumberOfMinions();

        VillainWithMinionsModel VillainWithMinionsById(int id);
    }
}