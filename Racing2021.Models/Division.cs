using System.Collections.Generic;

namespace Racing2021.Models
{
    public class Division
    {
        public int Id;
        public int Tier;
        public string Name;
        public IList<int> TeamsId;

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
