using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnFishDeathListener : MonoBehaviour
{
    public OnFishDeath Event;
    public UnityEvent<FlockAgent> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(FlockAgent fish) {
        Response.Invoke(fish);
    }
}