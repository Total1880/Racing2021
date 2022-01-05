using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Racing2021.Services
{
    public class DataService : IDataService
    {
        IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public string GetRandomFirstName(string nationality)
        {
            Random rnd = new Random();

            var listFirstNames = _dataRepository.FirstNames(nationality);

            int r = rnd.Next(listFirstNames.Count);

            return listFirstNames[r];
        }

        public string GetRandomLastName(string nationality)
        {
            Random rnd = new Random();

            var listLastNames = _dataRepository.LastNames(nationality);

            int r = rnd.Next(listLastNames.Count);

            return listLastNames[r];
        }

        public string GetRandomNationality()
        {
            Random rnd = new Random();

            var listNationalities = _dataRepository.Nationalities();

            int r = rnd.Next(listNationalities.Count);

            return listNationalities[r];
        }

        public IList<string> GetAllNationalities()
        {
            return _dataRepository.Nationalities();
        }
    }
}
