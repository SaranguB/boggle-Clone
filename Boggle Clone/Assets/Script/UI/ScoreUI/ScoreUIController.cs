using TMPro;
using UnityEngine;

namespace UI.ScoreUI
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI averageScoreText;
        [SerializeField] private TextMeshProUGUI totalScoreText;
        
        public void SetTexts(int averageScore, int totalScore)
        {
            averageScoreText.text = averageScore.ToString();
            totalScoreText.text = totalScore.ToString();
        }
    }
}