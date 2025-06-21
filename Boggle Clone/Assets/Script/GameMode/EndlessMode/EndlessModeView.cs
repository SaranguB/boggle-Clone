using System.Collections.Generic;
using GameMode.BaseMode;
using GameMode.EndlessMode;
using GameMode.Tiles;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Tile;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeView : BaseModeView
    {
        
        private EndlessModeController endlessModeController;

        
        public void SetController(EndlessModeController endlessModeController)
        {
           this.endlessModeController = endlessModeController;
        }

        
    }
}