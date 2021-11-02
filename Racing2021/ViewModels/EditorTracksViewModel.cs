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
        private ObservableCollection<TrackTile> _selectedTrackTrackTiles;
        private Track _selectedTrack;
        private RelayCommand _saveChangesCommand;
        private RelayCommand _addTrackTileCommand;
        private RelayCommand _deleteLastTrackTileCommand;
        private RelayCommand _addNewTrackCommand;
        private RelayCommand _deleteSelectedTrackCommand;

        public ObservableCollection<Track> Tracks { get => _tracks; set { _tracks = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> TrackTiles { get => _trackTiles; set { _trackTiles = value; RaisePropertyChanged(); } }
        public ObservableCollection<TrackTile> SelectedTrackTrackTiles { get => _selectedTrackTrackTiles; set { _selectedTrackTrackTiles = value; RaisePropertyChanged(); } }

        public RelayCommand SaveChangesCommand => _saveChangesCommand ??= new RelayCommand(SaveChanges);
        public RelayCommand AddTrackTileCommand => _addTrackTileCommand ??= new RelayCommand(AddTrackTile);
        public RelayCommand DeleteLastTrackTileCommand => _deleteLastTrackTileCommand ??= new RelayCommand(DeleteLastTrackTile);
        public RelayCommand AddNewTrackCommand => _addNewTrackCommand ??= new RelayCommand(AddNewTrack);
        public RelayCommand DeleteSelectedTrackCommand => _deleteSelectedTrackCommand ??= new RelayCommand(DeleteSelectedTrack);

        public Track SelectedTrack
        {
            get => _selectedTrack;
            set
            {
                _selectedTrack = value;

                if (_selectedTrack != null)
                {
                    SelectedTrackTrackTiles = new ObservableCollection<TrackTile>(_selectedTrack.TrackTiles);
                }

                RaisePropertyChanged();
            }
        }

        public TrackTile SelectedTrackTile { get; set; }


        public EditorTracksViewModel(ITrackService trackService)
        {
            _trackService = trackService;

            _tracks = new ObservableCollection<Track>(trackService.GetTracks());

            CreateListOfTrackTiles();
        }

        private void SaveChanges()
        {
            SelectedTrack.TrackTiles = SelectedTrackTrackTiles;
            _trackService.CreateTracks(Tracks);
        }

        private void AddTrackTile()
        {
            if (SelectedTrack == null)
                return;

            SelectedTrackTrackTiles.Add(SelectedTrackTile);
        }

        private void DeleteLastTrackTile()
        {
            if (SelectedTrack == null || SelectedTrackTrackTiles.Count < 1)
                return;

            SelectedTrackTrackTiles.RemoveAt(SelectedTrackTrackTiles.Count - 1);
        }

        private void AddNewTrack()
        {
            SelectedTrack = new Track();
            SelectedTrack.Name = "new track";
            SelectedTrack.Id = Tracks.Max(t => t.Id) + 1;
            SelectedTrack.TrackTiles = new List<TrackTile>();
            Tracks.Add(SelectedTrack);
        }

        private void DeleteSelectedTrack()
        {
            if (SelectedTrack == null)
                return;

            Tracks.Remove(SelectedTrack);
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
