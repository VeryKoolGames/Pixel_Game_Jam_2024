using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using Unity.VisualScripting;
using UnityEngine;

public class PillBoxHandler : ValidatedMonoBehaviour
{
    [SerializeField, Self] private Collider2D selfCollider;
    [SerializeField] private GameObject pillPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite highlightSprite;
    [SerializeField] private LayerMask waterLayerMask;
    [SerializeField] private Transform couvercleObject;
    private Transform couvercleBasePosition;
    private bool isOpen;

    private void Start()
    {
        couvercleBasePosition = couvercleObject.transform;
    }

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
        RaycastHit2D hit2 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.down, Mathf.Infinity, waterLayerMask);
        if (hit2 != null)
        {
            if (hit2.collider != null && hit2.collider == selfCollider)
            {
                OnMouseOver();
                return;
            }
            OnMouseExit();
        }
    }

    private void OnMouseOver()
    {
        // move the coucercleObject left
        if (couvercleObject.transform == couvercleBasePosition.transform)
            couvercleObject.DOMoveX(.1f, 1f);
        spriteRenderer.sprite = highlightSprite;
    }
    
    private void OnMouseExit()
    {
        couvercleObject.DOMove(couvercleBasePosition.position, 1f);
        spriteRenderer.sprite = baseSprite;
    }

    private void OnMouseDown()
    {
        Debug.Log("Hit Pill Box");
        // spriteRenderer.sprite = baseSprite;
        Instantiate(pillPrefab, transform.position, Quaternion.identity);
    }
}
