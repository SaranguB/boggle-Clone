using GameMode.BaseMode;

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