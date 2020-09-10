using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    public GameObject instantiatePlace;

    [Range(10, 500)]
    public int soldiersCount = 5;
    const float AgentDensity = 0.02f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    bool wasStuck;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        Debug.Log(instantiatePlace.transform.localPosition);
        Quaternion rot = new Quaternion();
        for (int i = 0; i < soldiersCount; i++)
        {
            Vector2 randomSquare = new Vector2(Random.Range(0, 0.5f)+instantiatePlace.transform.position.x, Random.Range(0, 1f)+ instantiatePlace.transform.position.y);
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                //randomSquare * soldiersCount * AgentDensity,
                randomSquare,
                rot,
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR DEMO ONLY
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            Vector2 move = behavior.CalculateMove(agent, context, this);
            bool stuck = agent.GetComponent<Walk>().IsStuck();
            if (stuck)
            {
                move *= driveFactor;
                wasStuck = true;
            }else if (wasStuck)
            {
                wasStuck = false;
                move /= driveFactor;
            }
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
            Rigidbody2D rb = agent.GetComponentInChildren<Rigidbody2D>();
            Animator anim = agent.GetComponentInChildren<Animator>();
            if (rb.velocity.sqrMagnitude < 0.1)
            {
                anim.SetBool("move", false);
            }
            else
            {
                anim.SetBool("move", true);
            }
        }

        
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
