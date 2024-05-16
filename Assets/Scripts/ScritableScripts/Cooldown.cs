using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Cooldown", menuName = "ScriptableObjects/Cooldown")]
public class Cooldown : ScriptableObject
{
    public float cooldownTime;
}
