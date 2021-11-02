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
    public class EditorHomePageViewModel : ViewModelBase
    {
        private RelayCommand _openEditorTracksPageCommand;

        public RelayCommand OpenEditorTracksPageCommand => _openEditorTracksPageCommand ??= new RelayCommand(OpenEditorTracksPage);

        private void OpenEditorTracksPage()
        {
            MessengerInstance.Send(new OpenEditorTracksPageMessage());
        }
    }
}
