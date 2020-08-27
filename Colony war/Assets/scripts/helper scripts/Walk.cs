using UnityEngine;
using Pathfinding;


public class Walk : MonoBehaviour
{
    Transform mySoldier;
    public float speed =20f;
    float nextWaypointDistance =0.14f;
    Path path;
    int currentWaypoint =0;
    bool reachedEndOfPath =false;
    Seeker seeker;
    Rigidbody2D myRb;
    Transform enemy;
    bool isMoving =false;
    Animator myAnimator;
    public bool targetChosen;
    bool soldierChosen;
    bool facingRight = false;
    int numOfSoldiersToKill = 0;
    GameObject enemyToAttack;
    bool toSearch = false;
    bool touchable = false;

    public void SetSearch(bool toSearch)
    {
        this.toSearch = toSearch;
    }
   
    public void SetTouchable(bool touchable)
    {
        this.touchable = touchable;
    }
    void Start()
    {
        if(toSearch)SearchAndAttack();
    }

    void onPathComplete(Path p){
     
        if (!p.error){
            path=p;
            currentWaypoint= path.vectorPath.Count-1;
            targetChosen=true;
            isMoving=true;
            reachedEndOfPath = false;
            if(!myAnimator.GetBool("toAttack"))myAnimator.SetBool("move", true);

        }
        else
        {
            Debug.LogError("path from " + mySoldier + " to " + enemy + " is not possible. error.");
         
        }
    }
    void OnCollisionEnter2D(Collision2D collision)//the attacked soldier near the attacker
    {

        if (((collision.collider.tag != this.tag) && collision.collider.tag.Contains("soldier")) || ((collision.collider.tag != this.tag) && collision.collider.tag.Contains("gold miner")))
        {
            isMoving = false;
            myAnimator.SetBool("move", false);

        }
      


    }



    private void Flip()//flip the animation
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FlipAnimation()
    {
        float h = myAnimator.GetFloat("horizontal");
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }

    void FixedUpdate()
    {
       
        if (targetChosen)
        {

            if (enemy != null)
            {
                FlipAnimation();
                MoveToPath();
            }
            else
            {
                isMoving = false;

            }
            
            
            if (!isMoving)
            {
                myAnimator.SetBool("move", false);
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
        else
        {
            if(toSearch)SearchAndAttack();
        }


    }



    void Update()
    {
        
        if (!targetChosen&&touchable){
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
            


            float velocityX = myRb.velocity.x;
            float velocityY = myRb.velocity.y;

            myAnimator.SetFloat("horizontal", velocityX);
            myAnimator.SetFloat("vertical", velocityY);

            myRb.velocity = force;
        
            float distance =Vector2.Distance(myRb.position, (Vector2) path.vectorPath[currentWaypoint]);
            
            if (distance < nextWaypointDistance)
            {
                currentWaypoint--;
                return;

            }


        }
    }


    GameObject FindClosestEnemy()
    {
        numOfSoldiersToKill = 0;
        GameObject[] targets;
        string[] tagsToSearch = new string[] { "colony soldier", "gold miner", "stone miner" };
        GameObject closest = null;

        foreach (string t in tagsToSearch)
        {
            
            targets = GameObject.FindGameObjectsWithTag(t);
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject target in targets)
            {
                numOfSoldiersToKill++;
                Vector3 diff = target.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = target;
                    distance = curDistance;

                }
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

    }

  
    public void BuildPathToTarget()
    {
        seeker = mySoldier.GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
   
        if (seeker.IsDone() && !reachedEndOfPath)
        {
            
            seeker.StartPath(enemy.position, mySoldier.position, onPathComplete);
        }

    }


    void SearchAndAttack()
    {
        enemyToAttack = FindClosestEnemy();

        if (enemyToAttack != null && myRb != null && enemy != null && myAnimator != null)
        {
            bool attacking = myAnimator.GetBool("toAttack");

            if (!attacking)
            {
                float currDistance = Vector2.Distance(myRb.position, enemy.position);
                float distance = Vector2.Distance(myRb.position, enemyToAttack.transform.position);

                if (seeker != null && currDistance > distance)
                {

                    seeker.CancelCurrentPathRequest();
                    reachedEndOfPath = false;
                    targetChosen = true;
                    MoveForwardTo(enemyToAttack);

                }
                else if (seeker != null && numOfSoldiersToKill == 1)
                {
                    reachedEndOfPath = false;
                    targetChosen = true;
                    MoveForwardTo(enemyToAttack);
                }
            }


        }
        else
        {
            if (enemyToAttack == null)
            {
                Debug.Log("there is no colony soldiers to attack ");
            }
            else
            {
                MoveForwardTo(enemyToAttack);


            }
        }

    }

}
