using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using KBCore.Refs;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishReproductionManager : ValidatedMonoBehaviour
{
    [SerializeField] private Counter maxFishHunger;
    [SerializeField] private Counter foodBeforeStarving;
    [SerializeField] private float fishLifeSpan;
    [SerializeField] private float detectionRadius = 5f;
    public OnFishDeath onFishDeath;
    public GameObject loveParticle;
    private float currentLifeSpan;
    private float currentHunger;
    public float currentReproductionRate;
    private bool hasEaten = false;
    private float checkSexTimer;
    private float checkFishionTimer;
    [SerializeField] private Cooldown fishReproductionCooldown;
    [SerializeField] private Cooldown checkSexInterval;
    [SerializeField] private Cooldown checkFishionInterval;
    [SerializeField, Self] public FlockAgent _flockAgent;
    [SerializeField, Self] private OnFilthCleanListener onFilthCleanListener;
    [SerializeField, Self] private OnFilthInvasionListener onFilthInvasionListener;
    [SerializeField, Self] private FishCustomizeManager fishCustomizeManager;
    private Flock chosenFlock;
    public Fish fish;
    private int sexCount = 0;
    
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

    private void OnDestroy()
    {
        onFilthCleanListener.Response.RemoveListener(OnAquariumClean);
        onFilthInvasionListener.Response.RemoveListener(OnAquariumDirty);
    }

    private void Start()
    {
        currentHunger = 0;
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
    
    public void SetChosenFlock(Flock flock)
    {
        chosenFlock = flock;
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
        if (currentHunger > 0)
            currentHunger -= Time.deltaTime;
        if (currentHunger <= foodBeforeStarving.counter)
        {
            hasEaten = false;
        }
    }

    public void FishLifeHandler()
    {
        if (sexCount < 2)
            return;
        currentLifeSpan += Time.deltaTime;
        if (currentLifeSpan >= fishLifeSpan)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    public void FishSexHandler()
    {
        currentReproductionRate += Time.deltaTime;
        if (hasEaten && currentReproductionRate >= fishReproductionCooldown.cooldownTime && stateMachine.CurrentState != reproducingState)
        {
            stateMachine.ChangeState(readyToReproduceState);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FoodEat"))
        {
            currentHunger = maxFishHunger.counter;
            hasEaten = true;
            Destroy(other.transform.parent.gameObject);
        }
    }
    
    public void UpdateCheckSexTimer()
    {
        checkSexTimer += Time.deltaTime;
        if (checkSexTimer >= checkSexInterval.cooldownTime)
        {
            CheckNearbyFishForSex();
            checkSexTimer = 0f;
        }
    }

    public void OnBubblePop()
    {
        currentHunger = maxFishHunger.counter;
        hasEaten = true;
        stateMachine.ChangeState(readyToReproduceState);
    }

    private void CheckNearbyFishForSex()
    {
        if (stateMachine.CurrentState != readyToReproduceState) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Fish") && hit.gameObject != this.gameObject)
            {
                FishReproductionManager otherFish = hit.GetComponent<FishReproductionManager>();
                if (otherFish != null && otherFish.stateMachine.CurrentState == otherFish.readyToReproduceState)
                {
                    Vector2 middlePoint = (transform.position + otherFish.transform.position) / 2;
                    reproducingState.SetVariables(otherFish, middlePoint);
                    stateMachine.ChangeState(reproducingState);
                    otherFish.reproducingState.SetVariables(this, middlePoint);
                    otherFish.stateMachine.ChangeState(reproducingState);
                    sexCount++;
                    otherFish.sexCount++;
                    break;
                }
            }
        }
    }
    
    public void CheckForFishion()
    {
        checkFishionTimer += Time.deltaTime;
        if (checkFishionTimer >= checkFishionInterval.cooldownTime)
        {
            checkFishionTimer = 0f;
            if (Random.Range(0, 100) < 50)
                GetFishion();
        }
    }
    
    private void GetFishion()
    {
        if (!gameObject)
            return;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Fishion") && hit.gameObject != this.gameObject)
            {
                chosenFlock.RemoveAgentWithoutDestroy(_flockAgent);
                var sequence = DOTween.Sequence();
                sequence.Append(transform.DOMove(hit.gameObject.transform.position, 2f));
                sequence.Append(transform.DOScale(0, 2f));
                sequence.Append(transform.DOScale(1, 2f));
                sequence.OnComplete(() =>
                {
                    fishCustomizeManager.ModifyFishAccessory();
                    chosenFlock.AddAgentFromFish(_flockAgent);
                });
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

    public void OnGameStart()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(Random.Range(0, 1f));

        float delay = 0f;
        sequence.Append(transform.DOMoveX(-0.5f, 4f).SetEase(Ease.InOutQuad));

        for (int i = 0; i < 8; i++)
        {
            sequence.Insert(delay,transform.DOBlendableMoveBy(new Vector3(0, 0.1f, 0), 0.25f).SetEase(Ease.InOutQuad));
            delay += 0.25f;
            sequence.Insert(delay,transform.DOBlendableMoveBy(new Vector3(0, -0.1f, 0), 0.25f).SetEase(Ease.InOutQuad));
            delay += 0.25f;
        }

        sequence.Append(transform.DOMoveY(.1f, 2f));

        sequence.OnComplete(() =>
        {
            FishCreator.Instance.RaiseFishEvent(chosenFlock, gameObject);
        });
    }
}
