using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface ICyclistService
    {
        IList<Cyclist> GetCyclists();
        IList<Cyclist> CreateCyclists(IList<Cyclist> cyclists);
        Cyclist CreateYoungCyclist(int teamId);
    }
}
