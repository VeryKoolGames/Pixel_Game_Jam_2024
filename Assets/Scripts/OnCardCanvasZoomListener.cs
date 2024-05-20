using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnCardCanvasZoomListener : MonoBehaviour
{
    public OnCardCanvasZoom Event;
    public UnityEvent<bool> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(bool isZoomed) {
        Response.Invoke(isZoomed);
    }
}