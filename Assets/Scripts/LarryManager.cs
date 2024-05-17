using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using KBCore.Refs;
using UnityEngine;

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
        dialogues = originalDialogues;
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
            Debug.Log("Larry: " + dialogue);
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
