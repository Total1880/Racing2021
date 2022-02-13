using OlavFramework;
using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing2021.Services
{
    class CyclistService : OlavMessages, ICyclistService
    {
        private IRepository<Cyclist> _cyclistRepository;
        private IDataService _dataService;
        private ITeamService _teamService;

        public CyclistService(IRepository<Cyclist> cyclistRepository, IDataService dataService, ITeamService teamService)
        {
            _cyclistRepository = cyclistRepository;
            _dataService = dataService;
            _teamService = teamService;
        }

        public IList<Cyclist> CreateCyclists(IList<Cyclist> cyclists)
        {
            return _cyclistRepository.Create(cyclists);
        }

        public Cyclist CreateYoungCyclist(int teamId)
        {
            var cyclists = GetCyclists();
            var teamYouthAccomodation = _teamService.GetTeams().Where(t => t.Id == teamId).FirstOrDefault().YouthAccomodation;
            var randomNationality = _dataService.GetRandomNationality();
            Cyclist cyclist = new Cyclist
            {
                TeamId = teamId,
                Age = 16,
                CyclistSpeedHorizontalPotential = RandomFloat(50f, 100f) + teamYouthAccomodation,
                CyclistSpeedCobblestonesPotential = RandomFloat(50f, 100f) + teamYouthAccomodation,
                CyclistSpeedDownPotential = RandomFloat(50f, 100f) + teamYouthAccomodation,
                CyclistSpeedUpPotential = RandomFloat(50f, 100f) + teamYouthAccomodation,
                CyclistSpeedHorizontal = 50f,
                CyclistSpeedCobblestones = 50f,
                CyclistSpeedDown = 50f,
                CyclistSpeedUp = 50f,
                Nationality = randomNationality,
                Name = _dataService.GetRandomFirstName(randomNationality) + " " + _dataService.GetRandomLastName(randomNationality),
                Id = cyclists.Max(c => c.Id + 1),
                SelectedForRace = false
            };

            AddMessage($"{cyclist.Name} started at2 {_teamService.GetTeams().Where(t => t.Id == cyclist.TeamId).FirstOrDefault().Name}");

            cyclists.Add(cyclist);
            CreateCyclists(cyclists);
            return cyclist;
        }

        public IList<Cyclist> GetCyclists()
        {
            return _cyclistRepository.Get();
        }

        public Cyclist saveCyclist(Cyclist cyclist)
        {
            var cyclists = _cyclistRepository.Get();

            if (cyclists.Any(c => c.Id == cyclist.Id))
            {
                var toDelete = cyclists.Where(c => c.Id == cyclist.Id).FirstOrDefault();
                cyclists.Remove(toDelete);
            }

            cyclists.Add(cyclist);

            CreateCyclists(cyclists);

            return cyclist;
        }

        public IList<Cyclist> UpdateCyclistsEndOfSeason(int playerTeamId)
        {
            var cyclists = GetCyclists();
            var teams = _teamService.GetTeams();

            foreach (var cyclist in cyclists)
            {
                if (cyclist.Contract.YearsLeft > 0)
                    cyclist.Contract.YearsLeft--;

                if (cyclist.Age < 20)
                {
                    cyclist.CyclistSpeedDown += RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedHorizontal += RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedCobblestones += RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedUp += RandomFloat(0f, 10f);
                }
                else if (cyclist.Age < 25)
                {
                    cyclist.CyclistSpeedDown += RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedHorizontal += RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedCobblestones += RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedUp += RandomFloat(0f, 5f);
                }
                else if (cyclist.Age < 30)
                {
                    cyclist.CyclistSpeedDown += RandomFloat(0f, 2f);
                    cyclist.CyclistSpeedHorizontal += RandomFloat(0f, 2f);
                    cyclist.CyclistSpeedCobblestones += RandomFloat(0f, 2f);
                    cyclist.CyclistSpeedUp += RandomFloat(0f, 2f);
                }
                else if (cyclist.Age < 35)
                {
                    cyclist.CyclistSpeedDown -= RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedHorizontal -= RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedCobblestones -= RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedUp -= RandomFloat(0f, 5f);
                }
                else
                {
                    cyclist.CyclistSpeedDown -= RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedHorizontal -= RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedCobblestones -= RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedUp -= RandomFloat(0f, 10f);
                }

                cyclist.Age++;

                if (cyclist.Age > 30)
                {
                    if (RandomFloat(0f, 10f) < 2f)
                    {
                        DeleteCyclist(cyclist.Id);
                        AddMessage($"{cyclist.Name} has retired");
                        continue;
                    }
                }

                //Team training bonus
                var teamTrainingAccomodation = teams.Where(t => t.Id == cyclist.TeamId).FirstOrDefault().TrainingAccomodation;
                cyclist.CyclistSpeedDown += teamTrainingAccomodation;
                cyclist.CyclistSpeedHorizontal += teamTrainingAccomodation;
                cyclist.CyclistSpeedCobblestones += teamTrainingAccomodation;
                cyclist.CyclistSpeedUp += teamTrainingAccomodation;

                saveCyclist(cyclist);
            }

            return GetCyclists();
        }

        static float RandomFloat(float min, float max)
        {
            Random random = new Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        public IList<Cyclist> DeleteCyclist(int cyclistId)
        {
            return CreateCyclists(GetCyclists().Where(c => c.Id != cyclistId).ToList());
        }

        public IList<string> GetAllMessages()
        {
            return Messages();
        }
        public void ReleaseCyclistsWithNoContract()
        {
            var cyclists = GetCyclists().Where(c => c.Contract.YearsLeft < 1 && c.TeamId >= 0);
            foreach (var cyclist in cyclists)
            {
                cyclist.TeamId = -1;
                AddMessage(saveCyclist(cyclist).Name + " released.");
            }
        }
    }
}
