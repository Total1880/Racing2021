using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface IManagerService
    {
        IList<Manager> CreateManagers(IList<Manager> managerList);
        IList<Manager> GetManagers();
        Manager GenerateRandomManager(int teamId);
        Manager CreateManager(Manager manager);
        Manager CreateFreeManagerFromCyclist(Cyclist cyclist);
    }
}
