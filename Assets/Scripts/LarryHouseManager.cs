using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryHouseManager : MonoBehaviour
{
    [SerializeField] private OnLarryHouseSpawn _onLarryHouseSpawn;
    // Start is called before the first frame update
    void Start()
    {
        _onLarryHouseSpawn.Raise(transform);
    }
}
