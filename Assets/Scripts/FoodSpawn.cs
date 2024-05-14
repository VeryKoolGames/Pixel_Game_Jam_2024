using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject Food;
    public List<Transform> foodTransforms = new List<Transform>();
    
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            
            SpawnFood();
        }

    }

    void SpawnFood()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int i = 0;

        GameObject newFood = Instantiate(
                Food,
                mousePos,
                Quaternion.identity
            );
        foodTransforms.Add(newFood.transform);
        i = i+1;
        newFood.name = "Food" + i;
    }
}
