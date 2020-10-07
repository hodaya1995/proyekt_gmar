using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System;

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
    public bool facingRight = false;
    GameObject enemyToAttack;
    bool automaticWalk = false;
    string res;
    bool moveToRigidbody;
    public Vector2 Velocity = new Vector2(0, 0);
    bool search;
    float prevVelocityX = Mathf.Infinity;
    float prevVelocityY = Mathf.Infinity;

    Vector2 point;

    bool stuck;
    GameObject frontSoldier;
    GameObject targetObject;
    bool startedToMove;
    public Collider2D[] potentialTargets;



    public void SetSearch(bool search)
    {
        this.search = search;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!startedToMove)
        {
            startedToMove = true;
        }


        if (!stuck)
        {

            if (collision.gameObject.GetComponent<Animator>() != null)
            {


                if ((((this.tag == collision.collider.tag && this.name.Split(' ')[0] == collision.collider.name.Split(' ')[0]) &&
                 ((!collision.gameObject.GetComponent<Animator>().GetBool("move")) || collision.rigidbody.velocity.magnitude < 0.05f))
                || collision.gameObject.GetComponent<Animator>().GetBool("die"))
                || (this.tag == "enemy soldier" && collision.collider.tag == "enemy building") || (this.tag == "colony soldier" && collision.collider.tag == "colony building"))//stuck
                {
                    Walk colWalk = collision.gameObject.GetComponent<Walk>();
                    if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
                    {
                        StopMovingToPath(true);
                    }
                    else
                    {
                        stuck = true;
                        frontSoldier = collision.gameObject;
                        SetSoldierAsObstacle(frontSoldier);
                    }


                }
            }
        }



    }

    private void OnCollisionStay2D(Collision2D collision)
    {


        if (!stuck)
        {

            if (collision.gameObject.GetComponent<Animator>() != null)
            {

                if (startedToMove)
                {
                    if ((((this.tag == collision.collider.tag && this.name.Split(' ')[0] == collision.collider.name.Split(' ')[0]) &&
                                    ((!collision.gameObject.GetComponent<Animator>().GetBool("move")) || collision.rigidbody.velocity.magnitude < 0.05f))
                                   || collision.gameObject.GetComponent<Animator>().GetBool("die"))
                                   || (this.tag == "enemy soldier" && collision.collider.tag == "enemy building") || (this.tag == "colony soldier" && collision.collider.tag == "colony building"))//stuck
                    {

                        stuck = true;
                        if (!collision.gameObject.GetComponent<Walk>().stuck)
                        {
                            Walk colWalk = collision.gameObject.GetComponent<Walk>();
                            if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
                            {
                                StopMovingToPath(true);
                            }
                            else
                            {
                                frontSoldier = collision.gameObject;
                                SetSoldierAsObstacle(frontSoldier);
                            }
                        }



                    }
                }

            }
        }

        if (collision.gameObject.GetComponent<Walk>() != null)
        {

            if (stuck && collision.gameObject.GetComponent<Walk>().stuck && collision.gameObject.GetComponent<Animator>().GetBool("move") && GetComponent<Animator>().GetBool("move"))
            {
                float myHorizontal = this.GetComponent<Animator>().GetFloat("horizontal");
                float colHoriznotal = collision.gameObject.GetComponent<Animator>().GetFloat("horizontal");
                float myVerticall = this.GetComponent<Animator>().GetFloat("vertical");
                float colVertical = collision.gameObject.GetComponent<Animator>().GetFloat("vertical");
                if (Mathf.Abs(myHorizontal + colHoriznotal) < 0.05f && Mathf.Abs(myVerticall + colVertical) < 0.05f) //if they moving to  the same direction and stuck
                {

                    Walk colWalk = collision.gameObject.GetComponent<Walk>();
                    if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
                    {
                        StopMovingToPath(true);
                    }
                    else
                    {
                        StopMovingToPath(false);
                        RewalkToTarget();
                        collision.gameObject.GetComponent<Walk>().StopMovingToPath(false);
                        collision.gameObject.GetComponent<Walk>().RewalkToTarget();
                    }
                }
            }
        }



    }
    public void LookIfStiilStuck()
    {
        if (stuck)
        {
            BuildPathToTarget();

        }
        else
        {
            CancelInvoke("LookIfStiilStuck");
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {

        if (stuck)
        {
            if (collision.gameObject.GetComponent<Animator>() != null)
            {

                if (((this.tag == collision.collider.tag && this.name.Split(' ')[0] == collision.collider.name.Split(' ')[0] &&
                 (collision.rigidbody.velocity.magnitude < 0.05f))
                 || (this.tag == "enemy soldier" && collision.collider.tag == "enemy building") || (this.tag == "colony soldier" && collision.collider.tag == "colony building")))//stuck
                {


                    Walk colWalk = collision.gameObject.GetComponent<Walk>();
                    if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
                    {
                        StopMovingToPath(true);
                    }
                    else
                    {
                        ResetSoldierAsObstacle(frontSoldier);
                        stuck = false;
                    }


                }
            }
        }

        //if (reachedEndOfPath && !moveToRigidbody && !automaticWalk && this.tag == collision.gameObject.tag)
        //{
        //    collision.gameObject.GetComponent<Walk>().StopMovingToPath();
        //}
    }





    void SetSoldierAsObstacle(GameObject frontSoldier)
    {
        if (frontSoldier != null)
        {
            DynamicGridObstacle obstacle = frontSoldier.GetComponent<DynamicGridObstacle>();
            string frontSoldierLayer = frontSoldier.name.Split(' ')[0];
            obstacle.enabled = true;
            this.GetComponentInParent<Flock>().SetAsObstacle(true, frontSoldierLayer);
        }

    }


    void ResetSoldierAsObstacle(GameObject soldier)
    {

        stuck = false;
        if (soldier != null)
        {
            DynamicGridObstacle obstacle = soldier.GetComponent<DynamicGridObstacle>();
            obstacle.enabled = false;


            string frontSoldierLayer = soldier.name.Split(' ')[0];

            this.GetComponentInParent<Flock>().SetAsObstacle(false, frontSoldierLayer);
        }


    }


    public void StopMovingToPath(bool stopAllUnit)
    {
        isMoving = false;
        targetChosen = false;
        CancelInvoke("UpdatePath");
        reachedEndOfPath = true;
        if (myAnimator == null)
        {
            myAnimator = this.gameObject.GetComponent<Animator>();
        }
        Debug.Log(this.name);
        myAnimator.SetBool("move", false);
        if (myRb == null)
        {
            myRb = this.gameObject.GetComponent<Rigidbody2D>();
        }
        myRb.AddForce(new Vector2(0, 0));
        Collider2D[] unit = GetNearSoldiers(this.transform.position);
        Debug.Log(unit.Length);
        //if(unit !=null && unit.Length > 0 && stopAllUnit)
        //{
        //    for (int i = 0; i < unit.Length; i++)
        //    {
        //        if(unit[i].transform.position)
        //        unit[i].GetComponent<Walk>().StopMovingToPath(false);
        //    }
        //}

    }

    public void RewalkToTarget()
    {
        if (!automaticWalk && moveToRigidbody)
        {
            soldierChosen = false;
            targetChosen = true;
            reachedEndOfPath = false;

            BuildPathToTarget();
        }

    }


    void FixedUpdate()
    {

        Animator anim = this.GetComponent<Animator>();
        bool move = true;

        if (anim != null)
        {
            move = anim.GetBool("move");
        }
        if (!move)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        }
        else
        {
            bool attacked = this.GetComponent<Attacked>().IsAttacked();//
            if (attacked)
            {

                GameObject attacker = this.GetComponent<Attacked>().attacker;
                if (attacker != null && this.GetComponent<Attack>() != null)
                {
                    this.GetComponent<Attack>().AttackCollision(attacker);
                }


            }
        }



        if (stuck && this.GetComponent<Animator>().GetBool("toAttack") && frontSoldier != null)
        {

            ResetSoldierAsObstacle(frontSoldier);
        }

        if (targetChosen)
        {

            MoveToPath();
        }
        else
        {

            if (automaticWalk && search)
            {
                SearchAndAttack();
            }
        }
        if (target != null && targetObject != null)
        {
            if (target.name.Contains("building"))
            {

                if (target.GetComponent<DestructOfBuilding>().IsObstacle())
                {
                    float distanceToBuilding = Mathf.Abs(Vector3.Distance(target.position, this.transform.position));
                    if (distanceToBuilding < 3f)
                    {
                        ResetBuildingAsObstacle(targetObject);
                        target.GetComponent<DestructOfBuilding>().SetObstacle(false);
                    }

                }

            }
        }



    }

    void ResetBuildingAsObstacle(GameObject building)
    {

        DynamicGridObstacle obstacle = building.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = false;
        this.GetComponentInParent<Flock>().SetAsObstacle(false, "buildings");

    }

    public Vector2 GetCurrentMoveToPoint()
    {
        return point;
    }
    public bool ContainMyTag(Collider2D[] objects)
    {

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].tag.Contains("building") && this.tag.Split(' ')[0] == objects[i].tag.Split(' ')[0])
            {
                return true;
            }
        }
        return false;
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
                if ((hitInformation.collider != null && nearSoldiers1 != null && nearSoldiers1.Length > 0) && !soldierChosen)
                {

                    Transform t = nearSoldiers1[0].transform;

                    float dis = Mathf.Infinity;
                    foreach (Collider2D collider in nearSoldiers1)
                    {
                        float currDis = Mathf.Abs(Vector3.Distance(collider.transform.position, mousePos));
                        if (currDis < dis)
                        {
                            dis = currDis;
                            t = collider.transform;
                        }
                    }



                    if ((this.gameObject.name.Split(' ')[0] == t.gameObject.name.Split(' ')[0]) && (this.tag == t.tag))
                    {
                        mySoldier = t.transform;
                        myRb = t.GetComponent<Rigidbody2D>();
                        myRb.drag = 1.5f;
                        myAnimator = t.GetComponent<Animator>();
                        soldierChosen = true;

                    }



                }
                else if (!ContainMyTag(nearSoldiers1))
                {
                    if (soldierChosen)
                    {

                        Collider2D[] nearSoldiers2 = GetNearSoldiers(mousePos);


                        moveToRigidbody = hitInformation.collider != null && nearSoldiers2.Length > 0;

                        if (moveToRigidbody)
                        {
                            MoveToTarget(nearSoldiers2, true);

                        }
                        else
                        {

                            MoveToTargetPos(mousePos, true);

                        }


                    }
                }
                else
                {
                    soldierChosen = false;
                }

            }





        }
    }



    Collider2D[] GetNearSoldiers(Vector3 pos)
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(pos, 3f);
        if (allColliders.Length > 0)
        {
            List<Collider2D> ans = new List<Collider2D>();

            Collider2D closest = allColliders[0];
            foreach (Collider2D collider in allColliders)
            {
                if (Mathf.Abs(Vector3.Distance(closest.gameObject.transform.position, pos)) > Mathf.Abs(Vector3.Distance(collider.gameObject.transform.position, pos)))
                {
                    closest = collider;
                }
            }
            foreach (Collider2D collider in allColliders)
            {
                if (collider.gameObject.layer == closest.gameObject.layer)
                {
                    ans.Add(collider);
                }
            }
            return ans.ToArray();
        }
        return allColliders;
    }
    public void MoveToTarget(Collider2D[] targets, bool setNewTarget)
    {

        potentialTargets = targets;
        mySoldier = this.GetComponent<Transform>();
        GameObject target = targets[0].gameObject;
        float dis = Mathf.Infinity;
        foreach (Collider2D t in targets)
        {
            float currDis = Mathf.Abs(Vector3.Distance(t.transform.position, mySoldier.position));
            Attacked attckComp = t.GetComponent<Attacked>();
            bool isAttacked = false;
            if (attckComp != null)
            {
                isAttacked = attckComp.IsTargeted();
            }
            if (currDis < dis && !isAttacked)
            {
                dis = currDis;
                target = t.gameObject;
            }
        }
        if (target.GetComponent<Attacked>() != null)
        {
            target.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
            target.GetComponent<Attacked>().SetTargeted(true);
        }

        myRb = this.GetComponent<Rigidbody2D>();
        myRb.drag = 1.5f;
        myAnimator = this.GetComponent<Animator>();
        moveToRigidbody = true;
        this.targetObject = target;
        this.target = target.transform;

        if (setNewTarget && GetComponentInParent<Flock>() != null) GetComponentInParent<Flock>().SetNewTarget(target, false);
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
        if (setNewTarget && GetComponentInParent<Flock>() != null) GetComponentInParent<Flock>().SetNewTargetPos(targetPos);
        soldierChosen = false;
        targetChosen = true;
        reachedEndOfPath = false;
        BuildPathToTarget();

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
        DynamicGridObstacle obstacle = this.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = false;
        string layer = this.name.Split(' ')[0];
        this.GetComponentInParent<Flock>().SetAsObstacle(false, layer);
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

            MoveForwardTo(closest, true);
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



    public bool IsMoving()
    {
        return isMoving;
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

                StopMovingToPath(true);
                return;
            }

            else
            {

                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - myRb.position).normalized;


            Vector2 force = direction * speed * Time.deltaTime;


            point = force;


            myRb.AddForce(force);



            if (myAnimator.GetBool("move"))
            {

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
            }


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
        GameObject[] targets;
        string[] tagsToSearch = new string[] { "colony soldier", "gold miner", "colony building" };
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (string t in tagsToSearch)
        {

            targets = GameObject.FindGameObjectsWithTag(t);

            Vector3 position = transform.position;
            foreach (GameObject target in targets)
            {

                Attacked currAttackedTarget = target.GetComponent<Attacked>();
                Attacked currAttacked = transform.GetComponent<Attacked>();
                Vector3 diff = target.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                bool targeted = true;
                if (this.target != null && target.GetComponent<Attacked>() != null)
                {
                    targeted = (!target.GetComponent<Attacked>().IsTargeted() && this.target.name != target.name);
                }
                if (curDistance < distance && targeted)
                {
                    closest = target;
                    distance = curDistance;

                }


            }
        }
        if (distance == Mathf.Infinity)
        {
            return null;
        }

        if (closest.GetComponent<Attacked>() != null)
        {
            closest.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
            closest.GetComponent<Attacked>().SetTargeted(true);
        }

        return closest;



    }


    public void MoveForwardTo(GameObject soldier, bool setNewTarget)
    {
        targetObject = soldier;
        mySoldier = this.GetComponent<Transform>();
        myRb = this.GetComponent<Rigidbody2D>();
        myAnimator = this.GetComponent<Animator>();
        target = soldier.GetComponent<Transform>();
        if (setNewTarget) GetComponentInParent<Flock>().SetNewTarget(enemyToAttack, true);
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
            if (target != null)
            {
                if (target.name != enemyToAttack.transform.name)
                {
                    if (enemyToAttack.GetComponent<Attacked>() != null)
                    {
                        enemyToAttack.GetComponent<Attacked>().SetTargeted(false);
                    }

                }
            }

            target = enemyToAttack.transform;

        }


        bool attacking = this.GetComponent<Animator>().GetBool("toAttack");


        if (enemyToAttack != null && myRb != null && myAnimator != null && target != null)
        {


            if (!attacking)
            {

                if (seeker != null)
                {

                    seeker.CancelCurrentPathRequest();
                    reachedEndOfPath = false;
                    targetChosen = true;

                    MoveForwardTo(enemyToAttack, true);

                }

            }



        }
        else
        {
            if (enemyToAttack != null && !attacking)
            {
                MoveForwardTo(enemyToAttack, true);

            }

        }

    }

}