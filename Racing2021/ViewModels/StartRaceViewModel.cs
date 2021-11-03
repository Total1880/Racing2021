using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Models;
using Racing2021.Models.RaceEngine;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private RelayCommand _startRaceCommand;
        private RelayCommand _nextSeasonCommand;
        private ObservableCollection<CyclistInRanking> _cyclistRanking;
        private ObservableCollection<TeamInRanking> _teamRanking;
        private Visibility _showNextRaceButton;
        private Visibility _showEndSeasonButton;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);
        public RelayCommand NextSeasonCommand => _nextSeasonCommand ??= new RelayCommand(NextSeason);

        public ObservableCollection<CyclistInRanking> CyclistRanking { get => _cyclistRanking; set { _cyclistRanking = value; RaisePropertyChanged(); } }
        public ObservableCollection<TeamInRanking> TeamRanking { get => _teamRanking; set { _teamRanking = value; RaisePropertyChanged(); } }

        public Visibility ShowNextRaceButton { get => _showNextRaceButton; set { _showNextRaceButton = value; RaisePropertyChanged(); } }
        public Visibility ShowEndSeasonButton { get => _showEndSeasonButton; set { _showEndSeasonButton = value; RaisePropertyChanged(); } }

        public StartRaceViewModel(ICyclistService cyclistService, ITrackService trackService, ISeasonService seasonService, ITeamService teamService, IDivisionService divisionService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            _seasonService = seasonService;
            _teamService = teamService;
            _divisionService = divisionService;
            CreateDivisions();
            CreateTeams();

            if (_cyclistService.GetCyclists().Count < 1)
                CreateCyclists();

            if (_trackService.GetTracks().Count < 1)
                CreateTracks();

            ShowNextRaceButton = Visibility.Visible;
            ShowEndSeasonButton = Visibility.Collapsed;
        }

        private void StartRace()
        {
            _seasonService.NextRace();

            CyclistRanking = new ObservableCollection<CyclistInRanking>(_seasonService.CyclistRanking());
            TeamRanking = new ObservableCollection<TeamInRanking>(_seasonService.TeamRanking());

            if (_seasonService.IsSeasonEnded())
            {
                ShowNextRaceButton = Visibility.Collapsed;
                ShowEndSeasonButton = Visibility.Visible;
            }
        }

        private void NextSeason()
        {
            _seasonService.NextSeason();
            CyclistRanking.Clear();
            TeamRanking.Clear();
            ShowNextRaceButton = Visibility.Visible;
            ShowEndSeasonButton = Visibility.Collapsed;
        }

        private void CreateDivisions()
        {
            var divisions = new List<Division>();

            divisions.Add(new Division(0, 1, "Division 1"));
            divisions.Add(new Division(1, 2, "Division 2"));

            divisions[0].TeamsId.Add(0);
            divisions[0].TeamsId.Add(1);
            divisions[0].TeamsId.Add(2);
            divisions[1].TeamsId.Add(3);
            divisions[1].TeamsId.Add(4);
            divisions[1].TeamsId.Add(5);

            _divisionService.CreateDivisions(divisions);
        }

        private void CreateCyclists()
        {
            var cyclists = new List<Cyclist>();

            cyclists.Add(new Cyclist(0, 80f,70f, 100f, 95f, "Tadej Pogacar", 0, 22));
            cyclists.Add(new Cyclist(1, 75f,60f, 80f, 90f, "Rui Costa", 0, 35));
            cyclists.Add(new Cyclist(2, 95f,92f, 90f, 90f, "Wout Van Aert", 1, 26));
            cyclists.Add(new Cyclist(3, 90f,85f, 70f, 85f, "Nathan Van Hooydonck", 1, 26));
            cyclists.Add(new Cyclist(4, 90f,85f, 95f, 80f, "Remco Evenepoel", 2, 21));
            cyclists.Add(new Cyclist(5, 85f,82f, 75f, 75f, "Tim Declercq", 2, 32));
            cyclists.Add(new Cyclist(6, 100f,91f, 85f, 90f, "Mathieu Van Der Poel", 3, 26));
            cyclists.Add(new Cyclist(7, 95f,84f, 65f, 85f, "Tim Merlier", 3, 32));
            cyclists.Add(new Cyclist(8, 70f,75f, 70f, 70f, "Olav Hendrickx", 4, 33));
            cyclists.Add(new Cyclist(9, 75f,80f, 65f, 70f, "Arne Hendrickx", 4, 35));
            cyclists.Add(new Cyclist(10, 85f,90f, 75f, 80f, "Sander Delmeire", 5, 34));
            cyclists.Add(new Cyclist(11, 80f,85f, 70f, 75f, "Loic Vandenbroucke", 5, 26));

            _cyclistService.CreateCyclists(cyclists);
        }

        private void CreateTeams()
        {
            var teams = new List<Team>();

            teams.Add(new Team(0, "Team A", TextureNames.CyclistBlue));
            teams.Add(new Team(1, "Team B", TextureNames.CyclistGreen));
            teams.Add(new Team(2, "Team C", TextureNames.CyclistRed));
            teams.Add(new Team(3, "Team D", TextureNames.CyclistRoseGrey));
            teams.Add(new Team(4, "Team E", TextureNames.CyclistYellow));
            teams.Add(new Team(5, "Team F", TextureNames.CyclistBlackYellow));

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

            do
            {
                tracks[0].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 3));
                counter++;
            } while (counter < 10);

            counter = 0;
            tracks.Add(new Track());
            tracks[1].Id = 1;
            tracks[1].Name = "track 2";

            do
            {
                tracks[1].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 3));
                counter++;
            } while (counter < 10);

            counter = 0;
            tracks.Add(new Track());
            tracks[2].Id = 2;
            tracks[2].Name = "track 3";

            do
            {
                tracks[2].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 3));
                counter++;
            } while (counter < 10);

            _trackService.CreateTracks(tracks);
        }
    }
}
