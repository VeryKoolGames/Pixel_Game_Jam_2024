using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/EatingBehavior")]
public class EatingBehavior : FlockBehavior
{
    private FlockAgent Agent;

    private Vector2 dir;



    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
   {
        Agent = agent;
        //Ajouter tous les points ensemble et faire la moyenne

        dir -= (Vector2)Agent.transform.position;
        return dir;
   }

   void OnTriggerEnter2D(Collider2D collider)
   {
        if(collider.CompareTag("Food"))
        {
            dir = collider.transform.position - Agent.transform.position;

        }

        
   }
}
