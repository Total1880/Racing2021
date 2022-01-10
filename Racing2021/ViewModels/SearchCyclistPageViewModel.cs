using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages;
using Racing2021.Messages.WindowOpener;
using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.ViewModels
{
    public class SearchCyclistPageViewModel : ViewModelBase
    {
        ICyclistService _cyclistService;
        ITeamService _teamService;

        private Cyclist _selectedCyclist;

        public Cyclist SelectedCyclist
        {
            get => _selectedCyclist;
            set
            {
                _selectedCyclist = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand _openCyclistPageCommand;

        public RelayCommand OpenCyclistPageCommand => _openCyclistPageCommand ??= new RelayCommand(OpenCyclistPage);

        private ObservableCollection<Cyclist> _allCyclists;

        public ObservableCollection<Cyclist> AllCyclists { get => _allCyclists; set { _allCyclists = value; RaisePropertyChanged(); } }

        public SearchCyclistPageViewModel(ICyclistService cyclistService, ITeamService teamService)
        {
            _cyclistService = cyclistService;
            _teamService = teamService;

            AllCyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists());

            Messenger.Default.Register<OpenSearchCyclistPageMessage>(this, InitializePage);
            Messenger.Default.Register<SearchCyclistDoubleClick>(this, OpenCyclistPage);
        }



        private void InitializePage(OpenSearchCyclistPageMessage obj)
        {
            AllCyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists());
        }

        private void OpenCyclistPage(SearchCyclistDoubleClick obj)
        {
            OpenCyclistPage();
        }

        private void OpenCyclistPage()
        {
            if (SelectedCyclist == null)
            {
                return;
            }
            MessengerInstance.Send(new OpenCyclistPageMessage());
            MessengerInstance.Send(new PassTroughCyclistMessage(SelectedCyclist));
        }
    }
}
