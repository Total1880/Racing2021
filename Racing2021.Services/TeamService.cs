using OlavFramework;
using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public void InvestInTrainingAccomodation(Team team)
        {
            var priceOfInvestment = (team.TrainingAccomodation + 1) * Configuration.BasePriceAccomodation;
            if (priceOfInvestment <= team.Money)
            {
                team.Money -= priceOfInvestment;
                team.TrainingAccomodation++;
            }
        }

        public void InvestInYouthAccomodation(Team team)
        {
            var priceOfInvestment = (team.YouthAccomodation + 1) * Configuration.BasePriceAccomodation;
            if (priceOfInvestment <= team.Money)
            {
                team.Money -= priceOfInvestment;
                team.YouthAccomodation++;
            }
        }

        public Team SaveTeam(Team team)
        {
            var teams = GetTeams();

            if (teams.Any(t => t.Id == team.Id))
            {
                var toDelete = teams.Where(c => c.Id == team.Id).FirstOrDefault();
                teams.Remove(toDelete);
            }

            teams.Add(team);

            CreateTeams(teams);

            return team;
        }
    }
}
