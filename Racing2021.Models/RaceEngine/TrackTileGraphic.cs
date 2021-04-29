using Racing2021.Models.Enums;

namespace Racing2021.Models.RaceEngine
{
    public class TrackTileGraphic
    {
        private TrackTile _trackTile;
        private int _x;
        private int _y;

        public TrackTile TrackTile { get => _trackTile; }
        public int X { get => _x; }
        public int Y { get => _y; }

        public TrackTileGraphic(TrackTile trackTile, int x, int y)
        {
            _trackTile = trackTile;
            _x = x;
            _y = y;
        }
    }
}
