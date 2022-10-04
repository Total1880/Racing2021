using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Messages.WindowOpener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing2021.ViewModels
{
    public class NavigationButtonsViewModel : ViewModelBase
    {
        private RelayCommand _openEditorHomePageCommand;
        private RelayCommand _openHomePageCommand;
        private RelayCommand _openGameHomeScreenPageCommand;
        private RelayCommand _openTeamPageCommand;
        private RelayCommand _openOtherTeamPageCommand;
        private RelayCommand _openSearchCyclistPageCommand;

        public RelayCommand OpenEditorHomePageCommand => _openEditorHomePageCommand ??= new RelayCommand(OpenEditorHomePage);
        public RelayCommand OpenHomePageCommand => _openHomePageCommand ??= new RelayCommand(OpenHomePage);
        public RelayCommand OpenGameHomeScreenPageCommand => _openGameHomeScreenPageCommand ??= new RelayCommand(OpenGameHomeScreenPage);

        public RelayCommand OpenTeamPageCommand => _openTeamPageCommand ??= new RelayCommand(OpenTeamPage);
        public RelayCommand OpenOtherTeamPageCommand => _openOtherTeamPageCommand ??= new RelayCommand(OpenOtherTeamPage);
        public RelayCommand OpenSearchCyclistPageCommand => _openSearchCyclistPageCommand ??= new RelayCommand(OpenSearchCyclistPage);

        private void OpenEditorHomePage()
        {
            MessengerInstance.Send(new OpenEditorHomePageMessage());
        }

        private void OpenHomePage()
        {
            MessengerInstance.Send(new OpenHomePageMessage());
        }

        private void OpenGameHomeScreenPage()
        {
            MessengerInstance.Send(new OpenGameHomeScreenPageMessage());
        }

        private void OpenTeamPage()
        {
            MessengerInstance.Send(new OpenTeamPageMessage());
        }
        private void OpenOtherTeamPage()
        {
            MessengerInstance.Send(new OpenOtherTeamPageMessage());
        }

        private void OpenSearchCyclistPage()
        {
            MessengerInstance.Send(new OpenSearchCyclistPageMessage());

        }
    }
}
