using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System.Collections.Generic;

namespace Racing2021.Services
{
    class CyclistService : ICyclistService
    {
        private IRepository<Cyclist> _cyclistRepository;

        public CyclistService(IRepository<Cyclist> cyclistRepository)
        {
            _cyclistRepository = cyclistRepository;
        }

        public IList<Cyclist> CreateCyclists(IList<Cyclist> cyclists)
        {
            return _cyclistRepository.Create(cyclists);
        }

        public IList<Cyclist> GetCyclists()
        {
            return _cyclistRepository.Get();
        }
    }
}
