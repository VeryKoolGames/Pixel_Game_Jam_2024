using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnFilthClean", menuName = "Events/OnFilthClean")]
public class OnFilthClean : ScriptableObject
{
    // A list of responses to the event
    private List<OnFilthCleanListener> listeners = new List<OnFilthCleanListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(OnFilthCleanListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnFilthCleanListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}