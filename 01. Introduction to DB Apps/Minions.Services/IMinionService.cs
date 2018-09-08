namespace Minions.Services
{
    using Minions.Models;
    using System.Collections.Generic;

    public interface IMinionService
    {
        int RemoveVillain(int id);

        void Add(Minion minion, Villain villain);

        void PrintAllMinionNames();

        void IncreaseMinionsAge(IEnumerable<int> ids);

        void MakeNameTitleCase(IEnumerable<int> ids);

        void PrintMinions();
    }
}