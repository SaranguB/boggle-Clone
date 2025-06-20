using System.Collections.Generic;
using System.Linq;
using GameMode.EndlessMode;
using GameMode.Tiles;
using Tile;
using UnityEngine;
using Utilities;

namespace Script.GameMode.EndlessMode
{
    public class EndlessModeController 
    {
        private EndlessModeView endlessModeView;
        private EndlesModeModel endlessModeModel;
        
        public EndlessModeController(EndlessModeView endlessModeView)
        {
            InitializeValues(endlessModeView);
            LoadWords();
            GenerateLetterGridWithWords();
            GenerateTileBlocks();
        }
        
        private void InitializeValues(EndlessModeView endlessModeView)
        {
            this.endlessModeView = endlessModeView;
            endlessModeModel = new EndlesModeModel(endlessModeView.endlessModeData);
            this.endlessModeView.SetController(this);
            
           endlessModeModel.tilePool = new TilePool(this.endlessModeView.endlessModeData, this.endlessModeView.tileGroupTransform);
            
            this.endlessModeView.endlessModeTileLayoutGround.constraintCount = 
                (int)endlessModeView.endlessModeData.gridSize.x;
            
            endlessModeModel.gridSize = (int)endlessModeModel.endlessModeData.gridSize.x * 
                                        (int)this.endlessModeModel.endlessModeData.gridSize.y;
        }
        
        private void LoadWords()
        {
            endlessModeModel.wordSets = WordLoaderExtention.GetWordList("Word Dictionary/wordlist");
        }
        
        private void GenerateLetterGridWithWords()
        {
            int rows = (int)endlessModeModel.endlessModeData.gridSize.y; 
            int cols = (int)endlessModeModel.endlessModeData.gridSize.x; 
           endlessModeModel.letterGrid = new char[rows, cols];
           
           List<string> canidateWords = endlessModeModel.wordSets
               .Where(w => w.Length >= 3 && w.Length <= 4)
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
                   if (endlessModeModel.letterGrid[i, j] == '\0')
                       endlessModeModel.letterGrid[i, j] = GetRandomLetter();
               }
           }
           
        }
        
        private char GetRandomLetter()
        {
            return (char)('A' + Random.Range(0, 26)); 
        }

        private bool TryPlacingWordInLetterGrid(string word, int rows, int cols)
        {
            const int maxAttempts = 500;

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
                char existing = endlessModeModel.letterGrid[row + i * dy, col + i * dx];
                if (existing != '\0' && existing != word[i])
                    return true;
            }
            return false;
        }

        private void PlaceWord(string word, int row, int col, int dx, int dy)
        {
            for (int i = 0; i < word.Length; i++)
            {
                endlessModeModel.letterGrid[row + i * dy, col + i * dx] = word[i];
            }
        }

        private void GenerateTileBlocks()
        {
            endlessModeModel.tileList.Clear();
            
            int rows = (int)endlessModeModel.endlessModeData.gridSize.y;
            int cols = (int)endlessModeModel.endlessModeData.gridSize.x;
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    char letter = endlessModeModel.letterGrid[row, col];
                    TileViewController tile = endlessModeModel.tilePool.GetItem();
                    tile.transform.SetParent(endlessModeView.tileGroupTransform, false);
                    tile.Init(row, col, letter);
                    endlessModeModel.tileList.Add(tile);
                }
            }
        }

     
        private void ReturnToPool(TileViewController tile)
        {
            endlessModeModel.tilePool.ReturnItem(tile);
        }
    
    }
}