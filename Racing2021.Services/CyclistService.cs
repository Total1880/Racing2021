using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Racing2021.Services
{
    class CyclistService : ICyclistService
    {
        private IRepository<Cyclist> _cyclistRepository;
        private IDataService _dataService;

        public CyclistService(IRepository<Cyclist> cyclistRepository, IDataService dataService)
        {
            _cyclistRepository = cyclistRepository;
            _dataService = dataService;
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
    }
}
