using System.Collections.Generic;

namespace Racing2021.Repositories
{
    public interface IDataRepository
    {
        IList<string> FirstNames(string nationality);
        IList<string> LastNames(string nationality);
        IList<string> Nationalities();
    }
}
