using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface ISeasonService
    {
        void NextRace();
        IList<CyclistInRanking> Ranking();
    }
}
