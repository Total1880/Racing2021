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
        private ITeamService _teamService;
        private IList<Track> _tracks;
        private IList<CyclistInRanking> _ranking;
        private IList<Cyclist> _cyclists;
        private IList<Team> _teams;
        private bool _justStartedUp = true;

        public SeasonService(ICyclistService cyclistService, ITrackService trackService, IRaceService raceService, ITeamService teamService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            _raceService = raceService;
            _teamService = teamService;
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
                _teams = _teamService.GetTeams();
                ResetRanking();
                _justStartedUp = false;
            }

            if (tracknumber > _tracks.Count)
            {
                throw new Exception("Tracknumber is greater than the number of tracks");
            }

            _raceService.StartRace(_tracks[tracknumber].TrackTiles, _cyclists, _teams);
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
        }

        private void ResetRanking()
        {
            if (_ranking != null)
            {
                _ranking.Clear();
            }
            
            foreach (var cyclist in _cyclists)
            {
                _ranking.Add(new CyclistInRanking(cyclist.Id, cyclist.Name, TimeSpan.Zero, _teams.Where(t => t.Id == cyclist.TeamId).FirstOrDefault().Name));
            }
        }

        public bool IsSeasonEnded()
        {
            return tracknumber >= _tracks.Count;
        }

        public void NextSeason()
        {
            ResetRanking();
            tracknumber = 0;
        }
    }
}