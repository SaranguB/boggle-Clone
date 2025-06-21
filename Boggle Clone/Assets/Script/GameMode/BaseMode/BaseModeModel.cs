using System.Collections.Generic;
using GameMode.Tiles;
using Tile;
using UnityEngine;

namespace GameMode.BaseMode
{
    public class BaseModeModel
    {
        public int TotalGridSize;
        public List<TileViewController> tileList = new();
        public TilePool tilePool;
        public HashSet<string> wordSets = new();
        public char[,] letterGrid;
        
        public virtual Vector2 GridSize => new Vector2(4, 4);
        
    }
}