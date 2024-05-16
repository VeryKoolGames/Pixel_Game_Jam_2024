using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Counter", menuName = "ScriptableObjects/Counter")]
public class Counter : ScriptableObject
{
    public int counter;
}
