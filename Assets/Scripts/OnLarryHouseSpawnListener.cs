using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class OnLarryHouseSpawnListener : MonoBehaviour
{
    public OnLarryHouseSpawn Event;
    public UnityEvent<Transform> Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Transform houseTransform) {
        Response.Invoke(houseTransform);
    }
}