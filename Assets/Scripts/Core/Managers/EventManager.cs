using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Managers
{
    public abstract class GameEvent
    {
        public void Raise(bool raiseOnce = false)
        {
            EventManager.Raise(this, raiseOnce);
        }
    }

    public class EventManager
    {
        private static EventManager instance;

        private static EventManager Instance
        {
            get { return instance ??= new EventManager(); }
        }

        public delegate void EventDelegate<in T>(T @event) where T : GameEvent;

        private readonly Dictionary<Type, Delegate> _delegates = new();

        public static void AddListener<T>(EventDelegate<T> @delegate) where T : GameEvent
        {
            var delegates = Instance._delegates;

            if (delegates.ContainsKey(typeof(T)))
            {
                var tempDelegate = delegates[typeof(T)];

                delegates[typeof(T)] = Delegate.Combine(tempDelegate, @delegate);
            }
            else
            {
                delegates[typeof(T)] = @delegate;
            }
        }

        public static void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            var delegates = Instance._delegates;

            if (!delegates.ContainsKey(typeof(T)))
                return;

            var currentDel = Delegate.Remove(delegates[typeof(T)], del);
            if (currentDel == null)
            {
                delegates.Remove(typeof(T));
            }
            else
            {
                delegates[typeof(T)] = currentDel;
            }
        }

        public static void Raise(GameEvent @event, bool raiseOnce = false)
        {
            if (@event == null)
            {
                Debug.Log("Invalid event argument");
                return;
            }

            var eventType = @event.GetType();
            var delegatesToInvoke = Instance._delegates.Where(pair => pair.Key == eventType || eventType.IsSubclassOf(pair.Key));
            foreach (var pair in delegatesToInvoke)
            {
                try
                {
                    pair.Value.DynamicInvoke(@event);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error while invoking delegate for {eventType}\n{e}");
                }

                if (raiseOnce)
                    Instance._delegates.Remove(eventType);
            }
        }

        public static void ClearAll()
        {
            Instance._delegates.Clear();
        }
    }
}