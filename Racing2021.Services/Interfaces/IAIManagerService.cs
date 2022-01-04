using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface IAIManagerService
    {
        void AtEndOfSeason(int playerTeamId);
        IList<string> GetAllMessages();

    }
}
