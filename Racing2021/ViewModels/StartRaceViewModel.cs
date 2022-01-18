using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Models;
using Racing2021.Models.RaceEngine;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Racing2021.ViewModels
{
    public class StartRaceViewModel : ViewModelBase
    {
        private ICyclistService _cyclistService;
        private ITrackService _trackService;
        private ISeasonService _seasonService;
        private ITeamService _teamService;
        private IDivisionService _divisionService;
        private IManagerService _managerService;
        private RelayCommand _startRaceCommand;
        private RelayCommand _nextSeasonCommand;
        private ObservableCollection<CyclistInRanking> _cyclistRanking;
        private ObservableCollection<TeamInRanking> _teamRanking;
        private ObservableCollection<Cyclist> _cyclistsWithStats;
        private ObservableCollection<Division> _divisions;
        private Visibility _showNextRaceButton;
        private Visibility _showEndSeasonButton;
        private TeamInRanking _selectedTeam;
        private CyclistInRanking _selectedCyclist;
        private Division _selectedDivision;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);
        public RelayCommand NextSeasonCommand => _nextSeasonCommand ??= new RelayCommand(NextSeason);

        public ObservableCollection<CyclistInRanking> CyclistRanking { get => _cyclistRanking; set { _cyclistRanking = value; RaisePropertyChanged(); } }
        public ObservableCollection<TeamInRanking> TeamRanking { get => _teamRanking; set { _teamRanking = value; RaisePropertyChanged(); } }
        public ObservableCollection<Cyclist> CyclistsWithStats { get => _cyclistsWithStats; set { _cyclistsWithStats = value; RaisePropertyChanged(); } }
        public ObservableCollection<Division> Divisions { get => _divisions; set { _divisions = value; RaisePropertyChanged(); } }

        public Visibility ShowNextRaceButton { get => _showNextRaceButton; set { _showNextRaceButton = value; RaisePropertyChanged(); } }
        public Visibility ShowEndSeasonButton { get => _showEndSeasonButton; set { _showEndSeasonButton = value; RaisePropertyChanged(); } }

        public TeamInRanking SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                if (_selectedTeam != null)
                    RefreshTeamListView();
                RaisePropertyChanged();
            }
        }

        public CyclistInRanking SelectedCyclist
        {
            get => _selectedCyclist;
            set
            {
                _selectedCyclist = value;
                if (_selectedCyclist != null)
                    RefreshTeamListViewWithCyclist();
                RaisePropertyChanged();
            }
        }

        public Division SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                if (_selectedDivision != null)
                {
                    RefreshRankings();
                }
                RaisePropertyChanged();
            }
        }

        public StartRaceViewModel(ICyclistService cyclistService, ITrackService trackService, ISeasonService seasonService, ITeamService teamService, IDivisionService divisionService, IManagerService managerService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            _seasonService = seasonService;
            _teamService = teamService;
            _divisionService = divisionService;
            _managerService = managerService;

            if (_divisionService.GetDivisions().Count < 1)
                CreateDivisions();

            if (_teamService.GetTeams().Count < 1)
                CreateTeams();

            if (_cyclistService.GetCyclists().Count < 1)
                CreateCyclists();

            if (_trackService.GetTracks().Count < 1)
                CreateTracks();

            ShowNextRaceButton = Visibility.Visible;
            ShowEndSeasonButton = Visibility.Collapsed;
            Divisions = new ObservableCollection<Division>(_divisionService.GetDivisions());
        }

        private void StartRace()
        {
            _seasonService.NextRace();

            var messages = _seasonService.Messages();
            if (messages != null && messages.Count > 0)
            {
                MessageBox.Show(string.Join(Environment.NewLine, messages));
                return;
            }

            CyclistRanking = new ObservableCollection<CyclistInRanking>(_seasonService.CyclistRanking());
            TeamRanking = new ObservableCollection<TeamInRanking>(_seasonService.TeamRanking());

            if (_seasonService.IsSeasonEnded())
            {
                ShowNextRaceButton = Visibility.Collapsed;
                ShowEndSeasonButton = Visibility.Visible;
            }

            SelectedDivision = Divisions.Where(d => d.TeamsId.Contains(_seasonService.PlayerTeamId())).FirstOrDefault();

            CyclistsWithStats = null;
        }

        private void NextSeason()
        {
            _seasonService.NextSeason();
            CyclistRanking.Clear();
            TeamRanking.Clear();
            ShowNextRaceButton = Visibility.Visible;
            ShowEndSeasonButton = Visibility.Collapsed;

            CyclistsWithStats = null;

            MessageBox.Show(string.Join(Environment.NewLine, _seasonService.Messages()));
            Divisions = new ObservableCollection<Division>(_divisionService.GetDivisions());
        }

        private void CreateDivisions()
        {
            var newDivisions = new List<Division>();

            newDivisions.Add(new Division(0, 1, "Division 1", 9000));
            newDivisions.Add(new Division(1, 2, "Division 2", 8000));
            newDivisions.Add(new Division(2, 3, "Division 3", 7000));

            newDivisions[0].TeamsId.Add(0);
            newDivisions[0].TeamsId.Add(1);
            newDivisions[0].TeamsId.Add(2);
            newDivisions[1].TeamsId.Add(3);
            newDivisions[1].TeamsId.Add(4);
            newDivisions[1].TeamsId.Add(5);
            newDivisions[2].TeamsId.Add(6);
            newDivisions[2].TeamsId.Add(7);
            newDivisions[2].TeamsId.Add(8);

            _divisionService.CreateDivisions(newDivisions);
        }

        private void CreateCyclists()
        {
            var cyclists = new List<Cyclist>();
            var random = new Random();

            cyclists.Add(new Cyclist(0, 80f, 70f, 100f, 95f, "Tadej Pogacar", 0, "none", 22));
            cyclists.Add(new Cyclist(1, 75f, 60f, 80f, 90f, "Rui Costa", 0, "none", 35));
            cyclists.Add(new Cyclist(2, 95f, 92f, 90f, 90f, "Wout Van Aert", 1, "Belgian", 26));
            cyclists.Add(new Cyclist(3, 90f, 85f, 70f, 85f, "Nathan Van Hooydonck", 1, "Belgian", 26));
            cyclists.Add(new Cyclist(4, 90f, 85f, 95f, 80f, "Remco Evenepoel", 2,"Belgian", 21));
            cyclists.Add(new Cyclist(5, 85f, 82f, 75f, 75f, "Tim Declercq", 2, "Belgian", 32));
            cyclists.Add(new Cyclist(6, 100f, 91f, 85f, 90f, "Mathieu Van Der Poel", 3, "Netherlands", 26));
            cyclists.Add(new Cyclist(7, 95f, 84f, 65f, 85f, "Tim Merlier", 3, "Belgian", 32));
            cyclists.Add(new Cyclist(8, 70f, 75f, 70f, 70f, "Olav Hendrickx", 4, "Belgian", 33));
            cyclists.Add(new Cyclist(9, 75f, 80f, 65f, 70f, "Arne Hendrickx", 4, "Belgian", 35));
            cyclists.Add(new Cyclist(10, 85f, 90f, 75f, 80f, "Sander Delmeire", 5, "Belgian", 34));
            cyclists.Add(new Cyclist(11, 80f, 85f, 70f, 75f, "Loic Vandenbroucke", 5, "Belgian", 26));

            foreach (var cyclist in cyclists)
            {
                cyclist.Contract = new Contract
                {
                    YearsLeft = random.Next(1, 5),
                    SalaryPerYear = 0
                };
            }

            _cyclistService.CreateCyclists(cyclists);

            for (int i = 0; i < 3; i++)
            {
                _cyclistService.CreateYoungCyclist(6);
                _cyclistService.CreateYoungCyclist(7);
                _cyclistService.CreateYoungCyclist(8);
            }
        }

        private void CreateTeams()
        {
            var teams = new List<Team>();
            var savegame = new SaveGame();

            teams.Add(new Team(0, "Team A", TextureNames.CyclistBlue, 8000));
            teams.Add(new Team(1, "Team B", TextureNames.CyclistGreen, 8000));
            teams.Add(new Team(2, "Team C", TextureNames.CyclistRed, 8000));
            teams.Add(new Team(3, "Team D", TextureNames.CyclistRoseGrey, 7000));
            teams.Add(new Team(4, "Team E", TextureNames.CyclistYellow, 7000));
            teams.Add(new Team(5, "Team F", TextureNames.CyclistBlackYellow, 7000));
            teams.Add(new Team(6, "Team G", TextureNames.CyclistRoseGrey, 6000));
            teams.Add(new Team(7, "Team H", TextureNames.CyclistYellow, 6000));
            teams.Add(new Team(8, "Team I", TextureNames.CyclistBlackYellow, 6000));

            teams.Where(t => t.Id == savegame.PlayerTeamId).FirstOrDefault().ManagerId = savegame.PlayerManager.Id;

            foreach (var team in teams)
            {
                if (team.ManagerId == 0)
                {
                    var newManager = _managerService.GenerateRandomManager(team.Id);
                    team.ManagerId = newManager.Id;
                }
            }

            _teamService.CreateTeams(teams);
        }

        private void CreateTracks()
        {
            var tracks = new List<Track>();
            var random = new Random();

            var counter = 0;
            tracks.Add(new Track());
            tracks[0].Id = 0;
            tracks[0].Name = "track 1";
            tracks[0].FirstPlacePrizeMoney = 1000;

            do
            {
                tracks[0].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 4));
                counter++;
            } while (counter < 10);

            counter = 0;
            tracks.Add(new Track());
            tracks[1].Id = 1;
            tracks[1].Name = "track 2";
            tracks[1].FirstPlacePrizeMoney = 2000;


            do
            {
                tracks[1].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 4));
                counter++;
            } while (counter < 10);

            counter = 0;
            tracks.Add(new Track());
            tracks[2].Id = 2;
            tracks[2].Name = "track 3";
            tracks[2].FirstPlacePrizeMoney = 3000;

            do
            {
                tracks[2].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 4));
                counter++;
            } while (counter < 10);

            _trackService.CreateTracks(tracks);
        }

        private void RefreshTeamListView()
        {
            CyclistsWithStats = new ObservableCollection<Cyclist>(_seasonService.Cyclists().Where(c => c.TeamId == SelectedTeam.Id));
            foreach (var cyclist in CyclistsWithStats)
            {
                cyclist.CyclistSpeedCobblestones = (float)Math.Round(cyclist.CyclistSpeedCobblestones);
                cyclist.CyclistSpeedDown = (float)Math.Round(cyclist.CyclistSpeedDown);
                cyclist.CyclistSpeedHorizontal = (float)Math.Round(cyclist.CyclistSpeedHorizontal);
                cyclist.CyclistSpeedUp = (float)Math.Round(cyclist.CyclistSpeedUp);
            }
        }

        private void RefreshTeamListViewWithCyclist()
        {
            CyclistsWithStats = new ObservableCollection<Cyclist>(_seasonService.Cyclists().Where(c => c.Id == SelectedCyclist.Id));
            foreach (var cyclist in CyclistsWithStats)
            {
                cyclist.CyclistSpeedCobblestones = (float)Math.Round(cyclist.CyclistSpeedCobblestones);
                cyclist.CyclistSpeedDown = (float)Math.Round(cyclist.CyclistSpeedDown);
                cyclist.CyclistSpeedHorizontal = (float)Math.Round(cyclist.CyclistSpeedHorizontal);
                cyclist.CyclistSpeedUp = (float)Math.Round(cyclist.CyclistSpeedUp);
            }
        }

        private void RefreshRankings()
        {
            CyclistRanking = new ObservableCollection<CyclistInRanking>(_seasonService.CyclistRanking(SelectedDivision.Id));
            TeamRanking = new ObservableCollection<TeamInRanking>(_seasonService.TeamRanking(SelectedDivision.Id));
        }
    }
}
