using UnityEngine;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeController 
    {
        private EndlessModeView endlessModeView;
        public EndlessModeController(EndlessModeView endlessModeView, Transform parent = null)
        {
            endlessModeView = Object.Instantiate(endlessModeView);
            endlessModeView.SetController(this);
        }
    }
}