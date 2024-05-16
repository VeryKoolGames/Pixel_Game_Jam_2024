using System;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

public class DragNDropVacuum : ValidatedMonoBehaviour
{
    [SerializeField] private Collider2D dragZone; // Collider2D to define the drag area

    private bool isDragging = false;
    private Vector3 offset;
    [SerializeField, Self] private Collider2D itemCollider;
    [SerializeField, Self] private SpriteRenderer spriteRenderer;


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

        // Handle mouse up manually
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnMouseUp();
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
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
    
}
