using Utilities;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        
        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
        }
        
        private void InitializeServices()
        {
            
        }
    }
}