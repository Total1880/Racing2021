﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class EditorCyclistsViewModel : ViewModelBase
    {
        private ICyclistService _cyclistService;
        private ITeamService _teamService;
        private IDataService _dataService;
        private ObservableCollection<Cyclist> _cyclists;
        private ObservableCollection<Team> _teams;
        private ObservableCollection<string> _nationalities;
        private Cyclist _selectedCyclist;
        private Team _selectedTeam;
        private string _selectedNationality;
        private RelayCommand _saveChangesCommand;
        private RelayCommand _addNewCyclistCommand;
        private RelayCommand _deleteSelectedCyclistCommand;

        public ObservableCollection<Cyclist> Cyclists { get => _cyclists; set { _cyclists = value; RaisePropertyChanged(); } }
        public ObservableCollection<Team> Teams { get => _teams; set { _teams = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> Nationalities { get => _nationalities; set { _nationalities = value; RaisePropertyChanged(); } }

        public RelayCommand SaveChangesCommand => _saveChangesCommand ??= new RelayCommand(SaveChanges);
        public RelayCommand AddNewCyclistCommand => _addNewCyclistCommand ??= new RelayCommand(AddNewCyclist);
        public RelayCommand DeleteSelectedCyclistCommand => _deleteSelectedCyclistCommand ??= new RelayCommand(DeleteSelectedCyclist);

        public Cyclist SelectedCyclist
        {
            get => _selectedCyclist;
            set
            {
                _selectedCyclist = value;

                if (_selectedCyclist != null)
                    SelectedTeam = Teams.Where(t => t.Id == _selectedCyclist.TeamId).FirstOrDefault();

                RaisePropertyChanged();
            }
        }
        public Team SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; RaisePropertyChanged(); } }
        public string SelectedNationality { get => _selectedNationality; set { _selectedNationality = value; RaisePropertyChanged(); } }

        public EditorCyclistsViewModel(ICyclistService cyclistService, ITeamService teamService, IDataService dataService)
        {
            _cyclistService = cyclistService;
            _teamService = teamService;
            _dataService = dataService;
            Cyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists());
            Teams = new ObservableCollection<Team>(_teamService.GetTeams());
            Nationalities = new ObservableCollection<string>(_dataService.GetAllNationalities());
        }

        private void SaveChanges()
        {
            if (SelectedCyclist == null)
            {
                return;
            }
            SelectedCyclist.TeamId = SelectedTeam.Id;
            SelectedCyclist.Nationality = SelectedNationality;
            _cyclistService.CreateCyclists(Cyclists);
        }

        private void AddNewCyclist()
        {
            SelectedCyclist = new Cyclist();
            SelectedCyclist.Name = "new cyclist";
            SelectedCyclist.Id = Cyclists.Max(c => c.Id) + 1;
            Cyclists.Add(SelectedCyclist);
        }

        private void DeleteSelectedCyclist()
        {
            if (SelectedCyclist == null)
                return;

            Cyclists.Remove(SelectedCyclist);
            _cyclistService.CreateCyclists(Cyclists);
        }
    }
}
