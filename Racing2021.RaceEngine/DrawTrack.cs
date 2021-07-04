using Racing2021.Models.Enums;
using Racing2021.Models.RaceEngine;
using System;
using System.Collections.Generic;

namespace Racing2021.RaceEngine
{
    public static class DrawTrack
    {
        public static List<TrackTileGraphic> Track(IList<TrackTile> trackTiles, int startPositionX)
        {
            var track = new List<TrackTileGraphic>();
            int x = startPositionX;
            int y = 600;

            foreach (var tile in trackTiles)
            {


                switch (tile)
                {
                    case TrackTile.Horizontal:
                        track.Add(new TrackTileGraphic(tile, x, y));

                        x += TextureParameters.Horizontal;
                        break;
                    case TrackTile.Up:
                        y -= TextureParameters.UpDown - TextureParameters.Horizontal;

                        track.Add(new TrackTileGraphic(tile, x, y));

                        x += TextureParameters.UpDown;
                        break;
                    case TrackTile.Down:
                        track.Add(new TrackTileGraphic(tile, x, y));

                        x += TextureParameters.UpDown;
                        y += TextureParameters.UpDown - TextureParameters.Horizontal;
                        break;
                    default:
                        throw Exception(tile + " not found");
                }

            }

            return track;
        }

        private static Exception Exception(string message)
        {
            throw new NotImplementedException(message);
        }
    }
}
