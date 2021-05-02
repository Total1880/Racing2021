using Racing2021.RaceEngine.Interfaces;
using System;

namespace Racing2021.RaceEngine
{
    public class RaceEngineStart : IRaceEngineStart
    {
        static void Main()
        {
            using (var game = new RaceEngine())
                game.Run();
        }

        void IRaceEngineStart.Main()
        {
            Main();
        }
    }
}
