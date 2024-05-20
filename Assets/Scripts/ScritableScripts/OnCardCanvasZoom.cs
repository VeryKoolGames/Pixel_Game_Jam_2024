using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "OnCardCanvasZoom", menuName = "Events/OnCardCanvasZoom")]
public class OnCardCanvasZoom : ScriptableObject
{
    // A list of responses to the event
    private List<OnCardCanvasZoomListener> listeners = new List<OnCardCanvasZoomListener>();

    public void Raise(bool isZoomed) {
        for(int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised(isZoomed);
        }
    }

    public void RegisterListener(OnCardCanvasZoomListener listener) {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(OnCardCanvasZoomListener listener) {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}