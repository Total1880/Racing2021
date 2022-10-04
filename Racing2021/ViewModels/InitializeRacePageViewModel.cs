using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages;
using Racing2021.Messages.WindowOpener;
using Racing2021.Services;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.ViewModels
{
    public class InitializeRacePageViewModel : ViewModelBase
    {
        ISeasonService _seasonService;
        private RelayCommand _startRaceCommand;

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);

        public InitializeRacePageViewModel()
        {
            Messenger.Default.Register<PassTroughSeasonServiceMessage>(this, InitializePage);
        }

        private void InitializePage(PassTroughSeasonServiceMessage obj)
        {
            _seasonService = obj.SeasonService;
        }

        private void StartRace()
        {
            MessengerInstance.Send(new OpenGameHomeScreenPageMessage());
            _seasonService.NextRace();
            MessengerInstance.Send(new RacesAreFinishedMessage());
        }
    }
}
