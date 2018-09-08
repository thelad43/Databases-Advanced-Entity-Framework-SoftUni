namespace Minions.Models
{
    public class MinionsVillains
    {
        public int MinionId { get; set; }

        public Minion Minion { get; set; }

        public int VillainId { get; set; }

        public Villain Villain { get; set; }
    }
}