using System.Collections.Generic;
using GameMode.EndlessMode;
using GameMode.Tiles;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Wepons.Tile;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeView : MonoBehaviour
    {
        [field: SerializeField] public EndlessModeSo endlessModeData { get; private set; }
        
        [Header("Tile")]
        [field: SerializeField] public GridLayoutGroup endlessModeTileLayoutGround { get; private set; }
        [field: SerializeField] public Transform tileGroupTransform { get; private set; } 
        
        private EndlessModeController endlessModeController;

        
        
        public void SetController(EndlessModeController endlessModeController)
        {
           this.endlessModeController = endlessModeController;
        }

        
    }
}