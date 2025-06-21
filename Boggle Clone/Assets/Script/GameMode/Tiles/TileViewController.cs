using System;
using GameMode.BaseMode;
using Script.GameMode.Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameMode.Tiles
{
    public class TileViewController : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler, IPointerUpHandler
    {
        [SerializeField] private TextMeshProUGUI letterText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image score1;
        [SerializeField] private Image score2;
        [SerializeField] private Image score3;
        
        private BaseModeController modeController;
        
        public TileModel tileModel {get; set;}
        
        private void Awake()
        {
            tileModel = new TileModel();
        }

        public void Initialize(BaseModeController modeController, int row, int column, char letter, TileSo tileSo,
            int letterScore)
        {
            tileModel.letterScore = letterScore;
            SetScore();
            tileModel.SetTileSO(tileSo);
            this.modeController = modeController;
            tileModel.letter = letter;
            tileModel.gridPosition = new Vector2Int(column, row);
            letterText.text = letter.ToString();
        }

        private void SetScore()
        {
          score1.gameObject.SetActive(tileModel.letterScore >= 1);
          score2.gameObject.SetActive(tileModel.letterScore >= 2);
          score3.gameObject.SetActive(tileModel.letterScore >= 3);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            modeController.OnTileDragStart(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            modeController.OnTileDraggedOver(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            modeController.OnTileDragEnd(this);
        }
        
        public void SetSelected(bool isSelected)
        {
            backgroundImage.color = isSelected ? tileModel.tileSo.selectedColor : tileModel.tileSo.defaultColor;
        }

        public void SetInvalid()
        {
            backgroundImage.color = tileModel.tileSo.invalidColor;
        }
        
    }
}