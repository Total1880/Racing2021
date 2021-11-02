using GalaSoft.MvvmLight;
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
    public class EditorCyclistsViewModel : ViewModelBase
    {
        private ICyclistService _cyclistService;
        private ObservableCollection<Cyclist> _cyclists;

        public ObservableCollection<Cyclist> Cyclists { get => _cyclists; set { _cyclists = value; RaisePropertyChanged(); } }

        public EditorCyclistsViewModel(ICyclistService cyclistService)
        {
            _cyclistService = cyclistService;
            Cyclists = new ObservableCollection<Cyclist>(_cyclistService.GetCyclists());
        }
    }
}
