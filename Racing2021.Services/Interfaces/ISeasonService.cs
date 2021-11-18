using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface ISeasonService
    {
        void NextRace();
        void NextSeason();
        IList<CyclistInRanking> CyclistRanking();
        IList<TeamInRanking> TeamRanking();
        IList<CyclistInRanking> CyclistRanking(int divisionId);
        IList<TeamInRanking> TeamRanking(int divisionId);
        IList<Cyclist> Cyclists();
        public IList<string> Messages();
        bool IsSeasonEnded();
    }
}
