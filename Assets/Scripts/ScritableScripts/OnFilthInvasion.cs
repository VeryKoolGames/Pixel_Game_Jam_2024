using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnFilthInvasion", menuName = "Events/OnFilthInvasion")]
public class OnFilthInvasion : ScriptableObject
{
    // A list of responses to the event
    private List<OnFilthInvasionListener> listeners = new List<OnFilthInvasionListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(OnFilthInvasionListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnFilthInvasionListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}