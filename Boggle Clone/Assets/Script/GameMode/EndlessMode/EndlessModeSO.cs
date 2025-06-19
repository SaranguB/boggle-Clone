using GameMode.Tiles;
using UnityEngine;

namespace GameMode.EndlessMode
{
    [CreateAssetMenu(fileName = "Endless Mode", menuName = "ScriptableObjects/GameMode/EndlessMode")]
    public class EndlessModeSo :ScriptableObject
    {
        public Vector2 gridSize;
        public TileViewController tilePrefab;
    }
}