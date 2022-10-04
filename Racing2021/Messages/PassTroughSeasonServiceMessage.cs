using Racing2021.Services;
using Racing2021.Services.Interfaces;

namespace Racing2021.Messages
{
    public class PassTroughSeasonServiceMessage
    {
        public ISeasonService SeasonService { get; set; }

        public PassTroughSeasonServiceMessage(ISeasonService seasonService)
        {
            SeasonService = seasonService;
        }
    }
}
