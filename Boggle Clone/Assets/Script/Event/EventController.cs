using System;

namespace Events
{
    public class EventController
    {
        public event Action baseEvent;
        public void InvokeEvent() => baseEvent?.Invoke();
        public void AddListener(Action listener) => baseEvent += listener;
        public void RemoveListener(Action listener) => baseEvent -= listener;
    }

    public class EventController<T1>
    {
        public event Action<T1> baseEvent;
        public void InvokeEvent(T1 type) => baseEvent?.Invoke(type);
        public void AddListener(Action<T1> listener) => baseEvent += listener;
        public void RemoveListener(Action<T1> listener) => baseEvent -= listener;
    }
}