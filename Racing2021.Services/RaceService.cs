using Racing2021.RaceEngine.Interfaces;
using Racing2021.Services.Interfaces;
using System;

namespace Racing2021.Services
{
    public class RaceService : IRaceService
    {
        private IRaceEngineStart _raceEngineStart;

        public RaceService(IRaceEngineStart raceEngineStart)
        {
            _raceEngineStart = raceEngineStart;
        }

        public void StartRace()
        {
            _raceEngineStart.Main();
        }
    }
}
