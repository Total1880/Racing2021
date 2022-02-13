using Racing2021.Models;
using Racing2021.Models.Enums;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing2021.Services
{
    public class ManagerService : IManagerService
    {
        IRepository<Manager> _managerRepository;
        IDataService _dataService;

        public ManagerService(IRepository<Manager> managerRepository, IDataService dataService)
        {
            _managerRepository = managerRepository;
            _dataService = dataService;
        }

        public IList<Manager> CreateManagers(IList<Manager> managerList)
        {
            return _managerRepository.Create(managerList);
        }

        public IList<Manager> GetManagers()
        {
            return _managerRepository.Get();
        }

        public Manager GenerateRandomManager(int teamId)
        {
            var managers = GetManagers();
            var nationality = _dataService.GetRandomNationality();
            var r = new Random();
            Array personalityValues = Enum.GetValues(typeof(ManagerPersonality));

            var manager = new Manager
            {
                Age = r.Next(40, 60),
                Name = _dataService.GetRandomFirstName(nationality) + " " + _dataService.GetRandomLastName(nationality),
                TeamId = teamId,
                Nationality = nationality,
                ManagerPersonality = (ManagerPersonality)r.Next(personalityValues.Length)
            };

            if (managers.Count() == 0)
            {
                manager.Id = 2;
            }
            else
            {
                manager.Id = managers.Max(m => m.Id) + 1;
            }

            

            return CreateManager(manager);
        }

        public Manager CreateManager(Manager manager)
        {
            var managers = GetManagers();
            managers.Add(manager);
            CreateManagers(managers);

            return manager;
        }

        public Manager CreateFreeManagerFromCyclist(Cyclist cyclist)
        {
            var manager = new Manager 
            { 
                Nationality = cyclist.Nationality ,
                Name = cyclist.Name ,
                TeamId = -1,
                Age = cyclist.Age ,
                Id = cyclist.Id
            };

            return manager;
        }
    }
}
