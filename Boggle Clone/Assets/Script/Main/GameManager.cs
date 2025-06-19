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
        
        [SerializeField] private GameModeView gameModeView;
        
        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }
        
        private void InitializeServices()
        {
            gameModeServices = new GameModeServices(gameModeView, uiService.transform);
        }
    }
}