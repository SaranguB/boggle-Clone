using UnityEngine;

namespace GameMode
{
    public class GameModeServices
    {
        public GameModeController GameModeController { get; private set; }
        
        public GameModeServices(GameModeView gameModeView, Transform canvasTransform)
        {
            GameModeController = new GameModeController(gameModeView,  canvasTransform);
        }
    }
}