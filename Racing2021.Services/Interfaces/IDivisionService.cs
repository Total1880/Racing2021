using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface IDivisionService
    {
        IList<Division> GetDivisions();
        IList<Division> CreateDivisions(IList<Division> divisions);
    }
}
