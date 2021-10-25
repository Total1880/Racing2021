using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.Services
{
    public class DivisionService : IDivisionService
    {
        private IRepository<Division> _divisionRepository;

        public DivisionService(IRepository<Division> divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public IList<Division> CreateDivisions(IList<Division> divisions)
        {
            return _divisionRepository.Create(divisions);
        }

        public IList<Division> GetDivisions()
        {
            return _divisionRepository.Get();
        }
    }
}
