

namespace Script.GameMode
{
    public class GameModeServices
    {
        public GameModeController GameModeController { get; private set; }
        
        public GameModeServices(GameModeView gameModeViewPrefab)
        {
            GameModeController = new GameModeController(gameModeViewPrefab);
        }


    }
}