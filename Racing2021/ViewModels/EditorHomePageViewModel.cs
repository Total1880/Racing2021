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
        private RelayCommand _openEditorCyclistPageCommand;

        public RelayCommand OpenEditorTracksPageCommand => _openEditorTracksPageCommand ??= new RelayCommand(OpenEditorTracksPage);
        public RelayCommand OpenEditorCyclistPageCommand => _openEditorCyclistPageCommand ??= new RelayCommand(OpenEditorCyclistsPage);

        private void OpenEditorTracksPage()
        {
            MessengerInstance.Send(new OpenEditorTracksPageMessage());
        }

        private void OpenEditorCyclistsPage()
        {
            MessengerInstance.Send(new OpenEditorCyclistPageMessage());
        }
    }
}
