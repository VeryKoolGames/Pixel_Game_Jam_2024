using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Vector2 movementDirection;
    public new Rigidbody2D rigidbody2D;
    [SerializeField]
    private float forceAmount;
    private float timeElapsed;

    void Update() { 
        
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 movementDirection = mouse - (Vector2)transform.position;

        rigidbody2D.velocity = movementDirection * forceAmount;
        
    }
}