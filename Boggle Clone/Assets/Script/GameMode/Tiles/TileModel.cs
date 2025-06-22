using UnityEngine;

namespace GameMode.Tiles
{
    public class TileModel
    {
        public char letter;
        public Vector2Int gridPosition;
        public int letterScore;
        public TileType tileType;
        public TileSo tileSo {get; private set ;}
        
        public void SetTileSO(TileSo tileSo)
        {
            this.tileSo = tileSo;
        }
    }
}