using System;

namespace Racing2021.Models
{
    public class Cyclist : Person
    {

        private float _cyclistSpeedHorizontal;
        private float _cyclistSpeedCobblestones;
        private float _cyclistSpeedUp;
        private float _cyclistSpeedDown;
        private float _cyclistSpeedHorizontalPotential;
        private float _cyclistSpeedCobblestonesPotential;
        private float _cyclistSpeedUpPotential;
        private float _cyclistSpeedDownPotential;

        private bool _selectedForRace;

        private Contract _contract;

        public float CyclistSpeedHorizontal { get => _cyclistSpeedHorizontal; 
            set 
            {
                _cyclistSpeedHorizontal = value > CyclistSpeedHorizontalPotential ? CyclistSpeedHorizontalPotential : (float)Math.Round(value);
            } 
        }
        public float CyclistSpeedCobblestones { get => _cyclistSpeedCobblestones; 
            set 
            {
                _cyclistSpeedCobblestones = value > CyclistSpeedCobblestonesPotential ? CyclistSpeedCobblestones : (float)Math.Round(value);
            } 
        }
        public float CyclistSpeedUp { get => _cyclistSpeedUp; 
            set 
            {
                _cyclistSpeedUp = value > CyclistSpeedUpPotential ? CyclistSpeedUpPotential : (float)Math.Round(value);
            } 
        }
        public float CyclistSpeedDown { get => _cyclistSpeedDown; 
            set 
            {
                _cyclistSpeedDown = value > CyclistSpeedDownPotential ? CyclistSpeedDownPotential : (float)Math.Round(value);
            } 
        }
        public float CyclistSpeedHorizontalPotential { get => _cyclistSpeedHorizontalPotential; set { _cyclistSpeedHorizontalPotential = (float)Math.Round(value); } }
        public float CyclistSpeedCobblestonesPotential { get => _cyclistSpeedCobblestonesPotential; set { _cyclistSpeedCobblestonesPotential = (float)Math.Round(value); } }
        public float CyclistSpeedUpPotential { get => _cyclistSpeedUpPotential; set { _cyclistSpeedUpPotential = (float)Math.Round(value); } }
        public float CyclistSpeedDownPotential { get => _cyclistSpeedDownPotential; set { _cyclistSpeedDownPotential = (float)Math.Round(value); } }


        public bool SelectedForRace { get => _selectedForRace; set { _selectedForRace = value; } }
        public Contract Contract { get => _contract; set { _contract = value; } }
        public float AllAttributes { get => _cyclistSpeedHorizontal + _cyclistSpeedCobblestones + _cyclistSpeedUp + _cyclistSpeedDown; }


        public Cyclist()
        {
            Contract = new Contract();
        }

        public Cyclist(int id, float speedHorizontal, float cobblestones, float speedUp, float speedDown, string name, int teamId, string nationality, int age = 16, float generalPotential = 100)
        {
            Id = id;

            CyclistSpeedHorizontalPotential = generalPotential;
            CyclistSpeedCobblestonesPotential = generalPotential;
            CyclistSpeedUpPotential = generalPotential;
            CyclistSpeedDownPotential = generalPotential;
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
