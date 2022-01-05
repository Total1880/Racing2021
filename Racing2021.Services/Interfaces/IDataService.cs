using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface IDataService
    {
        string GetRandomFirstName(string nationality);
        string GetRandomLastName(string nationality);
        string GetRandomNationality();
        IList<string> GetAllNationalities();
    }
}
