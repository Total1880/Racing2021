using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.Services
{
    public class SeasonService : ISeasonService
    {
        private int tracknumber = 0;
        private ITrackService _trackService;
        private ICyclistService _cyclistService;
        private IRaceService _raceService;
        private IList<Track> _tracks;
        private IList<CyclistInRanking> _ranking;
        private IList<Cyclist> _cyclists;
        private bool _justStartedUp = true;

        public SeasonService(ICyclistService cyclistService, ITrackService trackService, IRaceService raceService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            _raceService = raceService;
            _ranking = new List<CyclistInRanking>();
        }

        public IList<CyclistInRanking> Ranking()
        {
            return _ranking;
        }

        public void NextRace()
        {
            if (_justStartedUp)
            {
                _tracks = _trackService.GetTracks();
                _cyclists = _cyclistService.GetCyclists();
                ResetRanking();
                _justStartedUp = false;
            }
            _raceService.StartRace(_tracks[tracknumber].TrackTiles, _cyclists);
            UpdateAfterRace();
        }

        private void UpdateAfterRace()
        {
            foreach (var cyclist in _raceService.FinishedCyclists())
            {
                _ranking.Where(c => c.Id == cyclist.Id).FirstOrDefault().TotalTime += cyclist.TotalTime;
            }

            _ranking = _ranking.OrderBy(c => c.TotalTime).ToList();

            tracknumber++;

            if (tracknumber >= _tracks.Count)
            {
                tracknumber = 0;
            }
        }

        private void ResetRanking()
        {
            if (_ranking != null)
            {
                _ranking.Clear();
            }
            
            foreach (var cyclist in _cyclists)
            {
                _ranking.Add(new CyclistInRanking(cyclist.Id, cyclist.Name, TimeSpan.Zero));
            }
        }
    }
}
