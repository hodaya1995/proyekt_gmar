using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public GameObject instantiatePlace;

    List<FlockAgent> agents = new List<FlockAgent>();
    [Range(0, 500)]
    public int soldiersCount = 3;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    Vector2 randomSquare;
    int currI;
    List<FlockAgent> copyAgents = new List<FlockAgent>();
    float transRad = 0.2f;
    GameObject prev;
    bool firstCall = true;
    Vector3 prevPos = Vector3.zero;


    public List<FlockAgent> GetAgents()
    {
        return this.agents;
    }

    void Start()
    {
        randomSquare = new Vector2();
        InstantiateSoldier();


    }
    public static void SetAsObstacle(bool asObstacle, string layerName)
    {

        AstarPath astarPath = GameObject.Find("A*").GetComponent<AstarPath>();

        if (astarPath == null)
        {
            Debug.LogError("No Astar Path component found on this GameObject.");

            return;
        }



        foreach (GridGraph graph in astarPath.graphs.Cast<GridGraph>())
        {
            graph.SetGridShape(InspectorGridMode.IsometricGrid, layerName, asObstacle);

        }


    }
    FlockAgent InstantiateSoldier()
    {
        float x = instantiatePlace.transform.position.x;
        float y = instantiatePlace.transform.position.y + 1.5f;
     
        if (currI % 2 == 0)
        {
            randomSquare = new Vector2(-transRad +x  , transRad * (soldiersCount - currI) + y) ;
        }
        else
        {
            randomSquare = new Vector2(transRad + x , transRad * (soldiersCount - currI + 1) + y) ;

        }

        currI++;

        Quaternion rot = new Quaternion();
        FlockAgent newAgent = Instantiate(
                agentPrefab,
                randomSquare,
                rot,
                transform
                );

        newAgent.name = agentPrefab.name + " " + currI;

        newAgent.SetFlock(this);
        agents.Add(newAgent);
        copyAgents.Add(newAgent);
        if (currI < soldiersCount) InstantiateSoldier();
        else currI = 0;
        return newAgent;


    }

    public GameObject CreateNewSoldier(float speed, float life, float hitPower)
    {
        currI = soldiersCount;
        soldiersCount++;

        FlockAgent newSoldier = InstantiateSoldier();
        newSoldier.GetComponent<Soldier>().SetHealth(life);
        newSoldier.GetComponent<Soldier>().SetSpeed(speed);
        newSoldier.GetComponent<Attack>().SetHitPower(hitPower);

        return newSoldier.gameObject;
    }


    public GameObject CreateNewWorker(int life, float miningSpeed)
    {
        currI = soldiersCount;
        soldiersCount++;
        FlockAgent newWorker = InstantiateSoldier();
        newWorker.GetComponent<Worker>().health = life;
        newWorker.GetComponent<Worker>().miningSpeed = miningSpeed;


        return newWorker.gameObject;

    }

    public void SetNewTarget(GameObject target, bool isEnemy)
    {
        if (prev == null && target != null && !firstCall)
        {
            foreach (FlockAgent agent in agents)
            {
                if (agent != null)
                {
                    Walk walk = agent.GetComponent<Walk>();
                    if (isEnemy)
                    {
                        walk.SetTarget(target.transform);
                        walk.MoveForwardTo(target, false);
                    }
                    else
                    {

                        Collider2D[] ans = new Collider2D[1];
                        ans[0] = target.GetComponent<Collider2D>();
                        walk.MoveToTarget(ans, false);
                    }
                }


            }
            this.prev = target;

        }
        firstCall = false;
        prev = target;
    }


    public void SetNewTargetPos(Vector3 targetPos)
    {
        if (prevPos == null && !firstCall)
        {
            foreach (FlockAgent agent in agents)
            {
                Walk walk = agent.GetComponent<Walk>();
                walk.MoveToTargetPos(targetPos, false);


            }
            this.prevPos = targetPos;

        }
        firstCall = false;
        this.prevPos = targetPos;
    }






}