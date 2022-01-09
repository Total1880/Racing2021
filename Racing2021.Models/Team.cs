namespace Racing2021.Models
{
    public class Team
    {
        private int _id;
        private string _name;
        private string _jerseyName;
        private int _money;
        private int _managerId;
        private int _reputation;
        public int Id { get => _id; set { _id = value; } }
        public string Name { get => _name; set { _name = value; } }
        public string JerseyName { get => _jerseyName; set { _jerseyName = value; } }
        public int Money { get => _money; set { _money = value; } }
        public int ManagerId { get => _managerId; set { _managerId = value; } }
        public int Reputation 
        { 
            get => _reputation; 
            set 
            { 
                _reputation = value;
                if (_reputation > 9999)
                {
                    _reputation = 9999;
                }
                if (_reputation < 1)
                {
                    _reputation = 1;
                }
            } 
        }

        public Team()
        {

        }

        public Team(int id, string name, string jerseyName, int reputation)
        {
            Id = id;
            Name = name;
            JerseyName = jerseyName;
            Reputation = reputation;
        }
    }
}
