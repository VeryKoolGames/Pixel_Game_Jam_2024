using System;
using FMOD.Studio;
using UnityEngine;

public class VacuumManager : MonoBehaviour
{
    [SerializeField] private float vacuumForce = 10f; // The force with which objects are pulled towards the tube
    [SerializeField] private float vacuumRange = 1f; // The range within which objects are affected
    [SerializeField] private float vacuumSpeed = 1f; // The range within which objects are affected
    [SerializeField] private float destroyDistance = .1f; 
    [SerializeField] private OnFilthRemoved onFilthRemoved;
    [SerializeField] private ObjectPool filthPool;
    [SerializeField] private DragNDropVacuum dragNDropVacuum;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the object should be vacuumed
        if (other.CompareTag("Vacuumable"))
        {
            other.transform.position = Vector2.MoveTowards(other.transform.position, transform.position, vacuumSpeed * Time.deltaTime);

            float distanceToVacuum = Vector2.Distance(transform.position, other.transform.position);
            if (distanceToVacuum <= destroyDistance)
            {
                onFilthRemoved.Raise();
                filthPool.ReturnObject(other.gameObject);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, vacuumRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            PLAYBACK_STATE playbackState;
            Debug.Log("Underwater");
            dragNDropVacuum.aspiSound.getPlaybackState(out playbackState);

            if (playbackState == PLAYBACK_STATE.PLAYING)
            {
                AudioManager.Instance.SetAmbienceParameter(dragNDropVacuum.aspiSound, "underWater", 0.8f);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            PLAYBACK_STATE playbackState;
            dragNDropVacuum.aspiSound.getPlaybackState(out playbackState);

            if (playbackState == PLAYBACK_STATE.PLAYING)
            {
                AudioManager.Instance.SetAmbienceParameter(dragNDropVacuum.aspiSound, "underWater", 1f);
            }
        }
    }
}