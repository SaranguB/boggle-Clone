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

        protected override void OnWordValidated(List<TileViewController> usedTiles)
        {
            base.OnWordValidated(usedTiles);
            RefillGridAfterWords(usedTiles);
        }

        private void RefillGridAfterWords(List<TileViewController> usedTiles)
        {
            int rows = (int)baseModeModel.GridSize.y;
            int cols = (int)baseModeModel.GridSize.x;
    
            TileViewController[,] grid = GetTileGrid();
            
            foreach (var tile in usedTiles)
            {
                Vector2Int pos = tile.tileModel.gridPosition;
                ReturnToPool(tile);
                grid[pos.x, pos.y] = null;
            }
            
            ShiftTilesDownward(cols, rows, grid);
        }

        private void ShiftTilesDownward(int cols, int rows, TileViewController[,] grid)
        {
            for (int x = 0; x < cols; x++)
            {
                int writeRow = rows - 1;

                for (int y = rows - 1; y >= 0; y--)
                {
                    if (grid[x, y] != null)
                    {
                        if (y != writeRow)
                        {
                            var tile = grid[x, y];
                            grid[x, writeRow] = tile;
                            grid[x, y] = null;

                            tile.tileModel.gridPosition = new Vector2Int(x, writeRow);
                            tile.transform.SetSiblingIndex(writeRow * cols + x);
                        }
                        writeRow--;
                    }
                }
                
                InitializeNewTile(writeRow, x, cols, grid);
            }
        }

        private void InitializeNewTile(int writeRow, int x, int cols, TileViewController[,] grid)
        {
            for (int y = writeRow; y >= 0; y--)
            {
                TileViewController newTile = baseModeModel.tilePool.GetItem();
                char letter = GetRandomLetter();
                newTile.Initialize(this, y, x, letter, baseModeView.TileSo, baseModeModel.letterScores[letter]);
                newTile.transform.SetParent(baseModeView.TileGroupTransform, false);
                newTile.tileModel.gridPosition = new Vector2Int(x, y);
                newTile.transform.SetSiblingIndex(y * cols + x); 
                baseModeModel.tileList.Add(newTile);

                grid[x, y] = newTile;
            }
        }
    }
}
