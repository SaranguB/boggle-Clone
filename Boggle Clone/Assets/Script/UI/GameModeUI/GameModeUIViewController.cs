using System;
using Main;
using Script.GameMode.LevelMode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.GameModeUI
{
    public class GameModeUIViewController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup gameModeUICanvas;

        [Header("Endless Mode")]
        [SerializeField] private Button endlessModeButton;

        [Header("Level Mode")]
        [SerializeField] private Button levelModeButton;
        [SerializeField] private LevelModeSo levelData;
        [SerializeField] private CanvasGroup levelModeCanvas;
        [SerializeField] private GameObject levelButtonPrefab;

        private void Start()
        {
            GenerateLevelButtons();
            endlessModeButton.onClick.AddListener(OnEndlessModeClicked);
            levelModeButton.onClick.AddListener(OnLevelModeClicked);
        }

        private void OnDestroy()
        {
            endlessModeButton.onClick.RemoveListener(OnEndlessModeClicked);
            levelModeButton.onClick.RemoveListener(OnLevelModeClicked);
        }

        private void OnEndlessModeClicked()
        {
            CanvasGroupExtension.Hide(gameModeUICanvas);
            GameManager.Instance.gameModeServices.GameModeController.OnEndlessModeSelected();
        }

        private void OnLevelModeClicked()
        {
            CanvasGroupExtension.Hide(gameModeUICanvas);
            CanvasGroupExtension.Show(levelModeCanvas);
        }

        private void GenerateLevelButtons()
        {
            for (int i = 1; i <= levelData.numberOfLevels; i++)
            {
                CreateLevelButton(i);
            }
        }

        private void CreateLevelButton(int levelNumber)
        {
            GameObject levelButtonObject = Instantiate(levelButtonPrefab, levelModeCanvas.transform);

            TextMeshProUGUI levelText = levelButtonObject.GetComponentInChildren<TextMeshProUGUI>();
            if (levelText != null)
            {
                levelText.text = $"Level {levelNumber}";
            }
            
            if (levelButtonObject.TryGetComponent<Button>(out var levelButton))
            {
                levelButton.onClick.AddListener(() => OnLevelSelected(levelNumber));
            }
            else
            {
                Debug.LogWarning($"Button component missing on level button prefab!");
            }
        }

        private void OnLevelSelected(int selectedLevel)
        {
            Debug.Log($"Level {selectedLevel} selected.");

            levelData.selectedLevel = selectedLevel;
            GameManager.Instance.eventService.OnLevelModeSelected.InvokeEvent(levelData);
        }
    }
}
