using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishSO")]
public class FishSO : ScriptableObject
{
    public int rarity;
    public string name;
    public Sprite sprite;
    public string description;
    public GameObject prefab;
    public FishTypes fishType;
}