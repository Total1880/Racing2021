using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages.WindowOpener;
using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Racing2021.ViewModels
{
    public class OtherTeamPageViewModel : ViewModelBase
    {
        ITeamService _teamService;
        IManagerService _managerService;
        ICyclistService _cyclistService;
        private ObservableCollection<Team> _teams;
        private ObservableCollection<Cyclist> _cyclists;
        private Team _selectedTeam;
        private Manager _manager;

        public ObservableCollection<Team> Teams { get => _teams; set { _teams = value; RaisePropertyChanged(); } }
        public ObservableCollection<Cyclist> Cyclists { get => _cyclists; set { _cyclists = value; RaisePropertyChanged(); } }
        public Team SelectedTeam { get => _selectedTeam; set { _selectedTeam = value; InitializeTeam(); RaisePropertyChanged(); } }
        public Manager Manager { get => _manager; set { _manager = value; RaisePropertyChanged(); } }

        public OtherTeamPageViewModel(ITeamService teamService, IManagerService managerService, ICyclistService cyclistService)
        {
            _teamService = teamService;
            _managerService = managerService;
            _cyclistService = cyclistService;

            Teams = new ObservableCollection<Team>(_teamService.GetTeams());

            Messenger.Default.Register<OpenOtherTeamPageMessage>(this, ResetScreen);
        }

        private void ResetScreen(OpenOtherTeamPageMessage obj)
        {
            Teams = new ObservableCollection<Team>(_teamService.GetTeams());

        }

        private void InitializeTeam()
        {
            if (SelectedTeam == null)
            {
                SelectedTeam = Teams[0];
            }
            Manager = _managerService.GetManagers().Where(m => m.Id == SelectedTeam.ManagerId).FirstOrDefault();
            Cyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists().Where(c => c.TeamId == SelectedTeam.Id).ToList());
        }
    }
}
