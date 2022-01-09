using OlavFramework;

namespace Racing2021.Models
{
    public class SaveGame
    {
        public int Id;
        public int NextRaceId;
        public int NextDivisionId;
        public int PlayerTeamId;
        public Manager PlayerManager;

        public SaveGame()
        {
            PlayerTeamId = Configuration.UserTeamId;
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
