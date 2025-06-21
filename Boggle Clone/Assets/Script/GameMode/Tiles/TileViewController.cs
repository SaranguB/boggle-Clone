using System;
using Script.GameMode.Tiles;
using TMPro;
using UnityEngine;

namespace GameMode.Tiles
{
    public class TileViewController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI letterText;

        private TileModel tileModel;
        
        private void Awake()
        {
            tileModel = new TileModel();
        }

        public void Init(int row, int col, char letter)
        {
            
            letterText.text = letter.ToString();
        }
    }
}