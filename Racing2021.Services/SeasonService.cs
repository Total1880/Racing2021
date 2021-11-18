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
        private IList<string> _messages;
        private bool _justStartedUp = true;
        private bool _seasonHasEnded = false;
        private SaveGame _saveGame;
        private int _currentDivisionId;
        private int _playerTeamId = 4;

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

        public IList<CyclistInRanking> CyclistRanking(int divisionId)
        {
            var selectedDivision = _divisions.Where(d => d.Id == divisionId).FirstOrDefault();
            var teamsOfSelectedDivision = _teamRanking.Where(tm => selectedDivision.TeamsId.Contains(tm.Id)).ToList();
            return _cyclistRanking.Where(c => teamsOfSelectedDivision.Any(tm => tm.Name == c.TeamName)).ToList(); ;
        }

        public IList<TeamInRanking> TeamRanking(int divisionId)
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

        public IList<string> Messages()
        {
            return _messages;
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
            _raceService.StartRace(_tracks.Where(t => t.Id == _saveGame.NextRaceId).FirstOrDefault().TrackTiles, cyclistsForThisRace, teamsForThisRace, teamsForThisRace.Any(t => t.Id == _playerTeamId));
            UpdateAfterRace();
        }

        private void UpdateAfterRace()
        {
            _currentDivisionId = _saveGame.NextDivisionId;
            UpdateCyclistRankingAfterRace();
            if (_saveGame.NextDivisionId == _divisions.Last().Id)
            {
                _saveGame.NextDivisionId = _divisions[0].Id;
                if (_saveGame.NextRaceId == _tracks.Last().Id)
                {
                    _seasonHasEnded = true;
                }
                else
                {
                    _saveGame.NextRaceId = _tracks.SkipWhile(t => t.Id <= _saveGame.NextRaceId).FirstOrDefault().Id;
                }
            }
            else
            {
                _saveGame.NextDivisionId = _divisions.SkipWhile(d => d.Id <= _saveGame.NextDivisionId).FirstOrDefault().Id;
                NextRace();
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
            if (!_teamRanking.Any(t => t.Id == teamId))
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

            if (_teamRanking != null)
            {
                _teamRanking.Clear();
            }

            foreach (var cyclist in _cyclists)
            {
                _cyclistRanking.Add(new CyclistInRanking(cyclist.Id, cyclist.Name, TimeSpan.Zero, _teams.Where(t => t.Id == cyclist.TeamId).FirstOrDefault().Name));

                if (!_teamRanking.Any(t => t.Id == cyclist.TeamId))
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
            _messages = new List<string>();
            CalculateRelegationsAndPromotions();
            ResetRanking();
            UpdateCyclists();
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
                    division.TeamsId.Remove(promotedTeam);
                    changedTeams.Add(promotedTeam, division.Tier - 1);
                    _messages.Add($"{_teamRanking.Where(t => t.Id == promotedTeam).FirstOrDefault().Name} is promoted from {division.Name} to {_divisions.Where(d => d.Tier == division.Tier - 1).FirstOrDefault().Name}");
                }

                if (division.Tier != lowestTier)
                {
                    var relegatedTeam = TeamRanking(division.Id).Last().Id;
                    division.TeamsId.Remove(relegatedTeam);
                    changedTeams.Add(relegatedTeam, division.Tier + 1);
                    _messages.Add($"{_teamRanking.Where(t => t.Id == relegatedTeam).FirstOrDefault().Name} is relegated from {division.Name} to {_divisions.Where(d => d.Tier == division.Tier + 1).FirstOrDefault().Name}");
                }
            }

            foreach (var changedTeam in changedTeams)
            {
                _divisions.Where(d => d.Tier == changedTeam.Value).FirstOrDefault().TeamsId.Add(changedTeam.Key);
            }
        }

        private void UpdateCyclists()
        {
            foreach (var cyclist in _cyclists)
            {
                if (cyclist.Age < 20)
                {
                    cyclist.CyclistSpeedDown += RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedHorizontal += RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedCobblestones += RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedUp += RandomFloat(0f, 10f);
                }
                else if (cyclist.Age < 25)
                {
                    cyclist.CyclistSpeedDown += RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedHorizontal += RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedCobblestones += RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedUp += RandomFloat(0f, 5f);
                }
                else if (cyclist.Age < 30)
                {
                    cyclist.CyclistSpeedDown += RandomFloat(0f, 2f);
                    cyclist.CyclistSpeedHorizontal += RandomFloat(0f, 2f);
                    cyclist.CyclistSpeedCobblestones += RandomFloat(0f, 2f);
                    cyclist.CyclistSpeedUp += RandomFloat(0f, 2f);
                }
                else if (cyclist.Age < 35)
                {
                    cyclist.CyclistSpeedDown -= RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedHorizontal -= RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedCobblestones -= RandomFloat(0f, 5f);
                    cyclist.CyclistSpeedUp -= RandomFloat(0f, 5f);
                }
                else
                {
                    cyclist.CyclistSpeedDown -= RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedHorizontal -= RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedCobblestones -= RandomFloat(0f, 10f);
                    cyclist.CyclistSpeedUp -= RandomFloat(0f, 10f);
                }

                cyclist.Age++;

                if (cyclist.Age > 30)
                {
                    if (RandomFloat(0f, 10f) < 2f)
                    {
                        cyclist.Age = 16;
                        cyclist.CyclistSpeedDown = 50f;
                        cyclist.CyclistSpeedHorizontal = 50f;
                        cyclist.CyclistSpeedCobblestones = 50f;
                        cyclist.CyclistSpeedUp = 50f;
                        _messages.Add($"{cyclist.Name} has retired");
                    }
                }
            }
        }

        static float RandomFloat(float min, float max)
        {
            Random random = new Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }

        public IList<Cyclist> Cyclists()
        {
            return _cyclists;
        }
    }
}