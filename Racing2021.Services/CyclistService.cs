﻿using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing2021.Services
{
    class CyclistService : ICyclistService
    {
        private IRepository<Cyclist> _cyclistRepository;
        private IDataService _dataService;
        private ITeamService _teamService;
        private IList<string> _messages;

        public CyclistService(IRepository<Cyclist> cyclistRepository, IDataService dataService, ITeamService teamService)
        {
            _cyclistRepository = cyclistRepository;
            _dataService = dataService;
            _teamService = teamService;
            _messages = new List<string>();
        }

        public IList<Cyclist> CreateCyclists(IList<Cyclist> cyclists)
        {
            return _cyclistRepository.Create(cyclists);
        }

        public Cyclist CreateYoungCyclist(int teamId)
        {
            var cyclists = GetCyclists();
            Cyclist cyclist = new Cyclist
            {
                TeamId = teamId,
                Age = 16,
                CyclistSpeedHorizontal = 50f,
                CyclistSpeedCobblestones = 50f,
                CyclistSpeedDown = 50f,
                CyclistSpeedUp = 50f,
                Name = _dataService.GetRandomFirstName() + " " + _dataService.GetRandomLastName(),
                Id = cyclists.Max(c => c.Id + 1)
            };

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

            foreach (var cyclist in cyclists)
            {
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
                        if (cyclist.TeamId == playerTeamId)
                        {
                            DeleteCyclist(cyclist.Id);
                            _messages.Add($"{cyclist.Name} has retired");
                            continue;
                        }
                        cyclist.Age = 16;
                        cyclist.CyclistSpeedDown = 50f;
                        cyclist.CyclistSpeedHorizontal = 50f;
                        cyclist.CyclistSpeedCobblestones = 50f;
                        cyclist.CyclistSpeedUp = 50f;
                        _messages.Add($"{cyclist.Name} has retired");
                        cyclist.Name = _dataService.GetRandomFirstName() + " " + _dataService.GetRandomLastName();
                        _messages.Add($"{cyclist.Name} started at {_teamService.GetTeams().Where(t => t.Id == cyclist.TeamId).FirstOrDefault().Name}");
                    }
                }

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
            var messages = _messages;
            _messages = new List<string>();
            return messages;
        }
    }
}
