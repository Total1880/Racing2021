using System;

namespace Racing2021.Models
{
    public class Cyclist : Person
    {

        private float _cyclistSpeedHorizontal;
        private float _cyclistSpeedCobblestones;
        private float _cyclistSpeedUp;
        private float _cyclistSpeedDown;

        private bool _selectedForRace;

        private Contract _contract;

        public float CyclistSpeedHorizontal { get => _cyclistSpeedHorizontal; set { _cyclistSpeedHorizontal = (float)Math.Round(value); } }
        public float CyclistSpeedCobblestones { get => _cyclistSpeedCobblestones; set { _cyclistSpeedCobblestones = (float)Math.Round(value); } }
        public float CyclistSpeedUp { get => _cyclistSpeedUp; set { _cyclistSpeedUp = (float)Math.Round(value); } }
        public float CyclistSpeedDown { get => _cyclistSpeedDown; set { _cyclistSpeedDown = (float)Math.Round(value); } }


        public bool SelectedForRace { get => _selectedForRace; set { _selectedForRace = value; } }
        public Contract Contract { get => _contract; set { _contract = value; } }
        public float AllAttributes { get => _cyclistSpeedHorizontal + _cyclistSpeedCobblestones + _cyclistSpeedUp + _cyclistSpeedDown; }


        public Cyclist()
        {
            Contract = new Contract();
        }

        public Cyclist(int id, float speedHorizontal, float cobblestones, float speedUp, float speedDown, string name, int teamId, string nationality, int age = 16)
        {
            Id = id;
            CyclistSpeedHorizontal = speedHorizontal;
            CyclistSpeedCobblestones = cobblestones;
            CyclistSpeedUp = speedUp;
            CyclistSpeedDown = speedDown;
            Name = name;
            Age = age;
            TeamId = teamId;
            SelectedForRace = true;
            Nationality = nationality;
            Contract = new Contract();
        }
    }
}
