using UnityEngine;

public class VacuumManager : MonoBehaviour
{
    [SerializeField] private float vacuumForce = 10f; // The force with which objects are pulled towards the tube
    [SerializeField] private float vacuumRange = 1f; // The range within which objects are affected
    [SerializeField] private float vacuumSpeed = 1f; // The range within which objects are affected
    [SerializeField] private float destroyDistance = .1f; 

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object should be vacuumed
        if (other.CompareTag("Vacuumable"))
        {
            other.transform.position = Vector2.MoveTowards(other.transform.position, transform.position, vacuumSpeed * Time.deltaTime);

            float distanceToVacuum = Vector2.Distance(transform.position, other.transform.position);
            if (distanceToVacuum <= destroyDistance)
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Kelp"))
        {
            Debug.Log("Detected Kelp");
            other.transform.position = Vector2.MoveTowards(other.transform.position, transform.position, vacuumSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the vacuum range in the editor for visualization
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, vacuumRange);
    }
}