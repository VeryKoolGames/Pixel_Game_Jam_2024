using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class FishReproductionManager : MonoBehaviour
{
    [SerializeField] private float maxFishHunger;
    [SerializeField] private float fishLifeSpan;
    [SerializeField] private float fishReproductionCooldown;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private SpriteRenderer fishSpriteRenderer;
    [SerializeField] private OnFishDeath onFishDeath;
    private Color fuckColor = Color.magenta;
    private Color baseColor;
    private float currentLifeSpan;
    private float currentHunger;
    private float currentReproductionRate;
    private bool canReproduce;
    public Fish fish;
    private bool hasEaten = true;
    
    private void Start()
    {
        InvokeRepeating("CheckNearbyFishForSex", 0f, 5f);
        baseColor = fishSpriteRenderer.color;
    }
    
    void Update()
    {
        FishFeedHandler();
        FishLifeHandler();
        FishSexHandler();
    }
    
    public bool getHasEaten()
    {
        return hasEaten;
    }
    
    public void SetFish(Fish fish)
    {
        this.fish = fish;
    }
    
    public bool CanReproduce()
    {
        // return currentHunger >= 50 && canReproduce;
        return canReproduce;
    }

    private void FishSexHandler()
    {
        currentReproductionRate += Time.deltaTime;
        if (currentReproductionRate >= fishReproductionCooldown)
        {
            fishSpriteRenderer.color = fuckColor;
            canReproduce = true;
        }
    }

    private void FishLifeHandler()
    {
        currentLifeSpan += Time.deltaTime;
        if (currentLifeSpan >= fishLifeSpan)
        {
            onFishDeath.Raise(gameObject.GetComponent<FlockAgent>());
        }
    }
    

    private void FishFeedHandler()
    {
        currentHunger -= Time.deltaTime;
        if (currentHunger <= 50)
        {
            hasEaten = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FoodEat"))
        {
            currentHunger = maxFishHunger;
            hasEaten = true;
            Destroy(other.transform.parent.gameObject);
        }
    }
    
    void CheckNearbyFishForSex()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Fish") && hit.gameObject != this.gameObject)
            {
                FishReproductionManager otherFish = hit.GetComponent<FishReproductionManager>();
                if (otherFish != null && canReproduce && otherFish.CanReproduce())
                {
                    FishCreator.Instance.CreateFish(fish, otherFish.fish);
                    currentReproductionRate = 0;
                    canReproduce = false;
                    otherFish.currentReproductionRate = 0;
                    otherFish.canReproduce = false;
                    fishSpriteRenderer.color = baseColor;
                    otherFish.fishSpriteRenderer.color = baseColor;
                    break;
                }
            }
        }
    }
}
