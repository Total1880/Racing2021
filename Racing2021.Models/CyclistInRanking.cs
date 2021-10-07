using System;
using System.Collections.Generic;
using System.Text;

namespace Racing2021.Models
{
    public class CyclistInRanking
    {
        private int _id;
        private string _name;
        private string _teamName;
        private TimeSpan _totalTime;

        public int Id => _id;
        public string Name => _name;
        public string TeamName => _teamName;
        public TimeSpan TotalTime { get => _totalTime; set { _totalTime = value; } }
        public string ShowTime { get => TotalTime.Minutes.ToString() + ":" + TotalTime.Seconds.ToString(); }


        public CyclistInRanking(int id, string name, TimeSpan totalTime, string teamName)
        {
            _id = id;
            _name = name;
            TotalTime = totalTime;
            _teamName = teamName;
        }
    }
}
