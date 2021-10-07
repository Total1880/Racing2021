using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System.Collections.Generic;

namespace Racing2021.Services
{
    public class TeamService : ITeamService
    {
        private IRepository<Team> _teamRepository;

        public TeamService(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public IList<Team> CreateTeams(IList<Team> teams)
        {
            return _teamRepository.Create(teams);
        }

        public IList<Team> GetTeams()
        {
            return _teamRepository.Get();
        }
    }
}
