using JetBrains.Annotations;
using Script.GameMode;
using UnityEngine;
using Utilities;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public GameModeServices gameModeServices;

        [SerializeField] private GameModeView gameModeViewPrefab;
        
        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }
        
        private void InitializeServices()
        {
            gameModeServices = new GameModeServices(gameModeViewPrefab);
        }
    }
}