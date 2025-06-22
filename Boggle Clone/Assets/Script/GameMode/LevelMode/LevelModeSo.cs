using System.Collections.Generic;
using GameMode.BaseMode;
using GameMode.Tiles;
using UnityEngine;

namespace GameMode.LevelMode
{
    [CreateAssetMenu(fileName = "Level Mode", menuName = "ScriptableObjects/GameMode/LevelMode")]
    public class LevelModeSo : BaseModeSo
    {
        [Header("Level")] 
        public int numberOfLevels;
        public int selectedLevel;
        public int bugCount;
        public int wordCount;
        public int timeSec;
        public int totalScore;
        public List<TileData> gridData;
    }
}