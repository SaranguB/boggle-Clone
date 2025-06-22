using GameMode.BaseMode;
using Script.GameMode.LevelMode;
using UnityEngine;

namespace GameMode.LevelMode
{
    public class LevelModeModel : BaseModeModel
    {
        public LevelModeSo levelData  { get; private set; }
        public override Vector2 GridSize => levelData.gridSize;
        
        public LevelModeModel(BaseModeSo levelData)
        {
            this.levelData = (LevelModeSo)levelData;
        }
    }
}