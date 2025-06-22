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
            
            baseModeModel.totalGridSize = (int)baseModeModel.GridSize.x * 
                                             (int)this.baseModeModel.GridSize.y;
        }
        
        protected virtual void GenerateTileBlocks()
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
                    tile.Initialize(this, row, col, letter, baseModeView.TileSo, 
                        baseModeModel.letterScores[letter]);
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
                OnWordValidated(baseModeModel.currentDraggedTiles);
                UpdateScoreAndUI();
            
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
        
        protected virtual void GenerateLetterGridWithWords()
        {
          
           
        }
        
        protected void PlaceWord(string word, int row, int col, int dx, int dy)
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