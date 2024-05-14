using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, bool hasEaten)
   {
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in" + name, this);
            return Vector2.zero;
        }
    
        Vector2 move = Vector2.zero;
        Debug.Log(hasEaten);

        for (int i = 0; i < behaviors.Length; i++)
        {
            float weight = weights[i];
            if (behaviors[i] is SeekFoodBehavior && hasEaten == false)
            {
                weight *= 2;
            }
            else if (behaviors[i] is SeekFoodBehavior && hasEaten)
            {
                weight = 0;
            }
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock, hasEaten) * weight;

            if(partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }
        
        return move;
        
   }
}
