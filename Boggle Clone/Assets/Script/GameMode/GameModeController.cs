using Script.GameMode.EndlessMode;
using UnityEngine;

namespace Script.GameMode
{
    public class GameModeController
    {
        private GameModeView gameModeView;
        
        public GameModeController(GameModeView gameModeViewPrefab, Transform parent = null)
        {
            gameModeView = Object.Instantiate(gameModeViewPrefab, parent);
            
            gameModeView.SetController(this);
        }

        public void OnEndlessModeSelected()
        {
            gameModeView.EnableGameGrid();
            EndlessModeController endlessModeController = new  EndlessModeController(gameModeView.endlessModeView);


        }
    }
}