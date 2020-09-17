using UnityEngine;
using Pathfinding;
using System.Security.Cryptography;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
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
    public Transform target;
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
    bool stuck;

    public Vector2 Velocity = new Vector2(0, 0);

    float RotateSpeed = 0.3f;
    float startRad = 1.2f;
    float Radius;
    float startAngle;
    private Vector2 centre;
    private float angle;
    float dist;
    bool attacking;
    bool collided;
    bool search;
    float prevVelocityX = Mathf.Infinity;
    float prevVelocityY = Mathf.Infinity;
    float exitAngel = 100.0f;

    const float EXCELLENT =100;
    const float GOOD = 66.6f;
    const float NEAR_DEATH = 33.3f;
    Vector2 point;
    GameObject prevSoldierTaregted;
    Vector3 center;
    bool setCenter;

    public void SetCenter(Vector3 center)
    {
        setCenter = true;
        this.center = center;
    }

    public void SetSearch(bool search)
    {
        this.search = search;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gold")
        {
            stuck = true;
        }
        //collided = true;
        //attacking = this.gameObject.GetComponent<Animator>().GetBool("toAttack");
        //if (myRb != null && target != collision.transform && targetChosen)
        //{
        //    //if (collision.gameObject.tag == "gold")
        //        //        {
        //        //            startRad = 2.1f;
        //        //        }
        //        //        else
        //        //        {
        //        //            startRad = 1.3f;
        //        //        }

        //        //        Radius = startRad;

        //        bool collisionStuck = false;
        //    if (collision.gameObject.GetComponent<Walk>() != null)
        //    {

        //        collisionStuck = collision.gameObject.GetComponent<Walk>().IsStuck();
        //    }
        //    //if (!stuck && !attacking && !collisionStuck)
        //    if (!stuck && !attacking && !(collision.gameObject.tag.Contains("soldier")))
        //    {

        //        //            centre = collision.transform.position;
        //        //            Vector2 p1 = new Vector2(centre.x, centre.y - startRad);
        //        //            Vector2 p2 = transform.position;
        //        //            dist = Vector2.Distance(p1, p2);
        //        //            angle = Mathf.Acos(((2 * (startRad * startRad) - (Mathf.Abs(dist) * Mathf.Abs(dist))) / (2 * (startRad * startRad))));
        //        //            angle = (180 * Mathf.Deg2Rad) - angle;
        //        //            startAngle = angle;
        //        //            seeker.CancelCurrentPathRequest();
        //        //            CancelInvoke("UpdatePath");
        //        //            targetChosen = false;
        //        //            myRb.drag = 1.5f;
        //        stuck = true;

        //        //            InvokeRepeating("UpdateRadius", 0f, 0.3f);

        //    }


        //}

    }


    public bool IsStuck()
    {
        return stuck;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        stuck = false;
        CancelInvoke("UpdateRadius");
        collided = false;

    }

    void FixedUpdate()
    {
        //if (stuck)
        //{

        //    angle += RotateSpeed * Time.deltaTime;
        //    Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
        //    Vector2 moveTo = centre + offset;

        //    if (angle < (exitAngel * Mathf.Deg2Rad) + startAngle)
        //    {

        //        myRb.AddForce((moveTo - (Vector2)transform.position) * speed * Time.deltaTime);
        //        myAnimator.SetBool("move", true);
        //        myAnimator.SetFloat("horizontal", myRb.velocity.x);
        //        myAnimator.SetFloat("vertical", myRb.velocity.y);
        //        FlipAnimation();


        //    }
        //    else
        //    {
        //        CancelStuck();
        //    }



        //}


        //if (targetChosen && !stuck)
        if (targetChosen )
        {
            MoveToPath();
        }
        else
        {
            
            //if (automaticWalk && !stuck && !collided && search)
            if (automaticWalk && !collided && search)
            {
                SearchAndAttack();
            }
        }


    }


    public Vector2 GetCurrentMoveToPoint()
    {
        return point;
    }
    void Update()
    {


        if (!automaticWalk)
        {
            //bool detectedTouch = Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase;
            bool detectedTouch = Input.GetMouseButtonDown(0);
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool detectedTouchInZone = Move_out_of_zone.OnZone_Characters(p.x, p.y);

            if (!detectedTouchInZone)
            {
                soldierChosen = false;
            }
            if (detectedTouch && detectedTouchInZone)
            {



                //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                Collider2D[] nearSoldiers1 = GetNearSoldiers(mousePos);

                if ((hitInformation.collider != null || nearSoldiers1!=null) && !soldierChosen)
                {

                    Transform t = nearSoldiers1[0].transform;

                    float dis = Mathf.Infinity;
                    foreach (Collider2D collider in nearSoldiers1)
                    {
                        float currDis = Mathf.Abs(Vector3.Distance(collider.transform.position,mousePos));
                        if (currDis < dis)
                        {
                            dis = currDis;
                            t = collider.transform;
                        }
                    }


                    if (this.tag == t.tag)
                    {
                        
                        mySoldier = t.transform;
                        myRb = t.GetComponent<Rigidbody2D>();
                        myRb.drag = 1.5f;
                        myAnimator = t.GetComponent<Animator>();
                        soldierChosen = true;

                    }



                }
                else
                {
                    if (soldierChosen)
                    {

                        Collider2D [] nearSoldiers2 = GetNearSoldiers(mousePos);
                    

                        moveToRigidbody = hitInformation.collider != null && nearSoldiers2!=null;

                        if (moveToRigidbody)
                        {
                            MoveToTarget(nearSoldiers2, true);
                            
                        }
                        else
                        {
                            MoveToTargetPos(mousePos,true);
                            
                        }

                        
                    }
                }

            }





        }
    }



    Collider2D[] GetNearSoldiers(Vector3 pos)
    {
        return Physics2D.OverlapCircleAll(pos, 1.5f);
    }
    public void MoveToTarget(Collider2D[] targets, bool setNewTarget)
    {
        mySoldier = this.GetComponent<Transform>();
        GameObject target = targets[0].gameObject;
        float dis=Mathf.Infinity;
        foreach(Collider2D t in targets)
        {
            float currDis = Mathf.Abs(Vector3.Distance(t.transform.position, mySoldier.position));
            if (currDis < dis)
            {
                dis = currDis;
                target = t.gameObject;
            }
        }
        myRb = this.GetComponent<Rigidbody2D>();
        myRb.drag = 1.5f;
        myAnimator = this.GetComponent<Animator>();
        moveToRigidbody = true;
        this.target = target.transform;
        if (setNewTarget) GetComponentInParent<Flock>().SetNewTarget(target, false);
        soldierChosen = false;
        targetChosen = true;
        reachedEndOfPath = false;
        BuildPathToTarget();
       
    }

    public void MoveToTargetPos(Vector3 targetPos, bool setNewTarget)
    {
        mySoldier = this.GetComponent<Transform>();
        myRb = this.GetComponent<Rigidbody2D>();
        myRb.drag = 1.5f;
        myAnimator = this.GetComponent<Animator>();
        moveToRigidbody = false;
        this.targetPos = targetPos; 
        if (setNewTarget) GetComponentInParent<Flock>().SetNewTargetPos(targetPos);
        soldierChosen = false;
        targetChosen = true;
        reachedEndOfPath = false;
        BuildPathToTarget();
        
    }

    void CancelStuck()
    {
        BuildPathToTarget();
        //stuck = false;
    }
    public void SetAutomaticWalking(bool automaticWalk)
    {
        this.automaticWalk = automaticWalk;
    }


    void Start()
    {
        Seeker s = this.gameObject.AddComponent<Seeker>();

        seeker = s;
        search = true;

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

            MoveForwardTo(closest,true);
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
           // stuck = false;

        }
        else
        {
            Debug.LogError("path from " + mySoldier + " is not possible. error.");

        }
    }

    public void MoveToResorce()
    {
        res = this.tag.Split(' ')[0];
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
    void UpdateRadius()
    {
        if (collided)
        {
            Radius += 0.1f;
        }
        else
        {
            CancelInvoke("UpdateRadius");
        }
    }



    public bool IsMoving()
    {
        return isMoving;
    }

    public void StopMovingToPath()
    {
        isMoving = false;
        targetChosen = false;
        CancelInvoke("UpdatePath");
        reachedEndOfPath = true;
        if (myAnimator == null)
        {
            myAnimator = this.gameObject.GetComponent<Animator>();
        }
        myAnimator.SetBool("move", false);
        if (myRb == null)
        {
            myRb = this.gameObject.GetComponent<Rigidbody2D>();
        }
        myRb.AddForce(new Vector2(0, 0));
    }



    void MoveToPath()
    {
        isMoving = true;
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
            Vector2 direction =Vector2.zero;
            if (setCenter)
            {
                 direction = 0.7f*(((Vector2)path.vectorPath[currentWaypoint] - myRb.position).normalized) +(Vector2) center*0.3f;
                setCenter = false;
            }
            else
            {
                direction = ((Vector2)path.vectorPath[currentWaypoint] - myRb.position).normalized;
            }
         
            Vector2 force = direction * speed * Time.deltaTime;


            point = force;

           myRb.AddForce(force);
            
           

            float horizontalVelocity = Vector2.Dot(direction, Vector2.right);
            float verticalVelocity = Vector2.Dot(direction, Vector2.up);
            if (prevVelocityX != Mathf.Infinity && prevVelocityY != Mathf.Infinity && (prevVelocityX < 0 && horizontalVelocity < 0) || (prevVelocityX > 0 && horizontalVelocity > 0) || (prevVelocityX == 0 && horizontalVelocity == 0)
                && (prevVelocityY < 0 && verticalVelocity < 0) || (prevVelocityY > 0 && verticalVelocity > 0) || (prevVelocityY == 0 && verticalVelocity == 0))
            {
                myAnimator.SetFloat("horizontal", horizontalVelocity);
                myAnimator.SetFloat("vertical", verticalVelocity);

            }
            else if (prevVelocityX == Mathf.Infinity && prevVelocityY == Mathf.Infinity)
            {
                myAnimator.SetFloat("horizontal", horizontalVelocity);
                myAnimator.SetFloat("vertical", verticalVelocity);

            }
            prevVelocityX = horizontalVelocity;
            prevVelocityY = verticalVelocity;

            float h = direction.x;
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();

            FlipAnimation();

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
        string[] tagsToSearch = new string[] { "colony soldier", "gold miner" };
        GameObject closestDefault = null;
        GameObject closest = null;

        foreach (string t in tagsToSearch)
        {

            targets = GameObject.FindGameObjectsWithTag(t);
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject target in targets)
            {
                
                    Attacked currAttackedTarget = target.GetComponent<Attacked>();
                    Attacked currAttacked = transform.GetComponent<Attacked>();
                    float currHelathPrecentTarget = currAttackedTarget.GetHealth() * 1.0f / currAttackedTarget.GetMaxHealth() * 1.0f;
                    float currHelathPrecent = currAttacked.GetHealth() * 1.0f / currAttacked.GetMaxHealth() * 1.0f;
                    numOfSoldiersToKill++;
                    Vector3 diff = target.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closestDefault = target;
                        distance = curDistance;
                        if ((GOOD <= currHelathPrecentTarget && currHelathPrecentTarget <= EXCELLENT && GOOD <= currHelathPrecent && currHelathPrecentTarget <= EXCELLENT) ||
                            (NEAR_DEATH <= currHelathPrecentTarget && currHelathPrecentTarget <= GOOD && NEAR_DEATH <= currHelathPrecent && currHelathPrecentTarget <= GOOD) ||
                            (0 <= currHelathPrecentTarget && currHelathPrecentTarget <= NEAR_DEATH && 0 <= currHelathPrecent && currHelathPrecentTarget <= NEAR_DEATH))
                        {
                            closest = target;

                        }
                    }
                
                
            }
        }
      
        if (closest != null) return closest;
        return closestDefault;



    }


    public void MoveForwardTo(GameObject soldier,bool setNewTarget)
    {
        mySoldier = this.GetComponent<Transform>();
        myRb = this.GetComponent<Rigidbody2D>();
        myAnimator = this.GetComponent<Animator>();
        target = soldier.GetComponent<Transform>();
        if(setNewTarget) GetComponentInParent<Flock>().SetNewTarget(enemyToAttack,true);
        moveToRigidbody = true;
        BuildPathToTarget();

    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void BuildPathToTarget()
    {

        seeker = mySoldier.GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        
        if (mySoldier != null)
        {
            if (seeker.IsDone() && !reachedEndOfPath)
            {
                targetChosen = true;

                if (moveToRigidbody)
                {
                    if (target == null)
                    {
                        CancelInvoke("UpdatePath");
                        return;
                    }
                    seeker.StartPath(target.position, mySoldier.position, onPathComplete);
                }
                else

                {

                    seeker.StartPath(targetPos, mySoldier.position, onPathComplete);
                }




            }
        }
        else
        {
            CancelInvoke("UpdatePath");
        }


    }


    void SearchAndAttack()
    {
        enemyToAttack = FindClosestEnemy();
        if (enemyToAttack != null)
        {
            target = enemyToAttack.transform;

        }


        bool attacking = this.GetComponent<Animator>().GetBool("toAttack");

      
        if (enemyToAttack != null && myRb != null && myAnimator != null && target != null)
        {
         
         
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

                if (seeker != null) 
                {

                    seeker.CancelCurrentPathRequest();
                    reachedEndOfPath = false;
                    targetChosen = true;
                    
                    MoveForwardTo(enemyToAttack,true);

                }
             
            }
          


        }
        else
        {
           if (enemyToAttack != null && !attacking)
            {
                MoveForwardTo(enemyToAttack,true);


            }
           
        }

    }

}