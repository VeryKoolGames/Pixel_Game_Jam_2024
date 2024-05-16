using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public void OnObjectSpawn()
    {
        gameObject.SetActive(true);
    }

    public void OnObjectDeactivate()
    {
        gameObject.SetActive(false);
    }
}