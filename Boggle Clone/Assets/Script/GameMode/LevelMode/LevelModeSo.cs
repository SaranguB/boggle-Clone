using System.Collections.Generic;
using GameMode.BaseMode;
using GameMode.Tiles;
using UnityEngine;

namespace Script.GameMode.LevelMode
{
    [CreateAssetMenu(fileName = "Level Mode", menuName = "ScriptableObjects/GameMode/LevelMode")]
    public class LevelModeSo :BaseModeSo
    {
        [Header("Level")] 
        public int numberOfLevels;

        public int selectedLevel;
        public int bugCount;
        public int wordCount;    
        public int timeSec;       
        public int totalScore;    
    
        public Vector2Int gridSize;  

        public List<TileData> gridData = new List<TileData>();
    }
    
    [System.Serializable]
    public class TileData
    {
        public TileType tileType;
        public string letter; 
    }
}