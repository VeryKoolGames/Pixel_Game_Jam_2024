using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpVacuum : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> kelpRBs = new List<Rigidbody2D>();
    public float vacuumingForce;


    void Update()
    {
        foreach(var kelpRB in kelpRBs)
        {
            Vector2 toVacuumDir = (Vector2)transform.position - (Vector2)kelpRB.transform.position;
            toVacuumDir.Normalize();
            kelpRB.velocity += toVacuumDir * vacuumingForce * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Kelp"))
        {
            kelpRBs.Add(other.GetComponent<Rigidbody2D>());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Kelp"))
        {  
            kelpRBs.Remove(other.GetComponent<Rigidbody2D>());
        }
    }
}
