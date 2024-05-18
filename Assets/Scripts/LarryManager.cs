using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using KBCore.Refs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LarryManager : ValidatedMonoBehaviour
{
    [SerializeField, Self] private OnFilthCleanListener onFilthCleanListener;
    [SerializeField, Self] private OnFilthInvasionListener onFilthInvasionListener;
    [SerializeField] private Cooldown coolDownBeforeSpeaking;
    private float timeSinceLastSpeak = 0;
    
    // STATES
    private LarryStateMachine stateMachine;
    private GrumpyState grumpyState;
    private HappyState happyState;
    private EarlyGameState earlyGameState;
    private LarryIdleState idleState;
    
    private DialogueTypes currentDialogueType;
    [SerializeField] Dialogue[] originalDialogues;
    private Dialogue[] dialogues;
    [SerializeField] private OnFishSpawn onFishSpawn;
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image labelText;

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

        stateMachine.ChangeState(earlyGameState);
        onFishSpawn.Raise(gameObject);
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
                string randomSentence = dialogue.sentences[UnityEngine.Random.Range(0, dialogue.sentences.Count)];
                dialogue.sentences.Remove(randomSentence);
                return randomSentence;
            }
        }

        return null;
    }
}
