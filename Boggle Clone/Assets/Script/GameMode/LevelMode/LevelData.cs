using System.Collections.Generic;
using GameMode.Tiles;
using UnityEngine;

namespace GameMode.LevelMode
{
    [System.Serializable]
    public class LevelData
    {
        public int selectedLevel;
        public int bugCount;
        public int wordCount;
        public int timeSec;
        public int totalScore;
        public Vector2 gridSize;
        public List<TileData> gridData;
    }
}