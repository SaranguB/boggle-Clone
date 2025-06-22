using GameMode.BaseMode;
using Script.GameMode.LevelMode;

namespace GameMode.LevelMode
{
    public class LevelModeView : BaseModeView
    {
        private LevelModeController levelModeController;
        
        public void SetController(LevelModeController levelModeController)
        {
            this.levelModeController = levelModeController;
        }
    }
}