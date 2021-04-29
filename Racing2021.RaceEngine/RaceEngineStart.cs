using System;

namespace Racing2021.RaceEngine
{
    public static class RaceEngineStart
    {
        [STAThread]
        static void Main()
        {
            using (var game = new RaceEngine())
                game.Run();
        }
    }
}
