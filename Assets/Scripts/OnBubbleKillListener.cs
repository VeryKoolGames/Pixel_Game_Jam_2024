using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnBubbleKillListener : MonoBehaviour
{
    public OnBubbleKill Event;
    public UnityEvent<GameObject> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject bubble) {
        Response.Invoke(bubble);
    }
}