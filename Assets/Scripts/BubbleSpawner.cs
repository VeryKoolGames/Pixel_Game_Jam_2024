using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Vector2 startScale;
    void Start()
    {
        InvokeRepeating("SpawnBubble", 0, 2f);
    }

    private void SpawnBubble()
    {
        GameObject bubblePrefab = objectPool.GetObject();
        bubblePrefab.transform.localScale = Vector2.zero;
        bubblePrefab.transform.position = transform.position;
        bubblePrefab.transform.DOScale(startScale, 1f);
    }
}
