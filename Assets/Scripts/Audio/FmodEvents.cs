using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FmodEvents : MonoBehaviour
{
    public static FmodEvents Instance;
    public EventReference boxShake;
    public EventReference waterDrop;
    public EventReference bubblePop;
    public EventReference waterAmbiance;
    public EventReference mainMusic;
    public EventReference shortShakeSound;
    public EventReference pillPotClose;
    public EventReference pillPotOpen;
    public EventReference effervecense;
    public EventReference aspiNoise;
    public EventReference chestSound;
    public EventReference hoverUISound;

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
