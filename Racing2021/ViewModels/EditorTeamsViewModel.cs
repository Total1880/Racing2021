using GalaSoft.MvvmLight;
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
    public class EditorTeamsViewModel : ViewModelBase
    {
        private ITeamService _teamService;
        private IDivisionService _divisionService;
        private ObservableCollection<Team> _teams;
        private ObservableCollection<Division> _divisions;
        private ObservableCollection<string> _teamJerseys;
        private Team _selectedTeam;
        private Division _selectedDivision;
        private RelayCommand _saveChangesCommand;
        private RelayCommand _addNewTeamCommand;
        private RelayCommand _deleteSelectedTeamCommand;

        public ObservableCollection<Team> Teams { get => _teams; set { _teams = value; RaisePropertyChanged(); } }
        public ObservableCollection<Division> Divisions { get => _divisions; set { _divisions = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> TeamJerseys { get => _teamJerseys; set { _teamJerseys = value; RaisePropertyChanged(); } }

        public RelayCommand SaveChangesCommand => _saveChangesCommand ??= new RelayCommand(SaveChanges);
        public RelayCommand AddNewTeamCommand => _addNewTeamCommand ??= new RelayCommand(AddNewTeam);
        public RelayCommand DeleteSelectedTeamCommand => _deleteSelectedTeamCommand ??= new RelayCommand(DeleteSelectedTeam);

        public Team SelectedTeam 
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;

                if (_selectedTeam != null)
                {
                    SelectedDivision = Divisions.Where(d => d.TeamsId.Contains(_selectedTeam.Id)).FirstOrDefault();
                }

                RaisePropertyChanged();
            }
        }
        public Division SelectedDivision { get => _selectedDivision; set { _selectedDivision = value; RaisePropertyChanged(); } }

        public EditorTeamsViewModel(ITeamService teamService, IDivisionService divisionService)
        {
            _teamService = teamService;
            _divisionService = divisionService;
            Teams = new ObservableCollection<Team>(_teamService.GetTeams());
            Divisions = new ObservableCollection<Division>(_divisionService.GetDivisions());
            TeamJerseys = new ObservableCollection<string>(TextureNames.List());
        }

        private void SaveChanges()
        {
            if (SelectedTeam.JerseyName == null || SelectedDivision == null)
            {
                return;
            }

            DeleteTeamFromAllDivisions();

            SelectedDivision.TeamsId.Add(_selectedTeam.Id);

            _divisionService.CreateDivisions(Divisions);
            _teamService.CreateTeams(Teams);
        }

        private void AddNewTeam()
        {
            SelectedTeam = new Team();
            SelectedTeam.Name = "New team";
            SelectedTeam.Id = Teams.Max(t => t.Id) + 1;
            Teams.Add(SelectedTeam);
        }

        private void DeleteSelectedTeam()
        {
            if (SelectedTeam == null)
                return;

            DeleteTeamFromAllDivisions();
            Teams.Remove(SelectedTeam);

            _divisionService.CreateDivisions(Divisions);
            _teamService.CreateTeams(Teams);
        }

        private void DeleteTeamFromAllDivisions()
        {
            foreach (var division in Divisions)
            {
                division.TeamsId.Remove(_selectedTeam.Id);
            }
        }
    }
}
