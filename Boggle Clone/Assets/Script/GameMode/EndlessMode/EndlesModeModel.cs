namespace GameMode.EndlessMode
{
    public class EndlesModeModel
    {
        public EndlessModeSo endlessModeData { get; private set; }
        public int gridSize;
        public EndlesModeModel(EndlessModeSo endlessModeData)
        {
           this.endlessModeData = endlessModeData;
        }
    }
}