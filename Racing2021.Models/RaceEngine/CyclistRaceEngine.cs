using Microsoft.Xna.Framework.Graphics;
using System;
using System.Timers;

namespace Racing2021.Models.RaceEngine
{
    public class CyclistRaceEngine
    {
        private int _id;
        private float _cyclistPositionX;
        private float _cyclistPositionY = GeneralParameters.CentralPositionY;
        private float _cyclistSpeedHorizontal;
        private float _cyclistSpeedCobblestone;
        private float _cyclistSpeedUp;
        private float _cyclistSpeedDown;
        private float _cyclistFormOfTheDay;
        private Texture2D _cyclistTexture;
        private string _name;
        private Team _team;

        public int Id { get => _id; set { _id = value; } }
        public float CyclistPositionX { get => _cyclistPositionX; set { _cyclistPositionX = value; } }
        public float CyclistPositionY { get => _cyclistPositionY; set { _cyclistPositionY = value; } }
        public Texture2D CyclistTexture { get => _cyclistTexture; set { _cyclistTexture = value; } }
        public float CyclistSpeedHorizontal { get => _cyclistSpeedHorizontal + CyclistFormOfTheDay; set { _cyclistSpeedHorizontal = value; } }
        public float CyclistSpeedCobblestone { get => _cyclistSpeedCobblestone + CyclistFormOfTheDay; set { _cyclistSpeedCobblestone = value; } }
        public float CyclistSpeedUp { get => _cyclistSpeedUp + CyclistFormOfTheDay; set { _cyclistSpeedUp = value; } }
        public float CyclistSpeedDown { get => _cyclistSpeedDown + CyclistFormOfTheDay; set { _cyclistSpeedDown = value; } }
        public float CyclistFormOfTheDay { get => _cyclistFormOfTheDay; set { _cyclistFormOfTheDay = value; } }
        public string Name { get => _name; set { _name = value; } }
        public DateTime StartTime { get;  set; }
        public DateTime FinishTime { get;  set; }
        public TimeSpan TotalTime { get => FinishTime - StartTime; }
        public string ShowTime { get => TotalTime.Minutes.ToString() + ":" + TotalTime.Seconds.ToString(); }
        public Team Team{ get => _team; set { _team = value; } }

        public CyclistRaceEngine(int id, float speedHorizontal, float speedCobblestone, float speedUp, float speedDown, string name, float cyclistFormOfTheDay, Team team)
        {
            Id = id;
            CyclistSpeedHorizontal = speedHorizontal;
            CyclistSpeedCobblestone = speedCobblestone;
            CyclistSpeedUp = speedUp;
            CyclistSpeedDown = speedDown;
            CyclistFormOfTheDay = cyclistFormOfTheDay;
            Name = name;
            Team = team;
        }
    }
}
