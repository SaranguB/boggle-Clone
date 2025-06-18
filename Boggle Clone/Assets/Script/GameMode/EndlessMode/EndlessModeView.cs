using UnityEngine;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeView : MonoBehaviour
    {
        private EndlessModeController endlessModeController;
        public void SetController(EndlessModeController endlessModeController)
        {
           this.endlessModeController = endlessModeController; 
        }
    }
}