using UnityEngine;
using Pathfinding;

public class Walk : MonoBehaviour
{

    Transform mySoldier;
    float speed=10f;
    float nextWaypointDistance=0.14f;
    Path path;
    int currentWaypoint=0;
    bool reachedEndOfPath=false;
    Seeker seeker;
    Rigidbody2D myRb;
    Transform enemy;    
    bool isMoving=false;
    Animator myAnimator;
    bool facingRight=true;
    bool targetChosen;
    bool soldierChosen;
  
  


    void onPathComplete(Path p){
        if(!p.error){
            path=p;
            currentWaypoint= path.vectorPath.Count-1;
            targetChosen=true;
            isMoving=true;
            myAnimator.SetFloat("speed", 0.02f);

        }
    }



    void FixedUpdate()
    {
         if (targetChosen)
        {
            if (enemy != null)
            {
                MoveToPath();
            }
            else
            {
                isMoving = false;

            }
            
            
            if (!isMoving)
            {
                myAnimator.SetFloat("speed", 0f);
                targetChosen = false;
                soldierChosen = false;
                CancelInvoke();
            }

            if (reachedEndOfPath)
            {
                isMoving = false;
                soldierChosen = false;
                targetChosen = false;
                CancelInvoke();

            }
        }


    }



    void Update()
    { 
         
        if(!targetChosen){
            //bool detectedTouch = Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase;
            bool detectedTouch = Input.GetMouseButtonDown(0);

     
            if (detectedTouch) {
      


                //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);



                if (hitInformation.collider != null)
                {
                 

                    if (this.tag== hitInformation.collider.tag)
                    {
                        mySoldier = hitInformation.collider.transform;
                        myRb = hitInformation.collider.GetComponent<Rigidbody2D>();
                        myRb.drag = 1.5f;
                        myAnimator = hitInformation.collider.GetComponent<Animator>();
                        soldierChosen = true;

                    }
                    else if (soldierChosen)
                    {
                        if (hitInformation.collider.tag.Contains("soldier")&& (hitInformation.collider.tag!=this.tag))
                        {
                            enemy = hitInformation.collider.transform;
                            BuildPathToTarget();
                            soldierChosen = false;
                            targetChosen = true;
                            reachedEndOfPath = false;
                        }

                    }


                }
                else
                {
                    if (soldierChosen)
                    {
                        soldierChosen = false;
                        targetChosen = false;
                    }
                }

            }



            

        }   
    }


    void MoveToPath(){
        
        if (!reachedEndOfPath){   
            if(path==null){
                return;
            } 
            if(currentWaypoint<=0){
                
                reachedEndOfPath = true;
                return;
            } 
            
            else{
               reachedEndOfPath=false;
            }
            Vector2 direction=((Vector2)path.vectorPath[currentWaypoint]- myRb.position).normalized;
            Vector2 force=direction*speed*Time.deltaTime;



            myRb.AddForce(force);



            float distance =Vector2.Distance(myRb.position, (Vector2) path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                Debug.Log("" + currentWaypoint + "/" + path.vectorPath.Count);
                currentWaypoint--;
                return;

            }


        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] targets;
        targets = GameObject.FindGameObjectsWithTag("colony soldier");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
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
        return closest;
    }


    void MoveForwardTo(GameObject soldier)
    {
        mySoldier = this.GetComponent<Transform>();
        myRb = this.GetComponent<Rigidbody2D>();
        myAnimator = this.GetComponent<Animator>();
        enemy = soldier.GetComponent<Transform>();
        BuildPathToTarget();
        reachedEndOfPath = false;
        targetChosen = true;

    }
    void Flip(float horizontal)
    {
        if ((horizontal < 0 || facingRight) && (horizontal > 0 || !facingRight))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Start()
    {
        facingRight = true;

        //if (this.tag == "enemy soldier")
        //{
        //    enemyToAttack = FindClosestEnemy();
        //    MoveForwardTo(enemyToAttack);
        //}
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
            seeker.StartPath(enemy.position, mySoldier.position, onPathComplete);
        }
    }
}
