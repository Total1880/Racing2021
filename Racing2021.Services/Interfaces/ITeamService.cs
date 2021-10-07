using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface ITeamService
    {
        IList<Team> GetTeams();
        IList<Team> CreateTeams(IList<Team> teams);
    }
}
