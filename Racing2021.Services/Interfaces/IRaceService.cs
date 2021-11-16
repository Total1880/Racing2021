using Racing2021.Models;
using Racing2021.Models.Enums;
using Racing2021.Models.RaceEngine;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface IRaceService
    {
        void StartRace(IList<TrackTile> trackTileGraphics, IList<Cyclist> cyclists, IList<Team> teams, bool playerWatches);
        IList<CyclistRaceEngine> FinishedCyclists();
    }
}
