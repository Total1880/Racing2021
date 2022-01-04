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
        private RelayCommand _openStartRacePageCommand;
        private RelayCommand _openTeamPageCommand;

        public RelayCommand OpenEditorHomePageCommand => _openEditorHomePageCommand ??= new RelayCommand(OpenEditorHomePage);
        public RelayCommand OpenHomePageCommand => _openHomePageCommand ??= new RelayCommand(OpenHomePage);
        public RelayCommand OpenStartRacePageCommand => _openStartRacePageCommand ??= new RelayCommand(OpenStartRacePage);

        public RelayCommand OpenTeamPageCommand => _openTeamPageCommand ??= new RelayCommand(OpenTeamPage);



        private void OpenEditorHomePage()
        {
            MessengerInstance.Send(new OpenEditorHomePageMessage());
        }

        private void OpenHomePage()
        {
            MessengerInstance.Send(new OpenHomePageMessage());
        }

        private void OpenStartRacePage()
        {
            MessengerInstance.Send(new OpenStartRacePageMessage());
        }

        private void OpenTeamPage()
        {
            MessengerInstance.Send(new OpenTeamPageMessage());
        }
    }
}
