using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    
    Flock agentFlock;
    const float AgentDensity = 0.02f;
    Transform target;
    float distance;
    List<Transform> leaders;
    List<Transform> line;
    bool isLeader;
    Rigidbody2D rb;


    public Rigidbody2D GetRigidbody()
    {
        return this.rb;
    }
    public Transform GetTarget()
    {
       
        return target;
        
    }
    public void SetLine(List<FlockAgent> line)
    {
        List<Transform> trans = new List<Transform>();
        for(int i=0; i< line.Capacity; i++)
        {
            trans.Add(line[i].transform);
        }
        this.line = trans;

    }

    public void SetLeaders(List<FlockAgent> leaders)
    {
        List<Transform> trans = new List<Transform>();
        for (int i = 0; i < leaders.Capacity; i++)
        {
            trans.Add(leaders[i].transform);
        }
        this.leaders = trans;
    }

    public void SetIsLeader(bool isLeader)
    {
        this.isLeader = isLeader;
    }

    public bool IsLeader()
    {
        return this.isLeader;
    }

    public List<Transform> GetLeaders()
    {
        return this.leaders;
    }

    public List<Transform> GetLine()
    {
        return this.line;
    }
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();

        
    }

    private void Update()
    {
        Walk walk = GetComponent<Walk>();
        if (walk != null)
        {
            target = walk.target;
            if (target != null)
            {
                distance = Mathf.Abs(Vector2.Distance(target.position, this.transform.position));
            }
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            this.rb = rb;
        }

    }

    public float GetDistance()
    {
        return this.distance;
    }
    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }
  
    public void Move(Vector2 velocity)
    {
       this.gameObject.GetComponentInChildren<Rigidbody2D>().AddForce((Vector3)velocity);
       
    }

    
}
