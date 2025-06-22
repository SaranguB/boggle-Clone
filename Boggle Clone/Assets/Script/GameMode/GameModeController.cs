using GameMode.EndlessMode;
using GameMode.LevelMode;
using Main;
using Tile;
using UnityEngine;
using Utilities;

namespace GameMode
{
    public class GameModeController
    {
        private GameModeView gameModeView;
        private TilePool tilePool;
        
        public GameModeController(GameModeView gameModeView, Transform parent = null)
        {
            this.gameModeView = gameModeView;
            this.gameModeView.SetController(this);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
           GameManager.Instance.eventService.OnEndlessModeSelected.AddListener(OnEndlessModeSelected);
           GameManager.Instance.eventService.OnLevelModeSelected.AddListener(OneLevelModeSelected);
        }

        ~GameModeController()
        {
            GameManager.Instance.eventService.OnEndlessModeSelected.RemoveListener(OnEndlessModeSelected);
            GameManager.Instance.eventService.OnLevelModeSelected.RemoveListener(OneLevelModeSelected);
        }

        private void OnEndlessModeSelected()
        {
            gameModeView.EnableGameGrid();
            EndlessModeController endlessModeController = new  EndlessModeController(gameModeView.endlessModeView);
        }
        
        private void OneLevelModeSelected( LevelModeSo levelSo)
        {
            gameModeView.EnableGameGrid();
            LevelDataLoaderExtension.LoadAllLevels();
            LevelData levelData = LevelDataLoaderExtension.GetLevel(levelSo.selectedLevel);
            LevelDataLoaderExtension.ConvertToScriptable(levelData, levelSo);
            LevelModeController levelModeController = new LevelModeController(gameModeView.levelModeView, levelSo);;
        }
    }
}