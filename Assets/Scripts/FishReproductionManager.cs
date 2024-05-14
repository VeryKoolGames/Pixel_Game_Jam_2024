using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishReproductionManager : MonoBehaviour
{
    [SerializeField] private float fishHunger = 100;
    private bool hasEaten = true;
    
    public bool getHasEaten()
    {
        return hasEaten;
    }
    void Update()
    {
        FeedFish();
    }
    
    private void FeedFish()
    {
        fishHunger -= Time.deltaTime;
        if (fishHunger <= 50)
        {
            hasEaten = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger " + other.tag);
        if (other.CompareTag("FoodEat"))
        {
            fishHunger += 10;
            hasEaten = true;
            Destroy(other.transform.parent.gameObject);
        }
        // else if (other.CompareTag("Fish"))
        // {
        //     if (fishHunger > 50)
        //     {
        //         canHaveSex = true;
        //         if (other.GetComponent<FishReproductionManager>().canHaveSex)
        //         {
        //             Debug.Log("Fish can have sex");
        //         }
        //     }
        // }
    }
}
