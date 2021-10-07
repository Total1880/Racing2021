namespace Racing2021.Models
{
    public class Cyclist
    {
        private int _id;
        private string _name;
        private float _cyclistSpeedHorizontal;
        private float _cyclistSpeedUp;
        private float _cyclistSpeedDown;
        private int _age;
        private int _teamId;

        public float CyclistSpeedHorizontal { get => _cyclistSpeedHorizontal; set { _cyclistSpeedHorizontal = value; } }
        public float CyclistSpeedUp { get => _cyclistSpeedUp; set { _cyclistSpeedUp = value; } }
        public float CyclistSpeedDown { get => _cyclistSpeedDown; set { _cyclistSpeedDown = value; } }
        public string Name { get => _name; set { _name = value; } }
        public int Age { get => _age; set { _age = value; } }
        public int Id { get => _id; set { _id = value; } }
        public int TeamId { get => _teamId; set { _teamId = value; } }

        public Cyclist()
        {

        }

        public Cyclist(int id,  float speedHorizontal, float speedUp, float speedDown, string name, int teamId, int age = 16)
        {
            Id = id;
            CyclistSpeedHorizontal = speedHorizontal;
            CyclistSpeedUp = speedUp;
            CyclistSpeedDown = speedDown;
            Name = name;
            Age = age;
            TeamId = teamId;
        }
    }
}
