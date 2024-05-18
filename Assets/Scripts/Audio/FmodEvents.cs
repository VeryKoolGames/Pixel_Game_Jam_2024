using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents Instance;
    [SerializeField] public EventReference boxShake;
    [SerializeField] public EventReference waterDrop;
    [SerializeField] public EventReference bubblePop;
    [SerializeField] public EventReference waterAmbiance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
