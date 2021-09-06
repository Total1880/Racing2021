using Racing2021.Models;
using Racing2021.Models.RaceEngine;
using Racing2021.RaceEngine.Interfaces;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Racing2021.Services
{
    public class RaceService : IRaceService
    {
        private IRaceEngineStart _raceEngineStart;
        private IList<CyclistRaceEngine> _cyclistsRaceEngine;
        private IList<CyclistRaceEngine> _finishedCyclists;
        private IList<Track> _tracks;
        private ICyclistService _cyclistService;
        private ITrackService _trackService;
        private int tracknumber = 0;

        public RaceService(IRaceEngineStart raceEngineStart, ICyclistService cyclistService, ITrackService trackService)
        {
            _raceEngineStart = raceEngineStart;
            _cyclistService = cyclistService;
            _trackService = trackService;
            _tracks = _trackService.GetTracks();
        }

        public void StartRace()
        {
            InitializeTestCyclists();

            _raceEngineStart.Main(_cyclistsRaceEngine, _tracks[tracknumber].TrackTiles);
            _finishedCyclists = _raceEngineStart.FinishedCyclists();
            tracknumber++;
            tracknumber = tracknumber >= _tracks.Count ? 0 : tracknumber;
        }

        static float RandomFloat(float min, float max)
        {
            Random random = new Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        private void InitializeTestCyclists()
        {
            _cyclistsRaceEngine = new List<CyclistRaceEngine>();
            var cyclists = _cyclistService.GetCyclists();

            foreach (var cyclist in cyclists)
            {
                _cyclistsRaceEngine.Add(new CyclistRaceEngine(cyclist.CyclistSpeedHorizontal, cyclist.CyclistSpeedUp, cyclist.CyclistSpeedDown, cyclist.Name, RandomFloat(0f, 20f)));
            }
        }

        public IList<CyclistRaceEngine> FinishedCyclists()
        {
            return _finishedCyclists;
        }
    }
}
