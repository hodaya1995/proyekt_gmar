using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
 
    public GameObject instantiatePlace;

    [Range(1, 500)]
    public int soldiersCount = 6;
  
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
    List<FlockAgent> copyAgents =new List<FlockAgent>();
    float transRad = 0.2f;
    GameObject prev;
    bool firstCall=true;
    Vector3 prevPos = Vector3.zero;



    void Start()
    {
        randomSquare = new Vector2();
        InstantiateSoldier();
      
           
    }
    void InstantiateSoldier()
    {
        float x = instantiatePlace.transform.position.x;
        float y = instantiatePlace.transform.position.y + 0.5f;

       
        if (currI % 2 == 0)
        {
            randomSquare = new Vector2(-transRad + x, transRad * (soldiersCount - currI) + y );
        }
        else
        {
            randomSquare = new Vector2(transRad + x, transRad * (soldiersCount - currI + 1) + y);

        }

        currI++;

        Quaternion rot = new Quaternion();
        FlockAgent newAgent = Instantiate(
                agentPrefab,
                randomSquare,
                rot,
                transform
                );

        newAgent.name = agentPrefab.name+" " + currI;
        
        newAgent.Initialize(this);
        agents.Add(newAgent);
        copyAgents.Add(newAgent);
        if (currI % 2 == 0 && currI < soldiersCount)
        {
            InstantiateSoldier();
            //Invoke("InstantiateSoldier", 1f);

        }
        else if(currI % 2 == 1 && currI < soldiersCount)
        {
            InstantiateSoldier();
        }
    }



    public void SetNewTarget(GameObject target, bool isEnemy)
    {

        if (prev == null && target!=null&&!firstCall)
        {
            foreach (FlockAgent agent in agents)
            {
                Walk walk = agent.GetComponent<Walk>();
                if (isEnemy)
                {
                    walk.SetTarget(target.transform);
                    walk.MoveForwardTo(target, false);
                }
                else
                {

                    Collider2D[] ans= new Collider2D[1];
                    ans[0] = target.GetComponent<Collider2D>();
                    walk.MoveToTarget(ans , false);
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
