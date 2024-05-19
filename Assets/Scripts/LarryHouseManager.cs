using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;

public class LarryHouseManager : ValidatedMonoBehaviour
{
    [SerializeField] private OnLarryHouseSpawn _onLarryHouseSpawn;
    [SerializeField] private Transform larryDoor;
    [SerializeField] private GameObject larryHead;
    [SerializeField, Self] private OnGameEventListener onLarryArrival;
    // Start is called before the first frame update
    void Start()
    {
        _onLarryHouseSpawn.Raise(larryDoor);
        onLarryArrival.Response.AddListener(ActivateLarryHead);
    }

    private void ActivateLarryHead()
    {
        larryHead.SetActive(true);
    }
}
