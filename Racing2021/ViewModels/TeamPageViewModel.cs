using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages.WindowOpener;
using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Racing2021.ViewModels
{
    public class TeamPageViewModel : ViewModelBase
    {
        private ISeasonService _seasonService;
        private ITeamService _teamService;
        private ICyclistService _cyclistService;
        private Team _team;
        private ObservableCollection<Cyclist> _cyclists;
        private ObservableCollection<Cyclist> _cyclistsForRace;
        private RelayCommand _addYoungCyclistCommand;
        private RelayCommand _addCyclistToRaceCommand;
        private RelayCommand _removeCyclistFromRaceCommand;
        private int _maxCyclistsPerTeam = 3;
        private Cyclist _selectedCyclist;
        private Cyclist _selectedCyclistForRace;

        public RelayCommand AddYoungCyclistCommand => _addYoungCyclistCommand ??= new RelayCommand(AddYoungCyclist);
        public RelayCommand AddCyclistToRaceCommand => _addCyclistToRaceCommand ??= new RelayCommand(AddCyclistToRace);

        public RelayCommand RemoveCyclistFromRaceCommand => _removeCyclistFromRaceCommand ??= new RelayCommand(RemoveCyclistFromRace);

        public Team Team
        {
            get => _team;
            set
            {
                _team = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Cyclist> Cyclists
        {
            get => _cyclists;
            set
            {
                _cyclists = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Cyclist> CyclistsForRace
        {
            get => _cyclistsForRace;
            set
            {
                _cyclistsForRace = value;
                RaisePropertyChanged();
            }
        }

        public Cyclist SelectedCyclist
        {
            get => _selectedCyclist;
            set
            {
                _selectedCyclist = value;
                RaisePropertyChanged();
            }
        }

        public Cyclist SelectedCyclistForRace
        {
            get => _selectedCyclistForRace;
            set
            {
                _selectedCyclistForRace = value;
                RaisePropertyChanged();
            }
        }

        public TeamPageViewModel(ISeasonService seasonService, ITeamService teamService, ICyclistService cyclistService)
        {
            _seasonService = seasonService;
            _teamService = teamService;
            _cyclistService = cyclistService;
            InitializeTeamPage();

            Messenger.Default.Register<OpenTeamPageMessage>(this, OpenTeamPage);

        }

        private void InitializeTeamPage()
        {
            Team = _teamService.GetTeams().Where(t => t.Id == _seasonService.PlayerTeamId()).FirstOrDefault();

            if (Team == null)
                return;

            Cyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists().Where(c => c.TeamId == _seasonService.PlayerTeamId() && !c.SelectedForRace).ToList());
            CyclistsForRace = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists().Where(c => c.TeamId == _seasonService.PlayerTeamId() && c.SelectedForRace).ToList());
        }

        private void OpenTeamPage(OpenTeamPageMessage obj)
        {
            InitializeTeamPage();
        }

        private void AddYoungCyclist()
        {
            if (Cyclists.Count + CyclistsForRace.Count >= _maxCyclistsPerTeam)
                return;

            _cyclistService.CreateYoungCyclist(Team.Id);
            InitializeTeamPage();
        }

        private void AddCyclistToRace()
        {
            if (SelectedCyclist == null)
                return;

            if (CyclistsForRace.Any(c => c.Id == SelectedCyclist.Id))
            {
                throw new Exception("This shouldn't happen");
            }
            SelectedCyclist.SelectedForRace = true;

            _cyclistService.saveCyclist(SelectedCyclist);

            SelectedCyclist = null;

            InitializeTeamPage();
        }

        private void RemoveCyclistFromRace()
        {
            if (SelectedCyclistForRace == null)
                return;

            if (Cyclists.Any(c => c.Id == SelectedCyclistForRace.Id))
            {
                throw new Exception("This shouldn't happen");
            }
            SelectedCyclistForRace.SelectedForRace = false;

            _cyclistService.saveCyclist(SelectedCyclistForRace);
            SelectedCyclistForRace = null;

            InitializeTeamPage();
        }
    }
}
