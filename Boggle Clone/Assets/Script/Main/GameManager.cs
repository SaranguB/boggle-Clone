using Events;
using JetBrains.Annotations;
using Script.GameMode;
using UI;
using UnityEngine;
using Utilities;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public GameModeServices gameModeServices;
        public UIService uiService;
        public EventService eventService;
        
        [SerializeField] private GameModeView gameModeView;
        
        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }
        
        private void InitializeServices()
        {
            eventService = new EventService();
            gameModeServices = new GameModeServices(gameModeView, uiService.transform);
        }
    }
}