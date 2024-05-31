using System.Collections.Generic;
using FMOD.Studio;
using KBCore.Refs;
using UnityEngine;

public class DragNDropVacuum : ValidatedMonoBehaviour
{
    [SerializeField] private Collider2D dragZone; // Collider2D to define the drag area

    private bool isDragging;
    private Vector3 offset;
    [SerializeField, Self] private Collider2D itemCollider;
    [SerializeField, Self] private SpriteRenderer spriteRenderer;

    public List<GameObject> tuyaux = new List<GameObject>();
    public Transform stillPointTuyaux;
    public Transform stillPointHead;
    public EventInstance aspiSound;
    [SerializeField] private GameObject particleEffect;

    private Vector3 previousMousePosition;

    private void Start()
    {
        aspiSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.aspiNoise);
    }

    void Update()
    {
        if (isDragging)
        {
            Drag();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPosition, Vector2.zero);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider == itemCollider)
                {
                    OnMouseDown();
                    break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnMouseUp();
            stopSound();
        }

        if (!isDragging)
        {
            GoToStillPoint();
        }
    }

    void GoToStillPoint()
    {
        foreach (GameObject tuyau in tuyaux)
        {
            Rigidbody2D rb = tuyau.GetComponent<Rigidbody2D>();
            Vector2 dir = stillPointTuyaux.transform.position - tuyau.transform.position;
            rb.velocity = dir * 3;
        }

        Rigidbody2D thisRb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 thisDir = stillPointHead.transform.position - transform.position;
        thisRb.velocity = thisDir * 10;
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    void OnMouseDown()
    {
        playSound();
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        particleEffect.SetActive(true);
        previousMousePosition = Input.mousePosition; // Initialize previous mouse position
    }

    void OnMouseUp()
    {
        isDragging = false;
        particleEffect.SetActive(false);
    }

    private void playSound()
    {
        PLAYBACK_STATE playbackState;
        aspiSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            aspiSound.start();
        }
    }

    private void stopSound()
    {
        aspiSound.stop(STOP_MODE.ALLOWFADEOUT);
    }

    void Drag()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 newPosition = mousePosition + offset;
        Rigidbody2D vacuumRB = gameObject.GetComponent<Rigidbody2D>();
        Vector2 dir = (Vector2)mousePosition - (Vector2)gameObject.transform.position;

        // Constrain the new position within the drag zone
        if (dragZone.OverlapPoint(new Vector2(newPosition.x, newPosition.y)))
        {
            vacuumRB.velocity += dir;

            // Check if the mouse position has changed
            if (previousMousePosition != Input.mousePosition)
            {
                RotateToFaceDirection(dir);
                previousMousePosition = Input.mousePosition; // Update previous mouse position
            }
        }
    }

    public void RotateToFaceDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane; // Ensure proper conversion to world space
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    // }
}
