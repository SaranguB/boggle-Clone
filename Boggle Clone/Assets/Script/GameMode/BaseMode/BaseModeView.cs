using GameMode.Tiles;
using UI.ScoreUI;
using UnityEngine;
using UnityEngine.UI;

namespace GameMode.BaseMode
{
    public class BaseModeView : MonoBehaviour
    {
        [field: SerializeField] public BaseModeSo ModeData { get; private set; }
        [field:SerializeField] public ScoreUIController ScoreUIController {get; private set;} 
        [Header("Tile")]
        [field: SerializeField]public TileSo TileSo {get; private set;}
        [field: SerializeField] public GridLayoutGroup ModeTileLayoutGround { get; private set; }
        [field: SerializeField] public Transform TileGroupTransform { get; private set; } 
    }
}