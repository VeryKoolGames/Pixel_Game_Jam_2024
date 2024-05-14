using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(OnFishDeathListener))]
public class Flock : ValidatedMonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10,500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(0.1f,100f)]
    public float DriveFactor = 10f;
    [Range(0.1f,100f)]
    public float maxSpeed = 5f;

    [Range(0.1f,10f)]
    public float neighborRadius = 1.5f;
    [Range(0f,1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius {get {return squareAvoidanceRadius;} }
    [SerializeField, Self] private OnFishDeathListener onFishDeathListener;

    private void Awake()
    {
        onFishDeathListener.Response.AddListener(RemoveAgent);
    }

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }


    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            SpawnAgent();
        }

        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behavior.CalculateMove(agent, context, this, agent.GetComponent<FishReproductionManager>().getHasEaten());
            move *= DriveFactor;
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    void SpawnAgent()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int i = 0;

        FlockAgent newAgent = Instantiate(
                agentPrefab,
                mousePos,
                Quaternion.Euler(Vector3.forward * Random.Range(0f,360f)),
                transform
            );
            i = i + 1;
            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if( c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    
    public void RemoveAgent(FlockAgent agent)
    {
        agents.Remove(agent);
        Destroy(agent.gameObject);
    }
}
