using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System;

namespace Racing2021.Services
{
    public class DataService : IDataService
    {
        IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public string GetRandomFirstName()
        {
            Random rnd = new Random();

            var listFirstNames = _dataRepository.FirstNames();

            int r = rnd.Next(listFirstNames.Count);

            return listFirstNames[r];
        }

        public string GetRandomLastName()
        {
            Random rnd = new Random();

            var listLastNames = _dataRepository.LastNames();

            int r = rnd.Next(listLastNames.Count);

            return listLastNames[r];
        }
    }
}
