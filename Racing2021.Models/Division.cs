using System.Collections.Generic;

namespace Racing2021.Models
{
    public class Division
    {
        public int Id { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public IList<int> TeamsId { get; set; }

        public Division()
        {

        }

        public Division(int id, int tier, string name)
        {
            Id = id;
            Tier = tier;
            Name = name;

            TeamsId = new List<int>();
        }
    }
}
