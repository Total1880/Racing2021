using Racing2021.Models;
using Racing2021.Models.Enums;
using Racing2021.Models.RaceEngine;
using Racing2021.RaceEngine.Interfaces;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing2021.Services
{
    public class RaceService : IRaceService
    {
        private IRaceEngineStart _raceEngineStart;
        private IList<CyclistRaceEngine> _cyclistsRaceEngine;
        private IList<CyclistRaceEngine> _finishedCyclists;
        
        

        public RaceService(IRaceEngineStart raceEngineStart)
        {
            _raceEngineStart = raceEngineStart;
            
        }

        public void StartRace(IList<TrackTile> trackTileGraphics, IList<Cyclist> cyclists, IList<Team> teams)
        {
            InitializeTestCyclists(cyclists, teams);

            _raceEngineStart.Main(_cyclistsRaceEngine, trackTileGraphics);
            _finishedCyclists = _raceEngineStart.FinishedCyclists();
        }

        static float RandomFloat(float min, float max)
        {
            Random random = new Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        private void InitializeTestCyclists(IList<Cyclist> cyclists, IList<Team> teams)
        {
            _cyclistsRaceEngine = new List<CyclistRaceEngine>();

            foreach (var cyclist in cyclists)
            {
                var team = teams.Where(t => t.Id == cyclist.TeamId).FirstOrDefault();
                _cyclistsRaceEngine.Add(new CyclistRaceEngine(cyclist.Id, cyclist.CyclistSpeedHorizontal, cyclist.CyclistSpeedUp, cyclist.CyclistSpeedDown, cyclist.Name, RandomFloat(0f, 20f), team));
            }
        }

        public IList<CyclistRaceEngine> FinishedCyclists()
        {
            return _finishedCyclists;
        }
    }
}
