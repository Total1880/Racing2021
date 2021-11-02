using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Racing2021.Models;
using Racing2021.Models.Enums;
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
        private ObservableCollection<string> _trackTiles;
        private Track _selectedTrack;
        private RelayCommand _saveChangesCommand;

        public ObservableCollection<Track> Tracks { get => _tracks; set { _tracks = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> TrackTiles { get => _trackTiles; set { _trackTiles = value; RaisePropertyChanged(); } }

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

            CreateListOfTrackTiles();
        }

        private void SaveChanges()
        {
            _trackService.CreateTracks(Tracks);
        }

        private void CreateListOfTrackTiles()
        {
            TrackTiles = new ObservableCollection<string>();

            foreach (var trackTile in Enum.GetNames(typeof(TrackTile)))
            {
                TrackTiles.Add(trackTile);
            }
        }
    }
}
