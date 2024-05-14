using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "GameObjects", menuName = "ScriptableObjects/GameObjects")]
public class GameObjects : ScriptableObject
{
    public List<GameObject> gameObjects = new List<GameObject>();
}