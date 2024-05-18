using System;
using System.Collections;
using DG.Tweening;
using FMOD.Studio;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : ValidatedMonoBehaviour
{
    [SerializeField] private Collider2D dragZone; // Collider2D to define the drag area
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float shakeThreshold = 1.0f; // Adjust as needed
    [SerializeField] private float shakeCooldown = 0.5f; // Time between shakes

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 lastMousePosition;
    private float shakeTimer = 0f;
    private Vector3 startPosition;
    [SerializeField, Self] private Collider2D itemCollider;
    [SerializeField, Self] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite upsideDownSprite;
    [SerializeField] private Sprite highlightSprite;
    private Sprite baseSprite;
    private EventInstance shakeSound;
    private bool isSoundPlaying = false;
    [SerializeField] private float checkInterval = 0.2f; // Time interval to check for movement
    private Vector3 lastPosition;

    private void Start()
    {
        baseSprite = spriteRenderer.sprite;
        startPosition = transform.position;
        shakeSound = AudioManager.Instance.CreateInstance(FmodEvents.Instance.boxShake);
        StartCoroutine(CheckMovement());
    }


    void Update()
    {
        if (isDragging)
        {
            Drag();
            DetectShake();
        }

        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
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
        }
        
        if (isDragging)
        {
            return;
        }
        
        Vector3 mouseWorldPositionForOver = GetMouseWorldPosition();
        RaycastHit2D[] hitsForOver = Physics2D.RaycastAll(mouseWorldPositionForOver, Vector2.zero);

        foreach (var hit in hitsForOver)
        {
            if (hit.collider != null && hit.collider == itemCollider)
            {
                OnMouseOver();
                break;
            }
            OnMouseExit();
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        spriteRenderer.sprite = upsideDownSprite;
        transform.DORotate(new Vector3(0f, 0f, 180f), 0.3f);
        offset = transform.position - GetMouseWorldPosition();
        lastMousePosition = GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        transform.DORotate(new Vector3(0f, 0f, 0f), 1f);
        transform.DOMove(startPosition, 1f).SetEase(Ease.OutQuad);
        spriteRenderer.sprite = baseSprite;
        isDragging = false;
    }

    void OnMouseOver()
    {
        spriteRenderer.sprite = highlightSprite;
    }

    void Drag()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 newPosition = mousePosition + offset;

        // Constrain the new position within the drag zone
        if (dragZone.OverlapPoint(new Vector2(newPosition.x, newPosition.y)))
        {
            transform.position = newPosition;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.nearClipPlane; // Ensure proper conversion to world space
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
    
    private IEnumerator CheckMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            DetectMovement();
        }
    }

    private void DetectMovement()
    {
        Vector3 currentPosition = transform.position;
        Debug.Log("Dectecting movement");
        if (currentPosition != lastPosition && isDragging)
        {
            playSound();
        }
        else
        {
            stopSound();
        }
        lastPosition = currentPosition;
    }

    void DetectShake()
    {
        Vector3 currentMousePosition = GetMouseWorldPosition();
        float shakeAmount = (currentMousePosition - lastMousePosition).magnitude;

        // if (shakeAmount > shakeThreshold * 0.2f)
        // {
        //     playSound();
        // }
        // else if (currentMousePosition == lastMousePosition)
        // {
        //     Debug.Log("Stop sound");
        //     stopSound();
        // }

        if (shakeAmount > shakeThreshold && shakeTimer <= 0)
        {
            SpawnObject();
            shakeTimer = shakeCooldown; // Reset cooldown
        }

        lastMousePosition = currentMousePosition;
    }

    private void playSound()
    {
        PLAYBACK_STATE playbackState;
        shakeSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            shakeSound.start();
        }
    }

    private void stopSound()
    {
        shakeSound.stop(STOP_MODE.ALLOWFADEOUT);
    }

    void SpawnObject()
    {
        // Calculate the spawn position at the bottom of the current object
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= itemCollider.bounds.size.y / 2 + spawnObject.GetComponent<Collider2D>().bounds.size.y / 2;

        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }
    
    void OnMouseExit()
    {
        spriteRenderer.sprite = baseSprite;
    }
    
}
