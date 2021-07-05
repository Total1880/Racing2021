using Racing2021.Models;
using Racing2021.Repositories;
using Racing2021.Services.Interfaces;
using System.Collections.Generic;

namespace Racing2021.Services
{
    public class TrackService : ITrackService
    {
        IRepository<Track> _trackRepository;

        public TrackService(IRepository<Track> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public IList<Track> CreateTracks(IList<Track> tracks)
        {
            return _trackRepository.Create(tracks);
        }

        public IList<Track> GetTracks()
        {
            return _trackRepository.Get();
        }
    }
}
