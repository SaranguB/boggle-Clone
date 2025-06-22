using GameMode.Tiles;
using UnityEngine;

namespace GameMode.Tiles
{
    public class TileModel
    {
        public char letter;
        public Vector2Int gridPosition;
        public int letterScore;
        public TileSo tileSo {get; private set ;}
        public TileType tileType;

        public void SetTileSO(TileSo tileSo)
        {
            this.tileSo = tileSo;
        }
    }
}