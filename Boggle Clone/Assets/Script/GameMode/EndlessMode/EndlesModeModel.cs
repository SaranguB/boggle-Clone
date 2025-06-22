using GameMode.BaseMode;
using UnityEngine;

namespace GameMode.EndlessMode
{
    public class EndlesModeModel : BaseModeModel
    {
        public EndlessModeSo endlessModeData { get; private set; }
        public override Vector2 GridSize => endlessModeData.gridSize;
        
        public EndlesModeModel(BaseModeSo endlessModeData)
        {
           this.endlessModeData = (EndlessModeSo)endlessModeData;
        }
    }
}