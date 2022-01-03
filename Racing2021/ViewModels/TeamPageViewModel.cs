﻿using GalaSoft.MvvmLight;
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
        private RelayCommand _addYoungCyclistCommand;
        private int _maxCyclistsPerTeam = 3;

        public RelayCommand AddYoungCyclistCommand => _addYoungCyclistCommand ??= new RelayCommand(AddYoungCyclist);

        public Team Team => _team;
        public ObservableCollection<Cyclist> Cyclists
        {
            get => _cyclists;
            set
            {
                _cyclists = value;
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
            _team = _teamService.GetTeams().Where(t => t.Id == _seasonService.PlayerTeamId()).FirstOrDefault();

            if (_team == null)
                return;

            Cyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists().Where(c => c.TeamId == _seasonService.PlayerTeamId()).ToList());
        }

        private void OpenTeamPage(OpenTeamPageMessage obj)
        {
            InitializeTeamPage();
        }

        private void AddYoungCyclist()
        {
            if (Cyclists.Count >= _maxCyclistsPerTeam)
                return;

            _cyclistService.CreateYoungCyclist(_team.Id);
            InitializeTeamPage();
        }
    }
}
