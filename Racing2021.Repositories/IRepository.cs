using System.Collections.Generic;

namespace Racing2021.Repositories
{
    public interface IRepository<T>
    {
        IList<T> Create(IList<T> itemList);
        IList<T> Get();
        IList<T> Get(string filename);
    }
}
