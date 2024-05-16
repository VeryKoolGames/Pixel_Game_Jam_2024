using System;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

public class ShakeManager : ValidatedMonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float shakeThreshold = 1.0f; // Adjust as needed
    [SerializeField] private float shakeCooldown = 0.5f; // Time between shakes

    private Vector3 offset;
    private Vector3 lastMousePosition;
    private float shakeTimer = 0f;
    [SerializeField, Self] private Collider2D itemCollider;


    void Update()
    {
        DetectShake();
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
        }
    }

    void OnMouseDown()
    {
        lastMousePosition = GetMouseWorldPosition();
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
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= itemCollider.bounds.size.y / 2 + spawnObject.GetComponent<Collider2D>().bounds.size.y / 2;

        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }
}
