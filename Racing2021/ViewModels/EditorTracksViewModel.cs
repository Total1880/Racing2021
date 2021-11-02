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
    public class EditorTracksViewModel : ViewModelBase
    {
        private ITrackService _trackService;
        private ObservableCollection<Track> _tracks;
        private Track _selectedTrack;
        private RelayCommand _saveChangesCommand;

        public ObservableCollection<Track> Tracks { get => _tracks; set { _tracks = value; RaisePropertyChanged(); } }

        public RelayCommand SaveChangesCommand => _saveChangesCommand ??= new RelayCommand(SaveChanges);

        public Track SelectedTrack
        {
            get => _selectedTrack; set
            {
                _selectedTrack = value;
                RaisePropertyChanged();
            }
        }


        public EditorTracksViewModel(ITrackService trackService)
        {
            _trackService = trackService;

            _tracks = new ObservableCollection<Track>(trackService.GetTracks());
        }

        private void SaveChanges()
        {
            _trackService.CreateTracks(Tracks);
        }
    }
}
