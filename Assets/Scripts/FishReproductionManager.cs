using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using UnityEngine;

public class FishReproductionManager : MonoBehaviour
{
    [SerializeField] private float maxFishHunger;
    [SerializeField] private float fishLifeSpan;
    [SerializeField] private float fishReproductionCooldown;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private SpriteRenderer fishSpriteRenderer;
    public OnFishDeath onFishDeath;
    public GameObject loveParticle;
    private float currentLifeSpan;
    private float currentHunger;
    private float currentReproductionRate;
    private bool hasEaten = true;
    [SerializeField, Self] private FlockAgent _flockAgent;
    public Fish fish;

    [SerializeField] private StateMachine stateMachine;

    private void Start()
    {
        currentHunger = maxFishHunger;
        stateMachine = new StateMachine();
        stateMachine.ChangeState(new IdleState(this));
        InvokeRepeating("CheckNearbyFishForSex", 0f, 5f);
    }

    private void Update()
    {
        stateMachine.Update();
        Debug.Log("Current State: " + stateMachine.CurrentState);
    }
    
    public bool getHasEaten()
    {
        return hasEaten;
    }

    public void SetFish(Fish fish)
    {
        this.fish = fish;
    }

    public void FishFeedHandler()
    {
        currentHunger -= Time.deltaTime;
        if (currentHunger <= 50)
        {
            hasEaten = false;
            stateMachine.ChangeState(new IdleState(this));
        }
    }

    public void FishLifeHandler()
    {
        currentLifeSpan += Time.deltaTime;
        if (currentLifeSpan >= fishLifeSpan)
        {
            stateMachine.ChangeState(new DeadState(this));
        }
    }

    public void FishSexHandler()
    {
        currentReproductionRate += Time.deltaTime;
        if (currentReproductionRate >= fishReproductionCooldown)
        {
            Debug.Log("Fish is ready to reproduce!");
            stateMachine.ChangeState(new ReadyToReproduceState(this));
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

    public void CheckNearbyFishForSex()
    {
        if (!(stateMachine.CurrentState is ReadyToReproduceState)) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Fish") && hit.gameObject != this.gameObject)
            {
                FishReproductionManager otherFish = hit.GetComponent<FishReproductionManager>();
                if (otherFish != null && otherFish.stateMachine.CurrentState is ReadyToReproduceState)
                {
                    stateMachine.ChangeState(new ReproducingState(this, otherFish));
                    otherFish.stateMachine.ChangeState(new ReproducingState(otherFish, this));
                    Vector2 middlePoint = (transform.position + otherFish.transform.position) / 2;
                    SetFishesForSex(middlePoint, otherFish);
                    break;
                }
            }
        }
    }

    public void SetFishesForSex(Vector2 position, FishReproductionManager otherFish)
    {
        _flockAgent.sexSpot = position;
        otherFish._flockAgent.sexSpot = position;
        _flockAgent.isHavingSex = true;
        otherFish._flockAgent.isHavingSex = true;
    }

    public void ResetFishAfterSex(FishReproductionManager otherFish)
    {
        _flockAgent.isHavingSex = false;
        otherFish._flockAgent.isHavingSex = false;
        stateMachine.ChangeState(new IdleState(this));
        otherFish.stateMachine.ChangeState(new IdleState(otherFish));
    }

    public IEnumerator WaitForSex(FishReproductionManager otherFish)
    {
        yield return new WaitForSeconds(3);
        FishCreator.Instance.CreateFish(fish, otherFish.fish);
        currentReproductionRate = 0;
        otherFish.currentReproductionRate = 0;
        ResetFishAfterSex(otherFish);
    }
}
