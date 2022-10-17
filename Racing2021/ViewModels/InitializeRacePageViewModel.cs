using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OlavFramework;
using Racing2021.Messages;
using Racing2021.Messages.WindowOpener;
using Racing2021.Models;
using Racing2021.Services;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.ViewModels
{
    public class InitializeRacePageViewModel : ViewModelBase
    {
        ISeasonService _seasonService;
        ICyclistService _cyclistService;
        private ObservableCollection<Cyclist> _cyclists;
        private RelayCommand _startRaceCommand;
        private Cyclist _selectedTeamLeader;

        public ObservableCollection<Cyclist> Cyclists
        {
            get => _cyclists;
            set
            {
                _cyclists = value;
                RaisePropertyChanged();
            }
        }

        public Cyclist SelectedTeamLeader
        {
            get => _selectedTeamLeader;
            set
            {
                if (_selectedTeamLeader == null)
                {
                    _selectedTeamLeader = value;
                    RaisePropertyChanged();
                }
                else if (_selectedTeamLeader != null && value != null)
                {
                    Cyclists.FirstOrDefault(c => c.Id == _selectedTeamLeader.Id).TeamLeader = false;
                    Cyclists.FirstOrDefault(c => c.Id == value.Id).TeamLeader = true;
                    _cyclistService.saveCyclist(Cyclists.FirstOrDefault(c => c.Id == _selectedTeamLeader.Id));
                    _cyclistService.saveCyclist(Cyclists.FirstOrDefault(c => c.Id == value.Id));
                    _selectedTeamLeader = value;
                }
            }
        }

        public RelayCommand StartRaceCommand => _startRaceCommand ??= new RelayCommand(StartRace);

        public InitializeRacePageViewModel(ICyclistService clistService)
        {
            Messenger.Default.Register<PassTroughSeasonServiceMessage>(this, InitializePage);
            _cyclistService = clistService;
        }

        private void InitializePage(PassTroughSeasonServiceMessage obj)
        {
            _seasonService = obj.SeasonService;
            Cyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists().Where(c => c.TeamId == Configuration.UserTeamId && c.SelectedForRace));
            SelectedTeamLeader = _cyclistService.GetCyclists().FirstOrDefault(c => c.TeamId == Configuration.UserTeamId && c.TeamLeader);
        }

        private void StartRace()
        {
            MessengerInstance.Send(new OpenGameHomeScreenPageMessage());
            _seasonService.NextRace();
            MessengerInstance.Send(new RacesAreFinishedMessage());
        }
    }
}
