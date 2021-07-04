using Racing2021.Models.RaceEngine;
using Racing2021.RaceEngine.Interfaces;
using System.Collections.Generic;

namespace Racing2021.RaceEngine
{
    public class RaceEngineStart : IRaceEngineStart
    {
        private static IList<CyclistRaceEngine> _cyclists;
        private static IList<CyclistRaceEngine> _finishedCyclists;

        static void Main()
        {
            using (var game = new RaceEngine())
            {
                game.InitializeCyclists(_cyclists);
                game.Run();
                _finishedCyclists = game.GetFinishedCyclists();
            }
        }

        void IRaceEngineStart.Main(IList<CyclistRaceEngine> cyclists)
        {
            _cyclists = cyclists;

            Main();
        }
    }
}
