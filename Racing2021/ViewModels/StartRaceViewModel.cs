using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Services.Interfaces;
using System;

namespace Racing2021.ViewModels
{
    public class StartRaceViewModel : ViewModelBase
    {
        private IRaceService _raceService;
        private RelayCommand _startRaceCommand;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);

        public StartRaceViewModel(IRaceService raceService)
        {
            _raceService = raceService;
        }

        private void StartRace()
        {
            _raceService.StartRace();
        }
    }
}
