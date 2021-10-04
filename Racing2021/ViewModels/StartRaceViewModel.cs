﻿using GalaSoft.MvvmLight;
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
        private RelayCommand _startRaceCommand;
        private RelayCommand _nextSeasonCommand;
        private ObservableCollection<CyclistInRanking> _cyclistRanking;
        private Visibility _showNextRaceButton;
        private Visibility _showEndSeasonButton;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);
        public RelayCommand NextSeasonCommand => _nextSeasonCommand ??= new RelayCommand(NextSeason);

        public ObservableCollection<CyclistInRanking> CyclistRanking { get => _cyclistRanking; set { _cyclistRanking = value; RaisePropertyChanged(); } }

        public Visibility ShowNextRaceButton { get => _showNextRaceButton; set { _showNextRaceButton = value; RaisePropertyChanged(); } }
        public Visibility ShowEndSeasonButton { get => _showEndSeasonButton; set { _showEndSeasonButton = value; RaisePropertyChanged(); } }

        public StartRaceViewModel(ICyclistService cyclistService, ITrackService trackService, ISeasonService seasonService)
        {
            _cyclistService = cyclistService;
            _trackService = trackService;
            CreateCyclists();
            CreateTracks();
            _seasonService = seasonService;
            ShowNextRaceButton = Visibility.Visible;
            ShowEndSeasonButton = Visibility.Collapsed;
        }

        private void StartRace()
        {
            _seasonService.NextRace();
            CyclistRanking = new ObservableCollection<CyclistInRanking>(_seasonService.Ranking());
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
            ShowNextRaceButton = Visibility.Visible;
            ShowEndSeasonButton = Visibility.Collapsed;
        }

        private void CreateCyclists()
        {
            var cyclists = new List<Cyclist>();

            cyclists.Add(new Cyclist(0,80f, 100f, 95f, "Tadej Pogacar", 22));
            cyclists.Add(new Cyclist(1,95f, 90f, 90f, "Wout Van Aert", 26));
            cyclists.Add(new Cyclist(2,90f, 95f, 80f, "Remco Evenepoel", 21));
            cyclists.Add(new Cyclist(3,100f, 85f, 90f, "Mathieu Van Der Poel", 26));
            cyclists.Add(new Cyclist(4,50f, 50f, 50f, "Olav Hendrickx", 33));

            _cyclistService.CreateCyclists(cyclists);
        }

        private void CreateTracks()
        {
            var tracks = new List<Track>();
            var random = new Random();

            var counter = 0;
            tracks.Add(new Track());
            tracks[0].Name = "track 1";

            do
            {
                tracks[0].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 3));
                counter++;
            } while (counter < 10);

            counter = 0;
            tracks.Add(new Track());
            tracks[1].Name = "track 2";

            do
            {
                tracks[1].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 3));
                counter++;
            } while (counter < 10);

            counter = 0;
            tracks.Add(new Track());
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
