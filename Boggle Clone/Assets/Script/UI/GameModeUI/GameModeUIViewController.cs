using System;
using Main;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameModeUI
{
    public class GameModeUIViewController : MonoBehaviour
    {
        [SerializeField] private Button EndlessModeButton;
        [SerializeField] private Button LevelModeButton;

        private void Start()
        {
            EndlessModeButton.onClick.AddListener(OnEndlessModeClicked);
        }

        private void OnEndlessModeClicked()
        {
            GameManager.Instance.gameModeServices.GameModeController.OnEndlessModeSelected();
        }
    }
}