using Racing2021.Models.Enums;
using Racing2021.Models.RaceEngine;
using System.Collections.Generic;

namespace Racing2021.RaceEngine.Interfaces
{
    public interface IRaceEngineStart
    {
        void Main(IList<CyclistRaceEngine> cyclists, IList<TrackTile> trackTileGraphics);
    }
}
