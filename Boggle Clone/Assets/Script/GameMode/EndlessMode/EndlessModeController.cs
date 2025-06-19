using System.Collections.Generic;
using GameMode.EndlessMode;
using GameMode.Tiles;
using UnityEngine;
using Wepons.Tile;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeController 
    {
        private EndlessModeView endlessModeView;
        private EndlesModeModel endlessModeModel;
        private List<TileViewController> tileList = new();
        private TilePool tilePool;
        
        public EndlessModeController(EndlessModeView endlessModeView)
        {
            InitializeValues(endlessModeView);
            GenerateTileBlocks();
        }

        private void InitializeValues(EndlessModeView endlessModeView)
        {
            this.endlessModeView = endlessModeView;
            endlessModeModel = new EndlesModeModel(endlessModeView.endlessModeData);
            this.endlessModeView.SetController(this);
            
            tilePool = new TilePool(this.endlessModeView.endlessModeData, this.endlessModeView.tileGroupTransform);
            
            this.endlessModeView.endlessModeTileLayoutGround.constraintCount = 
                (int)endlessModeView.endlessModeData.gridSize.x;
            endlessModeModel.gridSize = (int)endlessModeModel.endlessModeData.gridSize.x * (int)this.endlessModeModel.endlessModeData.gridSize.y;
        }

        private void GenerateTileBlocks()
        {
            tileList.Clear();
            
           for(int i=0;i<endlessModeModel.gridSize;i++)
           {
               TileViewController tile = tilePool.GetItem();;
               tileList.Add(tile);
           }
        }

        private void ReturnToPool(TileViewController tile)
        {
            tilePool.ReturnItem(tile);
        }
    
    }
}