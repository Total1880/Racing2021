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
        private ICyclistService _cyclistService;

        public RaceService(IRaceEngineStart raceEngineStart, ICyclistService cyclistService)
        {
            _raceEngineStart = raceEngineStart;
            _cyclistService = cyclistService;
        }

        public void StartRace()
        {
            initializeTestCyclists();

            _raceEngineStart.Main(_cyclistsRaceEngine);
        }

        static float RandomFloat(float min, float max)
        {
            Random random = new Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        private void initializeTestCyclists()
        {
            _cyclistsRaceEngine = new List<CyclistRaceEngine>();
            var cyclists = _cyclistService.GetCyclists();

            foreach (var cyclist in cyclists)
            {
                _cyclistsRaceEngine.Add(new CyclistRaceEngine(cyclist.CyclistSpeedHorizontal, cyclist.CyclistSpeedUp, cyclist.CyclistSpeedDown, cyclist.Name, RandomFloat(0f, 20f)));
            }
            //var counter = 0;
            //do
            //{
            //    _cyclists.Add(new CyclistRaceEngine(RandomFloat(50f, 100f), RandomFloat(50f, 100f), RandomFloat(50f, 100f), "Cyclist " + counter));
            //    counter++;
            //} while (counter < 10);
        }
    }
}
