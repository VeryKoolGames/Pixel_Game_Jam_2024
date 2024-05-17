using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnRewardUnlockedListener : MonoBehaviour
{
    public OnRewardUnlocked Event;
    public UnityEvent<int> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(int rarety) {
        Response.Invoke(rarety);
    }
}