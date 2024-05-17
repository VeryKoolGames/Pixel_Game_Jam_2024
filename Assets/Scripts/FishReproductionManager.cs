using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using UnityEngine;

public class FishReproductionManager : ValidatedMonoBehaviour
{
    [SerializeField] private float maxFishHunger;
    [SerializeField] private float fishLifeSpan;
    [SerializeField] private float fishReproductionCooldown;
    [SerializeField] private float detectionRadius = 5f;
    public OnFishDeath onFishDeath;
    public GameObject loveParticle;
    private float currentLifeSpan;
    private float currentHunger;
    public float currentReproductionRate;
    private bool hasEaten = true;
    private float checkSexTimer;
    private float checkSexInterval = 5f;
    [SerializeField, Self] public FlockAgent _flockAgent;
    [SerializeField, Self] private OnFilthCleanListener onFilthCleanListener;
    [SerializeField, Self] private OnFilthInvasionListener onFilthInvasionListener;
    public Fish fish;
    
    // STATES
    public StateMachine stateMachine;
    private IdleState idleState;
    private AquariumDirtyState aquariumDirtyState;
    private ReadyToReproduceState readyToReproduceState;
    public ReproducingState reproducingState;
    private DeadState deadState;

    private void OnEnable()
    {
        onFilthCleanListener.Response.AddListener(OnAquariumClean);
        onFilthInvasionListener.Response.AddListener(OnAquariumDirty);
    }

    private void OnDisable()
    {
        onFilthCleanListener.Response.RemoveListener(OnAquariumClean);
        onFilthInvasionListener.Response.RemoveListener(OnAquariumDirty);
    }

    private void Start()
    {
        currentHunger = maxFishHunger;
        stateMachine = new StateMachine();

        idleState = new IdleState();
        idleState.Initialize(this);

        aquariumDirtyState = new AquariumDirtyState();
        aquariumDirtyState.Initialize(this);

        readyToReproduceState = new ReadyToReproduceState();
        readyToReproduceState.Initialize(this);

        reproducingState = new ReproducingState();
        reproducingState.Initialize(this);

        deadState = new DeadState();
        deadState.Initialize(this);

        stateMachine.ChangeState(idleState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnAquariumClean()
    {
        stateMachine.ChangeState(idleState);
    }
    
    private void OnAquariumDirty()
    {
        stateMachine.ChangeState(aquariumDirtyState);
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
            // stateMachine.ChangeState(idleState);
        }
    }

    public void FishLifeHandler()
    {
        currentLifeSpan += Time.deltaTime;
        if (currentLifeSpan >= fishLifeSpan)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    public void FishSexHandler()
    {
        currentReproductionRate += Time.deltaTime;
        if (currentReproductionRate >= fishReproductionCooldown)
        {
            stateMachine.ChangeState(readyToReproduceState);
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
    
    public void UpdateCheckSexTimer()
    {
        checkSexTimer += Time.deltaTime;
        if (checkSexTimer >= checkSexInterval)
        {
            CheckNearbyFishForSex();
            checkSexTimer = 0f;
        }
    }

    public void OnBubblePop()
    {
        currentHunger = maxFishHunger;
        hasEaten = true;
        stateMachine.ChangeState(readyToReproduceState);
    }

    private void CheckNearbyFishForSex()
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
                    Vector2 middlePoint = (transform.position + otherFish.transform.position) / 2;
                    reproducingState.SetVariables(otherFish, middlePoint);
                    stateMachine.ChangeState(reproducingState);
                    otherFish.reproducingState.SetVariables(this, middlePoint);
                    otherFish.stateMachine.ChangeState(reproducingState);
                    break;
                }
            }
        }
    }

    public IEnumerator WaitForSex(FishReproductionManager otherFish)
    {
        yield return new WaitForSeconds(3);
        FishCreator.Instance.CreateFish(fish, otherFish.fish);
        stateMachine.ChangeState(idleState);
        otherFish.stateMachine.ChangeState(idleState);
    }
}
