using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaterPillHandler : MonoBehaviour
{
    private Vector2 startPosition;
    private bool isDragging;
    private bool hasHitWater;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private GameObject particleEffect;

    [SerializeField] private OnFilthRemoved onWaterClean;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDragging) return;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouse;
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouse, Vector2.down, Mathf.Infinity, waterLayer);
            if (hit.collider != null && hit.collider.CompareTag("Water"))
            {
                hasHitWater = true;
            }
            else
            {
                transform.DOMove(startPosition, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => Destroy(gameObject));
            }
            isDragging = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHitWater && other.CompareTag("Water"))
        {
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.waterDrop, transform.position);
            Invoke("activateParticleEffect", 0.2f);
            transform.DOScale(Vector2.zero, 1.5f).SetEase(Ease.OutQuad).OnComplete(OnObjectDestroyed);
            float gravity = GetComponent<Rigidbody2D>().gravityScale;
            GetComponent<Rigidbody2D>().gravityScale = gravity * 0.05f;
            onWaterClean.Raise();
            AudioManager.Instance.PlayOneShot(FmodEvents.Instance.effervecense, transform.position);
        }
    }

    private void OnObjectDestroyed()
    {
        particleEffect.GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject, 2f);
    }
    
    private void activateParticleEffect()
    {
        particleEffect.SetActive(true);
    }
}
