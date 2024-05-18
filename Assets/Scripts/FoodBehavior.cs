using KBCore.Refs;
using UnityEngine;

public class FoodBehavior : ValidatedMonoBehaviour
{
    [SerializeField] private float waterDragFactor = 0.5f; // Factor to reduce the speed when in water
    [SerializeField, Self] private Rigidbody2D rb;
    private bool isInWater = false;

    void FixedUpdate()
    {
        if (isInWater)
        {
            ApplyWaterDrag();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water") && gameObject.CompareTag("Finish"))
        {
            isInWater = true;
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.waterDrop, transform.position);
            ApplyWaterDrag();
        }
    }

    private void ApplyWaterDrag()
    {
        if (rb != null)
        {
            rb.velocity *= waterDragFactor;
        }
    }
}
