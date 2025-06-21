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
        protected BaseModeController(EndlessModeView endlessModeView)
        {
           baseModeView = endlessModeView;
        }

        protected void InitializeValues()
        {
            baseModeModel.tilePool = new TilePool(baseModeView.ModeData, baseModeView.TileGroupTransform);
            
            baseModeView.ModeTileLayoutGround.constraintCount = 
                (int)baseModeView.ModeData.gridSize.x;
            
            baseModeModel.TotalGridSize = (int)baseModeModel.GridSize.x * 
                                             (int)this.baseModeModel.GridSize.y;
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
                    tile.Init(row, col, letter);
                    baseModeModel.tileList.Add(tile);
                }
            }
        }

        protected char GetRandomLetter()
        {
            return (char)('A' + Random.Range(0, 26)); 
        }
        
        private void ReturnToPool(TileViewController tile)
        {
            baseModeModel.tilePool.ReturnItem(tile);
        }
    }
}