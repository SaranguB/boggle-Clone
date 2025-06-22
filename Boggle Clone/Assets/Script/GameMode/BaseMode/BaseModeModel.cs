using System.Collections.Generic;
using GameMode.Tiles;
using Tile;
using UnityEngine;

namespace GameMode.BaseMode
{
    public class BaseModeModel
    {
        public int totalGridSize;
        public List<TileViewController> tileList = new();
        public TilePool tilePool;
        public HashSet<string> wordSets = new();
        public char[,] letterGrid;
        public int temporaryScore;
        public int totalScore;
        public int averageScore;
        public int wordCount;

        public bool isDragging = false;
        public List<TileViewController> currentDraggedTiles = new();
        public HashSet<TileViewController> selectedTiles = new();
        
        public virtual Vector2 GridSize => new Vector2(4, 4);

        public Dictionary<char, int> letterScores = new Dictionary<char, int>
        {
            { 'A', 1 }, { 'E', 1 }, { 'I', 1 }, { 'O', 1 }, { 'U', 1 },
            { 'L', 1 }, { 'N', 1 }, { 'S', 1 }, { 'T', 1 }, { 'R', 1 },

            { 'D', 2 }, { 'G', 2 }, { 'M', 2 },

            { 'B', 3 }, { 'C', 3 }, { 'P', 3 }, { 'F', 3 },
            { 'H', 3 }, { 'V', 3 }, { 'W', 3 }, { 'Y', 3 },

            { 'K', 4 }, { 'J', 5 }, { 'X', 5 }, { 'Q', 10 }, { 'Z', 10 }
        };
        
    }
}