using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Messages.WindowOpener;

namespace Racing2021.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private RelayCommand _openStartRacePageCommand;

        public RelayCommand OpenStartRacePageCommand => _openStartRacePageCommand ??= new RelayCommand(OpenStartPage);

        private void OpenStartPage()
        {
            MessengerInstance.Send(new OpenStartRacePageMessage());
        }
    }
}
