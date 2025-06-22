using System.Collections.Generic;
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
        public Vector2Int gridSize;
        public List<TileData> gridData;
    }

    [System.Serializable]
    public class TileData
    {
        public TileType tileType;
        public string letter;
    }

    public enum TileType
    {
        Normal,
        Bonus,
        Blocked
    }

}