using UnityEngine;
using Pathfinding;


public class Walk : MonoBehaviour
{
    Transform mySoldier;
    public float speed = 20f;
    float nextWaypointDistance = 0.14f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D myRb;
    Vector3 targetPos;
    Transform target;
    bool isMoving = false;
    Animator myAnimator;
    public bool targetChosen;
    bool soldierChosen;
    bool facingRight = false;
    int numOfSoldiersToKill = 0;
    GameObject enemyToAttack;
    bool automaticWalk = false;
    string res;
    bool moveToRigidbody;

    public void SetAutomaticWalking(bool automaticWalk)
    {
        this.automaticWalk = automaticWalk;
    }


    void Start()
    {
        seeker = this.gameObject.AddComponent<Seeker>();
    }

    void LookForResorces(string res)
    {
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


        if (closest != null)
        {

            MoveForwardTo(closest);
        }
        else
        {
            Debug.LogAssertion("there is no resorces to find");
        }
    }
    void onPathComplete(Path p)
    {

        if (!p.error)
        {
            path = p;
            currentWaypoint = path.vectorPath.Count - 1;
            targetChosen = true;
            isMoving = true;
            reachedEndOfPath = false;
            if (!myAnimator.GetBool("toAttack")) myAnimator.SetBool("move", true);

        }
        else
        {
            Debug.LogError("path from " + mySoldier + " is not possible. error.");

        }
    }

    public void MoveToResorce()
    {
        res = this.tag.Split(' ')[0];
        Debug.Log("res " + res);
        LookForResorces(res);
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
            FlipAnimation();
            MoveToPath();
        }
        else
        {
            if (automaticWalk) SearchAndAttack();
        }


    }

    public void StopMovingToPath()
    {
        isMoving = false;
        targetChosen = false;
        CancelInvoke();
        reachedEndOfPath = true;
        myAnimator.SetBool("move", false);
        myRb.AddForce(new Vector2(0, 0));
    }

    void Update()
    {

        if (!automaticWalk)
        {
            //bool detectedTouch = Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase;
            bool detectedTouch = Input.GetMouseButtonDown(0);


            if (detectedTouch)
            {



                //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);


                if (hitInformation.collider != null && !soldierChosen)
                {


                    if (this.tag == hitInformation.collider.tag)
                    {
                        mySoldier = hitInformation.collider.transform;
                        myRb = hitInformation.collider.GetComponent<Rigidbody2D>();
                        myRb.drag = 1.5f;
                        myAnimator = hitInformation.collider.GetComponent<Animator>();
                        soldierChosen = true;

                    }



                }
                else
                {
                    if (soldierChosen)
                    {

                        moveToRigidbody = hitInformation.collider != null;

                        if (moveToRigidbody)
                        {
                            target = hitInformation.collider.transform;
                        }
                        else
                        {
                            targetPos = mousePos;
                        }
                        
                        BuildPathToTarget();
                        soldierChosen = false;
                        targetChosen = true;
                        reachedEndOfPath = false;
                    }
                }

            }





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
            if (currentWaypoint <= 0)
            {

                reachedEndOfPath = true;
                StopMovingToPath();
                return;
            }

            else
            {
                reachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - myRb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;



            float velocityX = myRb.velocity.x;
            float velocityY = myRb.velocity.y;

            myAnimator.SetFloat("horizontal", velocityX);
            myAnimator.SetFloat("vertical", velocityY);

            myRb.velocity = force;

            float distance = Vector2.Distance(myRb.position, (Vector2)path.vectorPath[currentWaypoint]);

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
        target = soldier.GetComponent<Transform>();
        moveToRigidbody = true;
        BuildPathToTarget();

    }


    public void BuildPathToTarget()
    {
        seeker = mySoldier.GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        Debug.Log("MoveForwardTo");
        if (seeker.IsDone() && !reachedEndOfPath)
        {
            if (moveToRigidbody)
            {
                seeker.StartPath(target.position, mySoldier.position, onPathComplete);
            }
            else
            {
                seeker.StartPath(targetPos, mySoldier.position, onPathComplete);
            }
            
        }

    }


    void SearchAndAttack()
    {
        enemyToAttack = FindClosestEnemy();

        if (enemyToAttack != null && myRb != null && myAnimator != null)
        {
            bool attacking = myAnimator.GetBool("toAttack");

            if (!attacking)
            {
                float currDistance;
                if (moveToRigidbody)
                {
                    currDistance = Vector2.Distance(myRb.position, target.position);
                }
                else
                {
                    currDistance = Vector2.Distance(myRb.position, targetPos);
                }
              
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