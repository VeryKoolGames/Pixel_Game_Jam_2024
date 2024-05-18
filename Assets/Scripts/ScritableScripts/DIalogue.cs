using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueTypes dialogueType;
    public List<string> sentences;

    public Dialogue Clone()
    {
        Dialogue clone = ScriptableObject.CreateInstance<Dialogue>();

        clone.dialogueType = this.dialogueType;
        clone.sentences = new List<string>(this.sentences);
        return clone;
    }
}
