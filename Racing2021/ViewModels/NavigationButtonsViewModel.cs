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

        public RelayCommand OpenEditorHomePageCommand => _openEditorHomePageCommand ??= new RelayCommand(OpenEditorHomePage);
        public RelayCommand OpenHomePageCommand => _openHomePageCommand ??= new RelayCommand(OpenHomePage);



        private void OpenEditorHomePage()
        {
            MessengerInstance.Send(new OpenEditorHomePageMessage());
        }

        private void OpenHomePage()
        {
            MessengerInstance.Send(new OpenHomePageMessage());
        }
    }
}
