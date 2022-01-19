using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlavFramework;

namespace Racing2021.Services
{
    public class AIManagerService : OlavMessages, IAIManagerService
    {
        private ITeamService _teamService;
        private ICyclistService _cyclistService;

        public AIManagerService(ITeamService teamService, ICyclistService cyclistService)
        {
            _teamService = teamService;
            _cyclistService = cyclistService;
            AtEndOfSeason(Configuration.UserTeamId);
        }
        public void AtEndOfSeason(int playerTeamId)
        {
            var teams = _teamService.GetTeams();
            var cyclists = _cyclistService.GetCyclists();

            SelectStarterCyclists(cyclists, teams, playerTeamId);
            GiveCyclistsNewContract(cyclists.Where(c => c.TeamId >= 0).ToList());
            AddYoungCyclists(cyclists.Where(c => c.TeamId >= 0).ToList(), teams, playerTeamId);
            SelectStarterCyclists(cyclists, teams, playerTeamId);
        }

        public IList<string> GetAllMessages()
        {
            return Messages();
        }

        private void AddYoungCyclists(IList<Cyclist> cyclists, IList<Team> teams, int playerTeamId)
        {
            foreach (var team in teams)
            {
                if (team.Id == playerTeamId)
                    continue;

                while (cyclists.Where(c => c.TeamId == team.Id).Count() < Configuration.NumberOfCyclistsInTeam)
                {
                    var newCyclist = _cyclistService.CreateYoungCyclist(team.Id);
                    cyclists.Add(newCyclist);
                }
            }

            foreach (var message in _cyclistService.GetAllMessages())
            {
                AddMessage(message);
            }
        }

        private void SelectStarterCyclists(IList<Cyclist> cyclists, IList<Team> teams, int playerTeamId)
        {
            foreach (var team in teams)
            {
                if (team.Id == playerTeamId)
                    continue;

                var cyclistsInTeam = cyclists.Where(c => c.TeamId == team.Id).ToList();
                var cyclistsThatShouldRace = new List<Cyclist>();

                foreach (var cyclist in cyclistsInTeam)
                {
                    if (cyclistsThatShouldRace.Count() < Configuration.NumberOfCyclistsPerTeamForRace)
                    {
                        cyclistsThatShouldRace.Add(cyclist);
                        continue;
                    }

                    cyclistsThatShouldRace = cyclistsThatShouldRace.OrderBy(c => c.AllAttributes).ToList();

                    if (cyclist.CyclistSpeedCobblestones + cyclist.CyclistSpeedDown + cyclist.CyclistSpeedUp + cyclist.CyclistSpeedHorizontal > cyclistsThatShouldRace[0].CyclistSpeedCobblestones + cyclistsThatShouldRace[0].CyclistSpeedDown + cyclistsThatShouldRace[0].CyclistSpeedUp + cyclistsThatShouldRace[0].CyclistSpeedHorizontal)
                    {
                        cyclistsThatShouldRace.RemoveAt(0);
                        cyclistsThatShouldRace.Add(cyclist);
                    }
                }

                foreach (var cyclist in cyclistsInTeam)
                {
                    cyclist.SelectedForRace = cyclistsThatShouldRace.Any(c => c.Id == cyclist.Id);
                    _cyclistService.saveCyclist(cyclist);
                }
            }
        }
        private void GiveCyclistsNewContract(IList<Cyclist> cyclists)
        {
            var continueloop = false;
            var random = new Random();
            foreach ( var cyclist in cyclists)
            {
                if (cyclist.Contract.YearsLeft < 1 && cyclist.TeamId >= 0)
                {
                    continueloop = true;
                    if (cyclist.SelectedForRace || cyclist.Age < 30)
                    {
                        cyclist.Contract.YearsLeft = random.Next(1, 5);
                        _cyclistService.saveCyclist(cyclist);
                    }
                }
            }
            if (continueloop)
            {
                _cyclistService.ReleaseCyclistsWithNoContract();
            }
        }
    }
}
