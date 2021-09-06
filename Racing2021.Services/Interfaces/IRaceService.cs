using Racing2021.Models.RaceEngine;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface IRaceService
    {
        void StartRace();
        IList<CyclistRaceEngine> FinishedCyclists();
    }
}
