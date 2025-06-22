namespace GameMode.Tiles
{
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