using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryBehavior : MonoBehaviour
{
    [SerializeField] private OnFishSpawn onFishSpawn;
    // Start is called before the first frame update
    void Start()
    {
        onFishSpawn.Raise(gameObject);
    }
}
