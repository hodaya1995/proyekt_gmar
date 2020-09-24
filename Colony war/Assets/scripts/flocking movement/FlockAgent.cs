
using UnityEngine;


[RequireComponent(typeof(Collider2D))]

public class FlockAgent : MonoBehaviour
{
    
    Flock agentFlock;
    Collider2D agentCollider;
    const float AgentDensity = 0.02f;
    Transform target;
    float distance;
    Rigidbody2D rb;


  
    public Rigidbody2D GetRigidbody()
    {
        return this.rb;
    }
    public Transform GetTarget()
    {
       
        return target;
        
    }


    public float GetDistance()
    {
        return this.distance;
    }


    public void SetFlock(Flock flock)
    {
        agentFlock = flock;
    }



   
    public Flock AgentFlock { get { return agentFlock; } }

   
    public Collider2D AgentCollider { get { return agentCollider; } }

  
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();

       
    }


    /// <summary>
    /// every frame: calculate the distance to target. 
    /// </summary>
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

  


    
}
