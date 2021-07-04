using Racing2021.Models.RaceEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Racing2021.RaceEngine.Interfaces
{
    public interface IRaceEngineStart
    {
        void Main(IList<CyclistRaceEngine> cyclists);
    }
}
