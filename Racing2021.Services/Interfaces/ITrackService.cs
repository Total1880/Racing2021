using Racing2021.Models;
using System.Collections.Generic;

namespace Racing2021.Services.Interfaces
{
    public interface ITrackService
    {
        IList<Track> GetTracks();
        IList<Track> CreateTracks(IList<Track> tracks);
    }
}
