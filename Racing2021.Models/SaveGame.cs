using System;
using System.Collections.Generic;
using System.Text;

namespace Racing2021.Models
{
    public class SaveGame
    {
        public int Id;
        public int NextRaceId;
        public int NextDivisionId;
        public int PlayerTeamId = 4;
        public Manager PlayerManager;

        public SaveGame()
        {
            PlayerManager = new Manager
            {
                Name = "Olav Hendrickx",
                Nationality = "Belgian",
                Id = 1,
                Age = 34,
                TeamId = PlayerTeamId
            };
        }
    }
}
