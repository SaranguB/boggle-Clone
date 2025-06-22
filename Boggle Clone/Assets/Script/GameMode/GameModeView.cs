using GameMode.EndlessMode;
using GameMode.LevelMode;
using UnityEngine;
using Utilities;

namespace GameMode
{
    public class GameModeView :MonoBehaviour
    {
        [SerializeField] private CanvasGroup gameModeCanavs;
        [field: SerializeField] public EndlessModeView endlessModeView {get; private set;}
        [field: SerializeField] public LevelModeView levelModeView {get; private set;}
        private GameModeController gameModeController;
        
        public void SetController(GameModeController gameModeController)
        {
            this.gameModeController = gameModeController;
        }

        public void EnableGameGrid()
        {
          CanvasGroupExtension.Show(gameModeCanavs);
        }
    }
}