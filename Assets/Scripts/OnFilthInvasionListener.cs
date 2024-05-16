using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnFilthInvasionListener : MonoBehaviour
{
    public OnFilthInvasion Event;
    public UnityEvent Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised() {
        Response.Invoke();
    }
}