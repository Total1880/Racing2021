using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Messages.WindowOpener;

namespace Racing2021.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private RelayCommand _openStartRacePageCommand;
        private RelayCommand _openEditorHomePageCommand;

        public RelayCommand OpenStartRacePageCommand => _openStartRacePageCommand ??= new RelayCommand(OpenStartPage);
        public RelayCommand OpenEditorHomePageCommand => _openEditorHomePageCommand ??= new RelayCommand(OpenEditorHomePage);

        private void OpenStartPage()
        {
            MessengerInstance.Send(new OpenStartRacePageMessage());
        }

        private void OpenEditorHomePage()
        {
            MessengerInstance.Send(new OpenEditorHomePageMessage());
        }
    }
}
