using System.Collections.Generic;
using System.Linq;
using GameMode.Tiles;
using Script.GameMode.EndlessMode;
using Tile;
using UnityEngine;
using Utilities;

namespace GameMode.BaseMode
{
    public class BaseModeController
    {
        protected BaseModeModel baseModeModel;
        protected BaseModeView baseModeView;
        
        protected BaseModeController(BaseModeView baseModeView)
        {
           this.baseModeView = baseModeView;
        }

        protected void InitializeValues()
        {
            baseModeModel.tilePool = new TilePool(baseModeView.ModeData, baseModeView.TileGroupTransform);
            
            baseModeView.ModeTileLayoutGround.constraintCount = 
                (int)baseModeView.ModeData.gridSize.x;
            
            baseModeModel.TotalGridSize = (int)baseModeModel.GridSize.x * 
                                             (int)this.baseModeModel.GridSize.y;
        }
        
        protected void GenerateTileBlocks()
        {
            baseModeModel.tileList.Clear();
            
            int rows = (int)baseModeModel.GridSize.y;
            int cols = (int)baseModeModel.GridSize.x;
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    char letter = baseModeModel.letterGrid[row, col];
                    TileViewController tile = baseModeModel.tilePool.GetItem();
                    tile.transform.SetParent(baseModeView.TileGroupTransform, false);
                    tile.Initialize(this, row, col, letter, baseModeView.TileSo, baseModeModel.letterScores[letter]);
                    baseModeModel.tileList.Add(tile);
                }
            }
        }
        
        public virtual void OnTileDragStart(TileViewController tile)
        {
           baseModeModel.isDragging = true;
           baseModeModel.currentDraggedTiles.Clear();
           baseModeModel.selectedTiles.Clear();
           AddTileToCurrent(tile);
        }

        public virtual void OnTileDraggedOver(TileViewController tile)
        {
            if(!baseModeModel.isDragging || baseModeModel.selectedTiles.Contains(tile)) return;

            TileViewController lastTile = baseModeModel.currentDraggedTiles[^1];

            if (AreAdjacent(tile.tileModel.gridPosition, lastTile.tileModel.gridPosition))
            {
                AddTileToCurrent(tile);
            }
        }

        public virtual void OnTileDragEnd(TileViewController tile)
        {
            baseModeModel.isDragging = false;
            
            string word = string.Concat(baseModeModel.currentDraggedTiles.Select(t => t.tileModel.letter));
            Debug.Log($"Word formed: {word}");
            
            if (baseModeModel.wordSets.Contains(word.ToLower()))
            {
                Debug.Log("Valid word!");
                UpdateScoreAndUI();
                OnWordValidated(baseModeModel.currentDraggedTiles);
            }
            else
            {
                OnWordSelectedInvalid();
            }
            
            ClearTiles();
        }

        protected virtual void OnWordValidated(List<TileViewController> currentDraggedTiles)
        {
            
        }
        
        protected TileViewController[,] GetTileGrid()
        {
            int rows = (int)baseModeModel.GridSize.y;
            int cols = (int)baseModeModel.GridSize.x;

            TileViewController[,] grid = new TileViewController[cols, rows];

            foreach (var tile in baseModeModel.tileList)
            {
                Vector2Int pos = tile.tileModel.gridPosition;
                grid[pos.x, pos.y] = tile;
            }

            return grid;
        }


        private void UpdateScoreAndUI()
        {
            baseModeModel.totalScore += baseModeModel.temporaryScore;
            baseModeModel.temporaryScore = 0;
            baseModeModel.wordCount++;
            baseModeModel.averageScore = GetAverageScore();
            baseModeView.ScoreUIController.SetTexts(baseModeModel.averageScore, baseModeModel.totalScore);
        }

        private int GetAverageScore()
        {
          return baseModeModel.wordCount > 0 ? baseModeModel.totalScore / baseModeModel.wordCount : 0;
        }

        private void OnWordSelectedInvalid()
        {
            Debug.Log("Invalid word.");
            foreach (TileViewController tile in baseModeModel.currentDraggedTiles)
            {
                tile.SetInvalid(); 
            }
        }

        private void ClearTiles()
        {
            foreach (TileViewController tile in baseModeModel.currentDraggedTiles)
            {
                tile.SetSelected(false); 
            }
            
            baseModeModel.currentDraggedTiles.Clear();
            baseModeModel.selectedTiles.Clear();
        }

        private bool AreAdjacent(Vector2Int start, Vector2Int end)
        {
            int dx = Mathf.Abs(start.x - end.x);
            int dy = Mathf.Abs(start.y - end.y);
            return dx <= 1 && dy <= 1 && (dx + dy != 0);
        }
        
        private void AddTileToCurrent(TileViewController tile)
        {
           baseModeModel.currentDraggedTiles.Add(tile);
           baseModeModel.selectedTiles.Add(tile);
           tile.SetSelected(true);
           baseModeModel.temporaryScore += tile.tileModel.letterScore;
        }

        protected void LoadWords()
        {
            baseModeModel.wordSets = WordLoaderExtention.GetWordList("Word Dictionary/wordlist");
        }
        
        protected void GenerateLetterGridWithWords()
        {
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

        private void PlaceWord(string word, int row, int col, int dx, int dy)
        {
            for (int i = 0; i < word.Length; i++)
            {
                baseModeModel.letterGrid[row + i * dy, col + i * dx] = word[i];
            }
        }
        
       

        protected char GetRandomLetter()
        {
            return (char)('A' + Random.Range(0, 26)); 
        }
        
        protected void ReturnToPool(TileViewController tile)
        {
            baseModeModel.tilePool.ReturnItem(tile);
        }

      
    }
}