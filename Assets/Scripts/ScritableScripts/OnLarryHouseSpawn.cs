using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnLarryHouseSpawn", menuName = "Events/OnLarryHouseSpawn")]
public class OnLarryHouseSpawn : ScriptableObject
{
    // A list of responses to the event
    private List<OnLarryHouseSpawnListener> listeners = new List<OnLarryHouseSpawnListener>();

    public void Raise(Transform houseTransform) {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised(houseTransform);
        }
    }

    public void RegisterListener(OnLarryHouseSpawnListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnLarryHouseSpawnListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}