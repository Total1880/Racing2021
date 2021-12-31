using System.Collections.Generic;

namespace Racing2021.Repositories
{
    public interface IDataRepository
    {
        IList<string> FirstNames();
        IList<string> LastNames();
    }
}
