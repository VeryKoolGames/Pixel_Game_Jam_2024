using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Serialization;

public class FishReproductionManager : ValidatedMonoBehaviour
{
    [SerializeField] private float maxFishHunger;
    [SerializeField] private float fishLifeSpan;
    [SerializeField] private float fishReproductionCooldown;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private SpriteRenderer fishSpriteRenderer;
    [SerializeField] private OnFishDeath onFishDeath;
    [SerializeField] private GameObject loveParticle;
    private Color fuckColor = Color.magenta;
    private Color baseColor;
    private float currentLifeSpan;
    private float currentHunger;
    private float currentReproductionRate;
    private bool canReproduce;
    private bool isReproducing;
    public Fish fish;
    private bool hasEaten = true;
    [SerializeField, Self] private FlockAgent _flockAgent;

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
            Debug.Log("Fish can reproduce");
            loveParticle.SetActive(true);
            canReproduce = true;
        }
    }

    private void FishLifeHandler()
    {
        if (isReproducing)
            return;
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
                if (otherFish != null && !isReproducing && !otherFish.isReproducing && canReproduce && otherFish.CanReproduce())
                {
                    isReproducing = true;
                    otherFish.isReproducing = true;
                    Vector2 middlePoint = (transform.position + otherFish.transform.position) / 2;
                    SetFishesForSex(middlePoint, otherFish);
                    StartCoroutine(WaitForSex(otherFish));
                    break;
                }
            }
        }
    }

    private void SetFishesForSex(Vector2 position, FishReproductionManager otherFish)
    {
        _flockAgent.sexSpot = position;
        otherFish._flockAgent.sexSpot = position;
        _flockAgent.isHavingSex = true;
        otherFish._flockAgent.isHavingSex = true;
    }
    
    private void ResetFishAfterSex(FishReproductionManager otherFish)
    {
        _flockAgent.isHavingSex = false;
        isReproducing = false;
        otherFish._flockAgent.isHavingSex = false;
        otherFish.isReproducing = false;
    }
    
    private IEnumerator WaitForSex(FishReproductionManager otherFish)
    {
        yield return new WaitForSeconds(3);
        FishCreator.Instance.CreateFish(fish, otherFish.fish);
        currentReproductionRate = 0;
        canReproduce = false;
        otherFish.currentReproductionRate = 0;
        otherFish.canReproduce = false;
        loveParticle.SetActive(false);
        otherFish.loveParticle.SetActive(false);        
        ResetFishAfterSex(otherFish);
    }
}
