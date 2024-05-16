using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollowScript : MonoBehaviour
{
    
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mouse;
    }
}
