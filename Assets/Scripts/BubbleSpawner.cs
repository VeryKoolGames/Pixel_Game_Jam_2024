using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;

public class BubbleSpawner : ValidatedMonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField, Self] private Animator chestAnimator;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        InvokeRepeating("SpawnBubble", 1, 5f);
    }

    private void SpawnBubble()
    {
        StartCoroutine(animateChest());
    }

    private IEnumerator animateChest()
    {
        chestAnimator.Play("chestOpen");
        AudioManager.Instance.PlayOneShot(FmodEvents.Instance.chestSound, transform.position);
        for (int i = 0; i < 5; i++)
        {
            GameObject bubblePrefab = objectPool.GetObject();
            bubblePrefab.transform.position = spawnPoint.position;
            yield return new WaitForSeconds(.4f);
        }
        chestAnimator.Play("chestClose");
    }
}
