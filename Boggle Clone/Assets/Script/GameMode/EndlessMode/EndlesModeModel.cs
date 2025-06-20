using System.Collections.Generic;
using GameMode.Tiles;
using Tile;

namespace GameMode.EndlessMode
{
    public class EndlesModeModel
    {
        public EndlessModeSo endlessModeData { get; private set; }
        
        public int gridSize;
        public List<TileViewController> tileList = new();
        public TilePool tilePool;
        public HashSet<string> wordSets = new();
        public char[,] letterGrid;
        
        public EndlesModeModel(EndlessModeSo endlessModeData)
        {
           this.endlessModeData = endlessModeData;
        }
    }
}