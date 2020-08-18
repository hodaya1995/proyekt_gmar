using UnityEngine;
using Pathfinding;
public class MineResources : MonoBehaviour
{
    Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;
    Transform resource;

    bool isMoving = false;
    Animator animator;
    bool targetChosen;
    string res;
 
    void FixedUpdate()
    {
        if (targetChosen)
        {
            MoveToPath();
        }
        if (isMoving&& reachedEndOfPath)
        {
            if (rb.velocity.magnitude < 0.1)
            {
                isMoving = false;
                targetChosen = false;
                CancelInvoke();
                reachedEndOfPath = false;
                animator.SetFloat("speed", 0f);
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == res)
        {
            Debug.Log("collided");
            isMoving = false;
            targetChosen = false;
            CancelInvoke();
            reachedEndOfPath = false;
            animator.SetFloat("speed", 0f);
            rb.AddForce(new Vector2(0,0));
            animator.SetBool("mine", true);
        }
    }

 
  

    void BuildPathToTarget()
    {

        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && !reachedEndOfPath)
        {
            seeker.StartPath(resource.position, target.position, onPathComplete);
        }
    }
    void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            targetChosen = true;
            isMoving = true;
            animator.SetFloat("speed",0.02f);


        }
    }

    void MoveToPath()
    {
        if (!reachedEndOfPath)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }

            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);
           

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }


        }
    }

    void MoveForwardTo(GameObject res)
    {
        target = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        resource = res.GetComponent<Transform>();
        BuildPathToTarget();
        reachedEndOfPath = false;
        targetChosen = true;
    }

    void LookForResorces(string res)
    {
        Debug.Log("res" + res+"!");
        GameObject[] targets;
        targets = GameObject.FindGameObjectsWithTag(res);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = this.transform.position;
        foreach (GameObject target in targets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;

            }
        }


        MoveForwardTo(closest);
    }
    void Start()
    {       
        res = this.tag.Split(' ')[0];
        Debug.Log("this tag " + this.tag + " tag " + res);
        LookForResorces(res);  
    }

  

}
