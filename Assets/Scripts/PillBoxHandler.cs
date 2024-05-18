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
    private Vector2 couvercleBasePosition;
    private bool isOpen;

    private void Start()
    {
        couvercleBasePosition = couvercleObject.transform.position;
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is directly over this object's collider
            if (selfCollider.OverlapPoint(mousePosition))
            {
                OnMouseDown();
            }
        }
        if (selfCollider.OverlapPoint(mousePosition))
        {
            OnMouseOver();
        }
        else
        {
            OnMouseExit();
        }
    }

    private void OnMouseOver()
    {
        // move the coucercleObject left
        if ((Vector2)couvercleObject.transform.position == couvercleBasePosition)
        {
            Vector2 newPosition = couvercleBasePosition;
            newPosition.x += 0.2f;
            couvercleObject.DOMove(newPosition, 1f);
        }
        spriteRenderer.sprite = highlightSprite;
    }

    private void OnMouseExit()
    {
        couvercleObject.DOMove(couvercleBasePosition, .2f);
        spriteRenderer.sprite = baseSprite;
    }

    private void OnMouseDown()
    {
        Debug.Log("Hit Pill Box");
        // spriteRenderer.sprite = baseSprite;
        Instantiate(pillPrefab, transform.position, Quaternion.identity);
    }
}
