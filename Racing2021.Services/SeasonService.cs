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
        private IList<CyclistInRanking> _cyclistRanking;
        private IList<TeamInRanking> _teamRanking;
        private IList<Cyclist> _cyclists;
        private IList<Team> _teams;
        private bool _justStartedUp = true;

        public SeasonService(ICyclistService cyclistService, ITrackService trackService, IRaceService raceService, ITeamService teamService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            _raceService = raceService;
            _teamService = teamService;
            _cyclistRanking = new List<CyclistInRanking>();
            _teamRanking = new List<TeamInRanking>();
        }

        public IList<CyclistInRanking> CyclistRanking()
        {
            return _cyclistRanking;
        }

        public IList<TeamInRanking> TeamRanking()
        {
            return _teamRanking;
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
            UpdateCyclistRankingAfterRace();
        }

        private void UpdateCyclistRankingAfterRace()
        {
            foreach (var cyclist in _raceService.FinishedCyclists())
            {
                _cyclistRanking.Where(c => c.Id == cyclist.Id).FirstOrDefault().TotalTime += cyclist.TotalTime;

                UpdateTeamRankingAfterRace(cyclist.Team.Id, cyclist.TotalTime);
            }

            _cyclistRanking = _cyclistRanking.OrderBy(c => c.TotalTime).ToList();

            tracknumber++;
        }

        private void UpdateTeamRankingAfterRace(int teamId, TimeSpan time)
        {
            if(!_teamRanking.Any(t => t.Id == teamId))
            {
                throw new Exception(teamId + " teamid does not exist");
            }

            _teamRanking.Where(t => t.Id == teamId).FirstOrDefault().TotalTime += time;

            _teamRanking = _teamRanking.OrderBy(t => t.TotalTime).ToList();
        }

        private void ResetRanking()
        {
            if (_cyclistRanking != null)
            {
                _cyclistRanking.Clear();
            }

            if(_teamRanking != null)
            {
                _teamRanking.Clear();
            }
            
            foreach (var cyclist in _cyclists)
            {
                _cyclistRanking.Add(new CyclistInRanking(cyclist.Id, cyclist.Name, TimeSpan.Zero, _teams.Where(t => t.Id == cyclist.TeamId).FirstOrDefault().Name));

                if(!_teamRanking.Any(t => t.Id == cyclist.TeamId))
                {
                    _teamRanking.Add(new TeamInRanking(cyclist.TeamId, _teams.Where(t => t.Id == cyclist.TeamId).FirstOrDefault().Name, TimeSpan.Zero));
                }
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