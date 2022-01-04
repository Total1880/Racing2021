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
        private int _numberOfCyclistsInTeam = 3;
        private int _numberOfCyclistsPerTeamForRace = 2;

        public AIManagerService(ITeamService teamService, ICyclistService cyclistService)
        {
            _teamService = teamService;
            _cyclistService = cyclistService;
        }
        public void AtEndOfSeason(int playerTeamId)
        {
            var teams = _teamService.GetTeams();
            var cyclists = _cyclistService.GetCyclists();

            AddYoungCyclists(cyclists, teams, playerTeamId);
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

                while (cyclists.Where(c => c.TeamId == team.Id).Count() < _numberOfCyclistsInTeam)
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
                    if (cyclistsThatShouldRace.Count() < _numberOfCyclistsPerTeamForRace)
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

                if (cyclists.Where(c => c.TeamId == team.Id && c.SelectedForRace).Count() < _numberOfCyclistsPerTeamForRace)
                {
                    throw new Exception("Den AI heeft te weinig renners geselecteerd");
                }
            }
        }
    }
}
