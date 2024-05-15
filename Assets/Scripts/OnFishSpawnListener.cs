using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnFishSpawnListener : MonoBehaviour
{
    public OnFishSpawn Event;
    public UnityEvent<GameObject> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameObject fish) {
        Response.Invoke(fish);
    }
}