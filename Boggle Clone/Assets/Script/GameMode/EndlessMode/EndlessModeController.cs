using System.Collections.Generic;
using System.Linq;
using GameMode.EndlessMode;
using GameMode.Tiles;
using GameMode.BaseMode;
using Tile;
using UnityEngine;
using Utilities;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeController : BaseModeController
    {
        private EndlessModeView endlessModeView;
        private EndlesModeModel endlessModeModel;
        
        public EndlessModeController(EndlessModeView endlessModeView) : base(endlessModeView)
        {
            SetViewAndController(endlessModeView);
            InitializeValues();
            LoadWords();
            GenerateLetterGridWithWords();
            GenerateTileBlocks();
        }

        private void SetViewAndController(EndlessModeView endlessModeView)
        {
            this.endlessModeView = endlessModeView;
            endlessModeModel = new EndlesModeModel(this.endlessModeView.ModeData);
            baseModeModel = endlessModeModel;
            this.endlessModeView.SetController(this);
        }

        public override void OnTileDragStart(TileViewController tileViewController)
        {
            base.OnTileDragStart(tileViewController);
        }

        public override void OnTileDraggedOver(TileViewController tile)
        {
            base.OnTileDraggedOver(tile);
        }

        public override void OnTileDragEnd(TileViewController tile)
        {
            base.OnTileDragEnd(tile);
        }
    }
}