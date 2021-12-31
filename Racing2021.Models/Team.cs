namespace Racing2021.Models
{
    public class Team
    {
        private int _id;
        private string _name;
        private string _jerseyName;
        private int _money;

        public int Id { get => _id; set { _id = value; } }
        public string Name { get => _name; set { _name = value; } }
        public string JerseyName { get => _jerseyName; set { _jerseyName = value; } }
        public int Money { get => _money; set { _money = value; } }

        public Team()
        {

        }

        public Team(int id, string name, string jerseyName)
        {
            Id = id;
            Name = name;
            JerseyName = jerseyName;
        }
    }
}
