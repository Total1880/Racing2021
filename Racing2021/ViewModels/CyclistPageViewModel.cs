using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages;
using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System.Linq;

namespace Racing2021.ViewModels
{
    public class CyclistPageViewModel : ViewModelBase
    {
        private Cyclist _selectedCyclist;
        private Team _team;
        private ITeamService _teamService;

        public Cyclist SelectedCyclist
        {
            get => _selectedCyclist;
            set
            {
                _selectedCyclist = value;
                RaisePropertyChanged();
            }
        }

        public Team Team
        {
            get => _team;
            set
            {
                _team = value;
                RaisePropertyChanged();
            }
        }

        public CyclistPageViewModel(ITeamService teamService)
        {
            _teamService = teamService;

            Messenger.Default.Register<PassTroughCyclistMessage>(this, InitializePage);
        }

        private void InitializePage(PassTroughCyclistMessage obj)
        {
            SelectedCyclist = obj.Cyclist;
            Team = _teamService.GetTeams().Where(t => t.Id == SelectedCyclist.TeamId).FirstOrDefault();
        }
    }
}
