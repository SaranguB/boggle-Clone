using GameMode.BaseMode;
using Script.GameMode.LevelMode;
using UnityEngine;

namespace Events
{
    public class EventService
    {
        public EventController OnEndlessModeSelected;
        public EventController<LevelModeSo> OnLevelModeSelected;
        public EventService()
        {
            OnEndlessModeSelected = new EventController();
            OnLevelModeSelected = new EventController<LevelModeSo>();
        }

    }
}