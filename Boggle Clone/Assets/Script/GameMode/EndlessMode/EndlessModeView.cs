using GameMode.BaseMode;

namespace GameMode.EndlessMode
{
    public class EndlessModeView : BaseModeView
    {
        private EndlessModeController endlessModeController;

        public void SetController(EndlessModeController endlessModeController)
        {
           this.endlessModeController = endlessModeController;
        }
    }
}