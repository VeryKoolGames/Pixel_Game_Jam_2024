using System;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

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
    private Sprite baseSprite;

    private void Start()
    {
        baseSprite = spriteRenderer.sprite;
        startPosition = transform.position;
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

        // Handle mouse down manually with raycasting
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

        // Handle mouse up manually
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnMouseUp();
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

    void DetectShake()
    {
        Vector3 currentMousePosition = GetMouseWorldPosition();
        float shakeAmount = (currentMousePosition - lastMousePosition).magnitude;

        if (shakeAmount > shakeThreshold && shakeTimer <= 0)
        {
            SpawnObject();
            shakeTimer = shakeCooldown; // Reset cooldown
        }

        lastMousePosition = currentMousePosition;
    }

    void SpawnObject()
    {
        // Calculate the spawn position at the bottom of the current object
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= itemCollider.bounds.size.y / 2 + spawnObject.GetComponent<Collider2D>().bounds.size.y / 2;

        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }
}
