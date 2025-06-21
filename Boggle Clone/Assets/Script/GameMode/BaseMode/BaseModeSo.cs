using GameMode.Tiles;
using UnityEngine;

namespace GameMode.BaseMode
{
    public class BaseModeSo :ScriptableObject
    {
       [field:SerializeField] public Vector2 gridSize { get;private set; }
       [field:SerializeField] public TileViewController tilePrefab{ get;private set; }
    }
}