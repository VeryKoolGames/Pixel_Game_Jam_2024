using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

public class PillBoxHandler : ValidatedMonoBehaviour
{
    [SerializeField, Self] private Collider2D selfCollider;
    [SerializeField] private GameObject pillPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite highlightSprite;
    [SerializeField] private LayerMask waterLayerMask;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.down, Mathf.Infinity, waterLayerMask);
            if (hit != null)
            {
                if (hit.collider != null && hit.collider == selfCollider)
                {
                    OnMouseDown();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Hit Pill Box");
        // spriteRenderer.sprite = baseSprite;
        Instantiate(pillPrefab, transform.position, Quaternion.identity);
    }
}
