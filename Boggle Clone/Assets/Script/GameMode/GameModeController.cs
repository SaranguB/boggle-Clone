using Script.GameMode.EndlessMode;
using Tile;
using UnityEngine;

namespace Script.GameMode
{
    public class GameModeController
    {
        private GameModeView gameModeView;
        private TilePool tilePool;
        
        public GameModeController(GameModeView gameModeView, Transform parent = null)
        {
            this.gameModeView = gameModeView;
            this.gameModeView.SetController(this);
        }

        public void OnEndlessModeSelected()
        {
            gameModeView.EnableGameGrid();
            EndlessModeController endlessModeController = new  EndlessModeController(gameModeView.endlessModeView);
        }
    }
}