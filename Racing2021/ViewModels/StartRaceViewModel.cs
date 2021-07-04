using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Racing2021.ViewModels
{
    public class StartRaceViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private ICyclistService _cyclistService;
        private RelayCommand _startRaceCommand;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);

        public StartRaceViewModel(IRaceService raceService, ICyclistService cyclistService)
        {
            _raceService = raceService;
            _cyclistService = cyclistService;
            CreateCyclists();
        }

        private void StartRace()
        {
            _raceService.StartRace();
        }

        private void CreateCyclists()
        {
            var cyclists = new List<Cyclist>();

            cyclists.Add(new Cyclist(80f, 100f, 95f, "Tadej Pogacar"));
            cyclists.Add(new Cyclist(95f, 90f, 90f, "Wout van Aert"));
            cyclists.Add(new Cyclist(90f, 95f, 80f, "Remco Evenepoel"));
            cyclists.Add(new Cyclist(100f, 85f, 90f, "Mathieu van der Poel"));
            cyclists.Add(new Cyclist(50, 50, 50, "Olav Hendrickx"));

            _cyclistService.CreateCyclists(cyclists);
        }
    }
}
