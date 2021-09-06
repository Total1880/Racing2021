using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Models;
using Racing2021.Models.RaceEngine;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Racing2021.ViewModels
{
    public class StartRaceViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private ICyclistService _cyclistService;
        private ITrackService _trackService;
        private RelayCommand _startRaceCommand;
        private IList<CyclistRaceEngine> _cyclistRanking;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);

        public IList<CyclistRaceEngine> CyclistRanking { get => _cyclistRanking; set { _cyclistRanking = value; RaisePropertyChanged(); } }

        public StartRaceViewModel(IRaceService raceService, ICyclistService cyclistService, ITrackService trackService)
        {
            _raceService = raceService;
            _cyclistService = cyclistService;
            _trackService = trackService;
            CreateCyclists();
            CreateTracks();
        }

        private void StartRace()
        {
            _raceService.StartRace();
            CyclistRanking = _raceService.FinishedCyclists();
        }

        private void CreateCyclists()
        {
            var cyclists = new List<Cyclist>();

            cyclists.Add(new Cyclist(80f, 100f, 95f, "Tadej Pogacar"));
            cyclists.Add(new Cyclist(95f, 90f, 90f, "Wout van Aert"));
            cyclists.Add(new Cyclist(90f, 95f, 80f, "Remco Evenepoel"));
            cyclists.Add(new Cyclist(100f, 85f, 90f, "Mathieu van der Poel"));
            cyclists.Add(new Cyclist(50f, 50f, 50f, "Olav Hendrickx"));

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
            } while (counter < 20);

            counter = 0;
            tracks.Add(new Track());
            tracks[1].Name = "track 2";

            do
            {
                tracks[1].TrackTiles.Add((Models.Enums.TrackTile)random.Next(0, 3));
                counter++;
            } while (counter < 20);

            _trackService.CreateTracks(tracks);
        }
    }
}
