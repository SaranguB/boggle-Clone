using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.MainMenuUI
{
    public class MainMenuUIViewController : MonoBehaviour
    {
        [SerializeField] private Button PlayButton;
        [SerializeField] private Button QuitButton;
        [SerializeField] private CanvasGroup gameModeButtonCanvas;
        [SerializeField] private CanvasGroup mainMenuCanvas;
        
        private void Start()
        {
            PlayButton.onClick.AddListener(OnPlayButtonClicked);
            QuitButton.onClick.AddListener(OnquitButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            CanvasGroupExtension.Hide(mainMenuCanvas);
            CanvasGroupExtension.Show(gameModeButtonCanvas);
            
        }

        private void OnquitButtonClicked()
        {
            Application.Quit();
        }
    }
}