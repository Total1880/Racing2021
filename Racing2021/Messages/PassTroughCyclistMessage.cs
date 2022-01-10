using Racing2021.Models;

namespace Racing2021.Messages
{

    public class PassTroughCyclistMessage
    {
        public Cyclist Cyclist { get; set; }

        public PassTroughCyclistMessage(Cyclist cyclist)
        {
            Cyclist = cyclist;
        }
    }
}
