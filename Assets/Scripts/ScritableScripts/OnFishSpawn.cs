using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "FishEvent", menuName = "Events/FishEvent")]
public class OnFishSpawn : ScriptableObject
{
    // A list of responses to the event
    private List<OnFishSpawnListener> listeners = new List<OnFishSpawnListener>();

    public void Raise(GameObject fish) {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised(fish);
        }
    }

    public void RegisterListener(OnFishSpawnListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnFishSpawnListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}