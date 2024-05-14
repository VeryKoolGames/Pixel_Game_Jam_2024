using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "FishEvent", menuName = "Events/OnFishDeath")]
public class OnFishDeath : ScriptableObject
{
    // A list of responses to the event
    private List<OnFishDeathListener> listeners = new List<OnFishDeathListener>();

    public void Raise(FlockAgent fish) {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised(fish);
        }
    }

    public void RegisterListener(OnFishDeathListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnFishDeathListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}