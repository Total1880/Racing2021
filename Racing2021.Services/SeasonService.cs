﻿using Racing2021.Models;
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
        private ITrackService _trackService;
        private ICyclistService _cyclistService;
        private IRaceService _raceService;
        private ITeamService _teamService;
        private IDivisionService _divisionService;
        private IList<Track> _tracks;
        private IList<CyclistInRanking> _cyclistRanking;
        private IList<TeamInRanking> _teamRanking;
        private IList<Cyclist> _cyclists;
        private IList<Team> _teams;
        private IList<Division> _divisions;
        private bool _justStartedUp = true;
        private bool _seasonHasEnded = false;
        private SaveGame _saveGame;
        private int _currentDivisionId;

        public SaveGame SaveGameData => _saveGame;

        public SeasonService(ICyclistService cyclistService, ITrackService trackService, IRaceService raceService, ITeamService teamService, IDivisionService divisionService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            _raceService = raceService;
            _teamService = teamService;
            _divisionService = divisionService;
            _cyclistRanking = new List<CyclistInRanking>();
            _teamRanking = new List<TeamInRanking>();
            _saveGame = new SaveGame() { Id = 0 };
        }

        private IList<CyclistInRanking> CyclistRanking(int divisionId)
        {
            var selectedDivision = _divisions.Where(d => d.Id == divisionId).FirstOrDefault();
            var teamsOfSelectedDivision = _teamRanking.Where(tm => selectedDivision.TeamsId.Contains(tm.Id)).ToList();
            return _cyclistRanking.Where(c => teamsOfSelectedDivision.Any(tm => tm.Name == c.TeamName)).ToList(); ;
        }

        private IList<TeamInRanking> TeamRanking(int divisionId)
        {
            var selectedDivision = _divisions.Where(d => d.Id == divisionId).FirstOrDefault();
            return _teamRanking.Where(tm => selectedDivision.TeamsId.Contains(tm.Id)).ToList();
        }

        public IList<CyclistInRanking> CyclistRanking()
        {
           return CyclistRanking(_currentDivisionId);
        }

        public IList<TeamInRanking> TeamRanking()
        {
            return TeamRanking(_currentDivisionId);
        }

        public void NextRace()
        {
            if (_justStartedUp)
            {
                _tracks = _trackService.GetTracks();
                _cyclists = _cyclistService.GetCyclists();
                _teams = _teamService.GetTeams();
                _divisions = _divisionService.GetDivisions();
                ResetRanking();
                _justStartedUp = false;
            }

            if (!_tracks.Any(t => t.Id == _saveGame.NextRaceId))
            {
                throw new Exception($"{_saveGame.NextRaceId}: This race id does not exist");
            }

            if (!_divisions.Any(d => d.Id == _saveGame.NextDivisionId))
            {
                throw new Exception($"{_saveGame.NextDivisionId}: This division id does not exist");
            }

            var divisionForThisRace = _divisions.Where(d => d.Id == _saveGame.NextDivisionId).FirstOrDefault();
            var teamsForThisRace = _teams.Where(tm => divisionForThisRace.TeamsId.Contains(tm.Id)).ToList();
            var cyclistsForThisRace = _cyclists.Where(c => teamsForThisRace.Any(tm => tm.Id == c.TeamId)).ToList();
            _raceService.StartRace(_tracks.Where(t => t.Id == _saveGame.NextRaceId).FirstOrDefault().TrackTiles, cyclistsForThisRace, teamsForThisRace);
            UpdateAfterRace();
        }

        private void UpdateAfterRace()
        {
            _currentDivisionId = _saveGame.NextDivisionId;
            UpdateCyclistRankingAfterRace();
            if(_saveGame.NextDivisionId == _divisions.Last().Id)
            {
                _saveGame.NextDivisionId = _divisions[0].Id;
                if (_saveGame.NextRaceId == _tracks.Last().Id)
                {
                    //_saveGame.NextRaceId = _tracks[0].Id;
                    _seasonHasEnded = true;
                }
                else
                {
                    //_saveGame.NextRaceId = _tracks.Where(t => t.Id == _saveGame.NextRaceId + 1).FirstOrDefault().Id;
                    _saveGame.NextRaceId = _tracks.SkipWhile(t => t.Id <= _saveGame.NextRaceId).FirstOrDefault().Id;
                    
                }
            }
            else
            {
                //_saveGame.NextDivisionId = _divisions.Where(d => d.Id == _saveGame.NextDivisionId + 1).FirstOrDefault().Id;
                _saveGame.NextDivisionId = _divisions.SkipWhile(d => d.Id <= _saveGame.NextDivisionId).FirstOrDefault().Id;

            }
        }

        private void UpdateCyclistRankingAfterRace()
        {
            foreach (var cyclist in _raceService.FinishedCyclists())
            {
                _cyclistRanking.Where(c => c.Id == cyclist.Id).FirstOrDefault().TotalTime += cyclist.TotalTime;

                UpdateTeamRankingAfterRace(cyclist.Team.Id, cyclist.TotalTime);
            }

            _cyclistRanking = _cyclistRanking.OrderBy(c => c.TotalTime).ToList();
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
            //return tracknumber >= _tracks.Count;
            return _seasonHasEnded;
        }

        public void NextSeason()
        {
            CalculateRelegationsAndPromotions();
            ResetRanking();
            _saveGame.NextDivisionId = _divisions[0].Id;
            _saveGame.NextRaceId = _tracks[0].Id;
            _seasonHasEnded = false;
        }

        private void CalculateRelegationsAndPromotions()
        {
            var lowestTier = _divisions.Max(d => d.Tier);
            var changedTeams = new Dictionary<int, int>();

            foreach (var division in _divisions)
            {
                if (division.Tier > 1)
                {
                    var promotedTeam = TeamRanking(division.Id).First().Id;
                    //_divisions.Where(d => d.Tier == division.Tier - 1).FirstOrDefault().TeamsId.Add(promotedTeam);
                    division.TeamsId.Remove(promotedTeam);
                    changedTeams.Add(promotedTeam, division.Tier - 1);
                }

                if (division.Tier != lowestTier)
                {
                    var relegatedTeam = TeamRanking(division.Id).Last().Id;
                    //_divisions.Where(d => d.Tier == division.Tier + 1).FirstOrDefault().TeamsId.Add(relegatedTeam);
                    division.TeamsId.Remove(relegatedTeam);
                    changedTeams.Add(relegatedTeam, division.Tier + 1);
                }
            }

            foreach (var changedTeam in changedTeams)
            {
                _divisions.Where(d => d.Tier == changedTeam.Value).FirstOrDefault().TeamsId.Add(changedTeam.Key);
            }
        }
    }
}