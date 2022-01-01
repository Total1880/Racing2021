using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Racing2021.Messages.WindowOpener;
using Racing2021.Models;
using Racing2021.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Racing2021.ViewModels
{
    public class TeamPageViewModel : ViewModelBase
    {
        private ISeasonService _seasonService;
        private ITeamService _teamService;
        private ICyclistService _cyclistService;
        private Team _team;
        private IList<Cyclist> _cyclists;

        public Team Team => _team;
        public IList<Cyclist> Cyclists => _cyclists;

        public TeamPageViewModel(ISeasonService seasonService, ITeamService teamService, ICyclistService cyclistService)
        {
            _seasonService = seasonService;
            _teamService = teamService;
            _cyclistService = cyclistService;
            InitializeTeamPage();

            Messenger.Default.Register<OpenTeamPageMessage>(this, OpenTeamPage);

        }

        private void InitializeTeamPage()
        {
            _team = _teamService.GetTeams().Where(t => t.Id == _seasonService.PlayerTeamId()).FirstOrDefault();

            if (_team == null)
                return;

            _cyclists = _cyclistService.GetCyclists().Where(c => c.TeamId == _seasonService.PlayerTeamId()).ToList();
        }

        private void OpenTeamPage(OpenTeamPageMessage obj)
        {
            InitializeTeamPage();
        }
    }
}
