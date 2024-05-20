using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LarryManager : ValidatedMonoBehaviour
{
    [SerializeField] private OnFilthCleanListener onFilthCleanListener;
    [SerializeField] private OnFilthInvasionListener onFilthInvasionListener;
    [SerializeField] private OnFilthCleanListener onWaterColorClean; 
    [SerializeField] private Cooldown coolDownBeforeSpeaking;
    [SerializeField, Self] private OnLarryHouseSpawnListener onLarryHouseSpawn;
    [SerializeField] private OnFilthInvasionListener onWaterDirty;
    [SerializeField] private OnGameEvent onHouseArrival;
    private float timeSinceLastSpeak = 0;
    
    // STATES
    private LarryStateMachine stateMachine;
    private GrumpyState grumpyState;
    private HappyState happyState;
    private EarlyGameState earlyGameState;
    private LarryIdleState idleState;
    private InHouseState inHouseState;
    
    private DialogueTypes currentDialogueType;
    [SerializeField] Dialogue[] originalDialogues;
    private Dialogue[] dialogues;
    [SerializeField] private OnFishSpawn onFishSpawn;
    private bool canChangeState;
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image labelText;
    [SerializeField] private Flock flock;

    private void OnEnable()
    {
        onFilthCleanListener.Response.AddListener(OnAquariumClean);
        onFilthInvasionListener.Response.AddListener(OnAquariumDirty);
        onLarryHouseSpawn.Response.AddListener(OnLarryHouseSpawned);
        onWaterDirty.Response.AddListener(OnAquariumDirty);
        onWaterColorClean.Response.AddListener(OnAquariumClean);
    }

    private void OnDisable()
    {
        onFilthCleanListener.Response.RemoveListener(OnAquariumClean);
        onFilthInvasionListener.Response.RemoveListener(OnAquariumDirty);
        onLarryHouseSpawn.Response.RemoveListener(OnLarryHouseSpawned);
        onWaterDirty.Response.RemoveListener(OnAquariumDirty);
        onWaterColorClean.Response.RemoveListener(OnAquariumClean);
    }

    private void Start()
    {
        dialogues = new Dialogue[originalDialogues.Length];
        for (int i = 0; i < originalDialogues.Length; i++)
        {
            dialogues[i] = originalDialogues[i].Clone();
        }
        stateMachine = new LarryStateMachine();

        grumpyState = new GrumpyState();
        grumpyState.Initialize(this);

        happyState = new HappyState();
        happyState.Initialize(this);
        
        idleState = new LarryIdleState();
        idleState.Initialize(this);
        
        earlyGameState = new EarlyGameState();
        earlyGameState.Initialize(this);
        
        inHouseState = new InHouseState();
        inHouseState.Initialize(this);

        stateMachine.ChangeState(earlyGameState);
        StartCoroutine(WaitTwentySeconds());
        onFishSpawn.Raise(gameObject);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnAquariumClean()
    {
        if (canChangeState)
            stateMachine.ChangeState(idleState);
    }
    
    private IEnumerator WaitTwentySeconds()
    {
        yield return new WaitForSeconds(20);
        canChangeState = true;
    }
    
    private void OnAquariumDirty()
    {
        if (stateMachine.CurrentState != inHouseState && canChangeState)
            stateMachine.ChangeState(grumpyState);
    }
    
    public void SetDialogueType(DialogueTypes dialogueType)
    {
        currentDialogueType = dialogueType;
    }

    public void Speak()
    {
        timeSinceLastSpeak += Time.deltaTime;
        if (timeSinceLastSpeak < coolDownBeforeSpeaking.cooldownTime)
        {
            return;
        }
        if (Random.Range(0f, 1f) > 0.8f && stateMachine.CurrentState != earlyGameState)
        {
            return;
        }
        string dialogue = GetDialogue();
        if (dialogue != null)
        {
            dialogueUI.SetActive(true);
            Color color = labelText.color;
            color.a = 0;
            labelText.color = color;
            
            color = dialogueText.color;
            color.a = 0;
            dialogueText.color = color;
            dialogueText.text = dialogue;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(labelText.DOFade(1f, 1f));
            sequence.Append(dialogueText.DOFade(1f, 1f));
            sequence.AppendInterval(2f);
            sequence.Append(labelText.DOFade(0f, 1f));
            sequence.Join(dialogueText.DOFade(0f, 1f));
            sequence.OnComplete(() =>
            {
                dialogueUI.SetActive(false);
            });
            sequence.Play();
        }
        timeSinceLastSpeak = 0;
    }

    private string GetDialogue()
    {
        foreach (var dialogue in dialogues)
        {
            if (dialogue.dialogueType == currentDialogueType && dialogue.sentences.Count > 0)
            {
                string randomSentence;
                if (stateMachine.CurrentState != grumpyState)
                {
                    randomSentence = dialogue.sentences[0];
                    dialogue.sentences.Remove(randomSentence);
                }
                else
                {
                    randomSentence = dialogue.sentences[UnityEngine.Random.Range(0, dialogue.sentences.Count)];
                }
                return randomSentence;
            }
        }

        return null;
    }
    
    private void OnLarryHouseSpawned(Transform housePosition)
    {
        flock.RemoveAgentWithoutDestroy(GetComponent<FlockAgent>());
        Vector3 direction = (housePosition.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.DORotate(new Vector3(0, 0, angle), 2f)
            .SetEase(Ease.OutQuart);

        transform.DOMove(housePosition.position, 2f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                transform.DOScale(0f, 1f).OnComplete(() =>
                {
                    stateMachine.ChangeState(inHouseState);
                    onHouseArrival.Raise();
                    var transform1 = transform;
                    Vector2 newPos = transform1.position;
                    newPos.y += 0.2f;
                    transform1.position = newPos;
                });
            });
    }
}
