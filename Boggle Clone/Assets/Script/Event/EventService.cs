using Script.GameMode.LevelMode;
using UnityEngine;

namespace Events
{
    public class EventService
    {
        public EventController OnEndlessModeSelected;
        public EventController<int> OnLevelModeSelected;
        public EventService()
        {
            OnEndlessModeSelected = new EventController();
            OnLevelModeSelected = new EventController<int>();
        }

    }
}