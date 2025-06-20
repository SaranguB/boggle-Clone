using TMPro;
using UnityEngine;

namespace GameMode.Tiles
{
    public class TileViewController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI letterText;
        
        public void Init(int row, int col, char letter)
        {
            letterText.text = letter.ToString();
        }
    }
}