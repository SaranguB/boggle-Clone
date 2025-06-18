using Main;

using UnityEngine;

namespace UI
{
    public class UIService : MonoBehaviour
    {
        private void Start()
        {
            InitializeUIControllers();
            RegisterUI();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
           
            
        }

        private void OnDestroy()
        {
            UnSubscribeToEvents();
        }

        private void UnSubscribeToEvents()
        {
           
        }

        private void InitializeUIControllers()
        {
            
        }

        private void RegisterUI()
        {
           
        }

        public void CreateLevelLostUI()
        {
            
        }


        public void CreateLevelWonUI()
        {
            
        }
        
    }
}