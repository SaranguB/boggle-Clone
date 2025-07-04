using UnityEngine;

namespace GameMode.Tiles
{
    [CreateAssetMenu(fileName = "TileSo", menuName = "ScriptableObjects/GameMode/TileSo")]
    
    public class TileSo :ScriptableObject
    {
        [field: SerializeField] public Color defaultColor { get; private set; }
        [field: SerializeField] public Color selectedColor { get; private set; }
        [field: SerializeField] public Color invalidColor { get; private set; }
    }
}