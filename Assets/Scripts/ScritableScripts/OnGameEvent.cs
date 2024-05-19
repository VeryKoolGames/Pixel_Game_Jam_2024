using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnGameEvent", menuName = "Events/OnGameEvent")]
public class OnGameEvent : ScriptableObject
{
    // A list of responses to the event
    private List<OnGameEventListener> listeners = new List<OnGameEventListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(OnGameEventListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnGameEventListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}