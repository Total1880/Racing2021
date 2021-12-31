using Racing2021.Models.Enums;
using System.Collections.Generic;

namespace Racing2021.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TrackTile> TrackTiles { get; set; }
        public int FirstPlacePrizeMoney { get; set; }

        public Track()
        {
            TrackTiles = new List<TrackTile>();
        }
    }
}
