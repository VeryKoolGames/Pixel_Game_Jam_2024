using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SeekFood")]
public class SeekShopBehavior : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, bool hasEaten)
    {
        Transform nearestFood = null;
        float shortestDistance = Mathf.Infinity;

        // Find the nearest food item
        foreach (Transform item in context)
        {
            if (item.CompareTag("Shop"))
            {
                float distance = Vector2.Distance(item.position, agent.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestFood = item;
                }
            }
        }

        if (nearestFood != null)
        {
            Vector2 direction = (nearestFood.position - agent.transform.position).normalized;
            return direction;
        }

        return Vector2.zero;
    }
}