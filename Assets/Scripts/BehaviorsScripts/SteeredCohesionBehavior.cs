using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
   Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

   public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, bool hasEaten)
   {
        //Si y'a pas de voisins alors retourne zero, pour economiser des ressources
        if(context.Count == 0)
        {
            return Vector2.zero;
        }

        //Ajouter tous les points ensemble et faire la moyenne
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach(Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
   }

}
