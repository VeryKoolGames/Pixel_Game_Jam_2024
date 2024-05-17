using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnBubbleKill", menuName = "Events/OnBubbleKill")]
public class OnBubbleKill : ScriptableObject
{
    // A list of responses to the event
    private List<OnBubbleKillListener> listeners = new List<OnBubbleKillListener>();

    public void Raise(GameObject bubble) {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised(bubble);
        }
    }

    public void RegisterListener(OnBubbleKillListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnBubbleKillListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}