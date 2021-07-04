namespace Racing2021.Models
{
    public class Cyclist
    {
        private string _name;
        private float _cyclistSpeedHorizontal;
        private float _cyclistSpeedUp;
        private float _cyclistSpeedDown;

        public float CyclistSpeedHorizontal { get => _cyclistSpeedHorizontal; set { _cyclistSpeedHorizontal = value; } }
        public float CyclistSpeedUp { get => _cyclistSpeedUp; set { _cyclistSpeedUp = value; } }
        public float CyclistSpeedDown { get => _cyclistSpeedDown; set { _cyclistSpeedDown = value; } }
        public string Name { get => _name; set { _name = value; } }

        public Cyclist()
        {

        }

        public Cyclist(float speedHorizontal, float speedUp, float speedDown, string name)
        {
            CyclistSpeedHorizontal = speedHorizontal;
            CyclistSpeedUp = speedUp;
            CyclistSpeedDown = speedDown;
            Name = name;
        }
    }
}
