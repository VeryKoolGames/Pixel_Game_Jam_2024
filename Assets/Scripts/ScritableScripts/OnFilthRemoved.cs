using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnFilthRemoved", menuName = "Events/OnFilthRemoved")]
public class OnFilthRemoved : ScriptableObject
{
    // A list of responses to the event
    private List<OnFilthRemovedListener> listeners = new List<OnFilthRemovedListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(OnFilthRemovedListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnFilthRemovedListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}