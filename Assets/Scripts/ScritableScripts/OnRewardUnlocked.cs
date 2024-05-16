using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnRewardsUnlocked", menuName = "Events/OnRewardUnlocked")]
public class OnRewardUnlocked : ScriptableObject
{
    // A list of responses to the event
    private List<OnRewardUnlockedListener> listeners = new List<OnRewardUnlockedListener>();

    public void Raise() {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(OnRewardUnlockedListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnRewardUnlockedListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}