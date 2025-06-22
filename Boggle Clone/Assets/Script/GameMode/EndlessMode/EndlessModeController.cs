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
            SetMVC(endlessModeView);
            InitializeValues();
            LoadWords();
            GenerateLetterGridWithWords();
            GenerateTileBlocks();
        }

        private void SetMVC(EndlessModeView endlessModeView)
        {
            this.endlessModeView = endlessModeView;
            endlessModeModel = new EndlesModeModel(this.endlessModeView.ModeData);
            baseModeModel = endlessModeModel;
            this.endlessModeView.SetController(this);
        }

        protected override void GenerateTileBlocks()
        {
            base.GenerateTileBlocks();
            foreach (TileViewController tile in baseModeModel.tileList)
            {
                tile.SetTileType(TileType.Normal);
            }
        }

        protected override void GenerateLetterGridWithWords()
        {
            base.GenerateLetterGridWithWords();
            int rows = (int)baseModeModel.GridSize.y; 
            int cols = (int)baseModeModel.GridSize.x; 
            baseModeModel.letterGrid = new char[rows, cols];
           
            List<string> canidateWords = baseModeModel.wordSets
                .Where(w => w.Length == 3)
                .OrderBy(_ => Random.value)
                .Take(3)
                .ToList();

            foreach (string word in canidateWords)
            {
                bool success = TryPlacingWordInLetterGrid(word.ToUpper(), rows, cols);
                Debug.Log($"Trying to place word: {word} → {(success ? "Success" : "Failed")}");
            }
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (baseModeModel.letterGrid[i, j] == '\0')
                        baseModeModel.letterGrid[i, j] = GetRandomLetter();
                }
            }
        }
        
        private bool TryPlacingWordInLetterGrid(string word, int rows, int cols)
        {
            const int maxAttempts = 100;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                int row = Random.Range(0, rows);
                int col = Random.Range(0, cols);
                var (dx, dy) = GetRandomDirection();

                if (!IsWithinBounds(row, col, dx, dy, word.Length, rows, cols))
                    continue;

                if (!HasConflict(word, row, col, dx, dy))
                {
                    PlaceWord(word, row, col, dx, dy);
                    return true;
                }
            }

            Debug.LogWarning($"Failed to place word: {word} after {maxAttempts} attempts.");
            return false;
        }
        
        private (int dx, int dy) GetRandomDirection()
        {
            List<(int dx, int dy)> directions = new()
            {
                (1, 0), (-1, 0), (0, 1), (0, -1),
                (1, 1), (-1, -1), (1, -1), (-1, 1)
            };

            return directions[Random.Range(0, directions.Count)];
        }

        private bool IsWithinBounds(int row, int col, int dx, int dy, int length, int rows, int cols)
        {
            int endRow = row + (length - 1) * dy;
            int endCol = col + (length - 1) * dx;

            return endRow >= 0 && endRow < rows && endCol >= 0 && endCol < cols;
        }

        private bool HasConflict(string word, int row, int col, int dx, int dy)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char existing = baseModeModel.letterGrid[row + i * dy, col + i * dx];
                if (existing != '\0' && existing != word[i])
                    return true;
            }
            return false;
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
