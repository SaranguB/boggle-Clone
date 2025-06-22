using System.Collections.Generic;
using GameMode.BaseMode;
using GameMode.Tiles;
using UnityEngine;

namespace GameMode.LevelMode
{
    public class LevelModeController : BaseModeController
    {
        private LevelData levelData;
        private LevelModeView levelModeView;
        private LevelModeModel levelModeModel;
        
        public LevelModeController(LevelModeView levelModeView, LevelModeSo levelSo) : base(levelModeView)
        {
            SetMVC(levelModeView, levelSo);
            InitializeValues();
            LoadWords();
            GenerateLetterGridWithWords();
            GenerateTileBlocks();
        }

        private void SetMVC(LevelModeView levelModeView, LevelModeSo levelSo)
        {
            this.levelModeView = levelModeView;
            Debug.Log(levelSo.selectedLevel);
            Debug.Log(levelSo.gridSize);
            levelModeModel  = new LevelModeModel(levelSo);
            baseModeModel = levelModeModel;
            this.levelModeView.SetController(this);
        }

        protected override void GenerateTileBlocks()
        {
            base.GenerateTileBlocks();
           
            int cols = (int)levelModeModel.levelData.gridSize.x;

            for (int i = 0; i < baseModeModel.tileList.Count; i++)
            {
                int row = i / cols;
                int col = i % cols;

                int index = row * cols + col;
                TileType type = levelModeModel.levelData.gridData[index].tileType;
                baseModeModel.tileList[i].SetTileType(type);
            }
        }

        protected override void GenerateLetterGridWithWords()
        {
            base.GenerateLetterGridWithWords();
            Vector2 gridSize = levelModeModel.GridSize; 
            int rows = (int)gridSize.y;
            int cols = (int)gridSize.x;

            levelModeModel.letterGrid = new char[rows, cols];
            
            for (int index = 0; index < levelModeModel.levelData.gridData.Count; index++)
            {
                int row = index / cols;
                int col = index % cols;

                TileData tileData = levelModeModel.levelData.gridData[index];
                
                levelModeModel.letterGrid[row, col] = tileData.letter.ToUpper()[0]; 
            }
        }
        
        protected override void OnWordValidated(List<TileViewController> currentDraggedTiles)
        {
            base.OnWordValidated(currentDraggedTiles);

            ProcessNeighbouringBlockedTiles(currentDraggedTiles);
            ProcessBugTiles(currentDraggedTiles);
        }

        private void ProcessBugTiles(List<TileViewController> currentDraggedTiles)
        {
            foreach (TileViewController tile in currentDraggedTiles)
            {
                if (tile.tileModel.tileType == TileType.Bonus)
                {
                    baseModeModel.temporaryScore += tile.tileModel.letterScore;
                    tile.SetTileType(TileType.Normal);
                }
            }
        }

        private void ProcessNeighbouringBlockedTiles(List<TileViewController> currentDraggedTiles)
        {
            TileViewController[,] grid = GetTileGrid();

            foreach (var tile in currentDraggedTiles)
            {
                Vector2Int pos = tile.tileModel.gridPosition;
                UnblockNeighboringTiles(pos, grid);
            }
        }

        private void UnblockNeighboringTiles(Vector2Int center, TileViewController[,] grid)
        {
            int rows = (int)baseModeModel.GridSize.y;
            int cols = (int)baseModeModel.GridSize.x;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = center.x + dx;
                    int ny = center.y + dy;

                    if (IsInBounds(nx, ny, cols, rows))
                    {
                        TryUnblockTile(nx, ny, grid, cols);
                    }
                }
            }
        }

        private bool IsInBounds(int x, int y, int cols, int rows)
        {
            return x >= 0 && x < cols && y >= 0 && y < rows;
        }

        private void TryUnblockTile(int x, int y, TileViewController[,] grid, int cols)
        {
            TileViewController tile = grid[x, y];
            int index = y * cols + x;

            if (tile != null && levelModeModel.levelData.gridData[index].tileType == TileType.Blocked)
            {
                levelModeModel.levelData.gridData[index].tileType = TileType.Normal;
                tile.SetTileType(TileType.Normal);
            }
        }
    }
}