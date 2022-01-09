using System.Collections.Generic;

namespace Racing2021.Models
{
    public class Division
    {
        public int Id { get; set; }
        public int Tier { get; set; }
        public int Reputation { get; set; }
        public string Name { get; set; }
        public IList<int> TeamsId { get; set; }

        public Division()
        {

        }

        public Division(int id, int tier, string name, int reputation)
        {
            Id = id;
            Tier = tier;
            Name = name;
            Reputation = reputation;

            TeamsId = new List<int>();
        }
    }
}
