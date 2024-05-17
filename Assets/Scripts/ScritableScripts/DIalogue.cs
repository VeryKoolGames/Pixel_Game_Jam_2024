using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueTypes dialogueType;
    public List<string> sentences;
}
