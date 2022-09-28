using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OlavFramework;
using Racing2021.Models.Enums;

namespace Racing2021.Services
{
    public class AIManagerService : OlavMessages, IAIManagerService
    {
        private ITeamService _teamService;
        private ICyclistService _cyclistService;
        private IManagerService _managerService;

        public AIManagerService(ITeamService teamService, ICyclistService cyclistService, IManagerService managerService)
        {
            _teamService = teamService;
            _cyclistService = cyclistService;
            _managerService = managerService;
            
            AtEndOfSeason(Configuration.UserTeamId);
        }
        public void AtEndOfSeason(int playerTeamId)
        {
            UpdateAccomodations();
            SelectStarterCyclists();
            GiveCyclistsNewContract();
            AddYoungCyclists();
            SelectStarterCyclists();
            ChooseTeamLeaders();
        }

        private void UpdateAccomodations()
        {
            var teams = _teamService.GetTeams();
            var managers = _managerService.GetManagers();
            foreach (var team in teams)
            {
                if (team.Id == Configuration.UserTeamId)
                    continue;

                var personalityManager = managers.Where(m => m.Id == team.ManagerId).FirstOrDefault().ManagerPersonality;
                if (personalityManager == ManagerPersonality.TrainingFocused || team.YouthAccomodation - team.TrainingAccomodation > 4)
                {
                    _teamService.InvestInTrainingAccomodation(team);
                }
                else if(personalityManager == ManagerPersonality.YouthFocused || team.TrainingAccomodation - team.YouthAccomodation > 4)
                {
                    _teamService.InvestInYouthAccomodation(team);
                }
                else
                {
                    throw new Exception("personality of manager is not found: " + personalityManager);
                }
            }

            if (teams.Count > 0)
            {
                _teamService.CreateTeams(teams);
            }
        }

        public IList<string> GetAllMessages()
        {
            return Messages();
        }

        private void AddYoungCyclists()
        {
            var teams = _teamService.GetTeams();
            var cyclists = _cyclistService.GetCyclists().Where(c => c.TeamId >= 0).ToList();

            foreach (var team in teams)
            {
                if (team.Id == Configuration.UserTeamId)
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

        private void SelectStarterCyclists()
        {
            var teams = _teamService.GetTeams();
            var cyclists = _cyclistService.GetCyclists();

            foreach (var team in teams)
            {
                if (team.Id == Configuration.UserTeamId)
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

        private void ChooseTeamLeaders()
        {
            var teams = _teamService.GetTeams();
            var cyclists = _cyclistService.GetCyclists();

            foreach (var team in teams)
            {
                //if (team.Id == Configuration.UserTeamId)
                //    continue;

                var cyclistsInTeam = cyclists.Where(c => c.TeamId == team.Id).OrderByDescending(c => c.AllAttributes).ToList();
                foreach (var cyclist in cyclistsInTeam)
                {
                    if (cyclist.Id == cyclistsInTeam[0].Id)
                    {
                        cyclist.TeamLeader = true;
                        _cyclistService.saveCyclist(cyclist);
                        continue;
                    }
                    cyclist.TeamLeader = false;
                    _cyclistService.saveCyclist(cyclist);
                }
            }
        }

        private void GiveCyclistsNewContract()
        {
            var cyclists = _cyclistService.GetCyclists().Where(c => c.TeamId >= 0).ToList();

            var continueloop = false;
            var random = new Random();
            foreach (var cyclist in cyclists)
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
