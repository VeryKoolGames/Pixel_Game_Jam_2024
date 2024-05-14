using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishReproductionManager : MonoBehaviour
{
    [SerializeField] private float fishHunger = 40;
    private bool canHaveSex = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        FeedFish();
    }
    
    private void FeedFish()
    {
        fishHunger -= Time.deltaTime;
        if (fishHunger <= 0)
        {
            Destroy(gameObject);
        }
        else if (fishHunger <= 50)
        {
            canHaveSex = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            fishHunger += 10;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Fish"))
        {
            if (fishHunger > 50)
            {
                canHaveSex = true;
                if (other.GetComponent<FishReproductionManager>().canHaveSex)
                {
                    Debug.Log("Fish can have sex");
                }
            }
        }
    }
}
