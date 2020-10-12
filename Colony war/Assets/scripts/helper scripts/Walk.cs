//using UnityEngine;
//using Pathfinding;
//using System.Collections.Generic;
//using UnityEngine.Rendering;
//using System;

//public class Walk : MonoBehaviour
//{
//    Transform mySoldier;
//    public float speed = 20f;
//    float nextWaypointDistance = 0.14f;
//    Path path;
//    int currentWaypoint = 0;
//    bool reachedEndOfPath = false;
//    Seeker seeker;
//    Rigidbody2D myRb;
//    Vector3 targetPos;
//    public Transform target;
//    bool isMoving = false;
//    Animator myAnimator;
//    public bool targetChosen;
//    bool soldierChosen;
//    public bool facingRight = false;
//    GameObject enemyToAttack;
//    bool automaticWalk = false;
//    string res;
//    bool moveToRigidbody;
//    public Vector2 Velocity = new Vector2(0, 0);
//    bool search;
//    float prevVelocityX = Mathf.Infinity;
//    float prevVelocityY = Mathf.Infinity;

//    Vector2 point;

//    bool stuck;
//    GameObject frontSoldier;
//    GameObject targetObject;
//    bool startedToMove;
//    public Collider2D[] potentialTargets;

//    bool If_Butoon_Collide_On_Tool_Building = false;


//    Collider2D[] resourcesAround;
//    GameObject resource;

//    public void SetSearch(bool search)
//    {
//        this.search = search;
//    }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (!startedToMove)
//        {
//            startedToMove = true;
//        }


//        if (!stuck)
//        {

//            if (collision.gameObject.GetComponent<Animator>() != null)
//            {


//                if ((((this.tag == collision.collider.tag && this.name.Split(' ')[0] == collision.collider.name.Split(' ')[0]) &&
//                 ((!collision.gameObject.GetComponent<Animator>().GetBool("move")) || collision.rigidbody.velocity.magnitude < 0.05f))
//                || collision.gameObject.GetComponent<Animator>().GetBool("die"))
//                || (this.tag == "enemy soldier" && collision.collider.tag == "enemy building") || (this.tag == "colony soldier" && collision.collider.tag == "colony building"))//stuck
//                {
//                    Walk colWalk = collision.gameObject.GetComponent<Walk>();
//                    if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
//                    {
//                        StopMovingToPath(true);
//                    }
//                    else
//                    {
//                        stuck = true;
//                        frontSoldier = collision.gameObject;
//                        SetSoldierAsObstacle(frontSoldier);
//                    }


//                }
//            }
//        }



//    }

//    private void OnCollisionStay2D(Collision2D collision)
//    {


//        if (!stuck)
//        {

//            if (collision.gameObject.GetComponent<Animator>() != null)
//            {

//                if (startedToMove)
//                {
//                    if ((((this.tag == collision.collider.tag && this.name.Split(' ')[0] == collision.collider.name.Split(' ')[0]) &&
//                                    ((!collision.gameObject.GetComponent<Animator>().GetBool("move")) || collision.rigidbody.velocity.magnitude < 0.05f))
//                                   || collision.gameObject.GetComponent<Animator>().GetBool("die"))
//                                   || (this.tag == "enemy soldier" && collision.collider.tag == "enemy building") || (this.tag == "colony soldier" && collision.collider.tag == "colony building"))//stuck
//                    {

//                        stuck = true;
//                        if (!collision.gameObject.GetComponent<Walk>().stuck)
//                        {
//                            Walk colWalk = collision.gameObject.GetComponent<Walk>();
//                            if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
//                            {
//                                StopMovingToPath(true);
//                            }
//                            else
//                            {
//                                frontSoldier = collision.gameObject;
//                                SetSoldierAsObstacle(frontSoldier);
//                            }
//                        }



//                    }
//                }

//            }
//        }

//        if (collision.gameObject.GetComponent<Walk>() != null)
//        {

//            if (stuck && collision.gameObject.GetComponent<Walk>().stuck && collision.gameObject.GetComponent<Animator>().GetBool("move") && GetComponent<Animator>().GetBool("move"))
//            {
//                float myHorizontal = this.GetComponent<Animator>().GetFloat("horizontal");
//                float colHoriznotal = collision.gameObject.GetComponent<Animator>().GetFloat("horizontal");
//                float myVerticall = this.GetComponent<Animator>().GetFloat("vertical");
//                float colVertical = collision.gameObject.GetComponent<Animator>().GetFloat("vertical");
//                if (Mathf.Abs(myHorizontal + colHoriznotal) < 0.05f && Mathf.Abs(myVerticall + colVertical) < 0.05f) //if they moving to  the same direction and stuck
//                {

//                    Walk colWalk = collision.gameObject.GetComponent<Walk>();
//                    if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
//                    {
//                        StopMovingToPath(true);
//                    }
//                    else
//                    {
//                        StopMovingToPath(false);
//                        RewalkToTarget();
//                        collision.gameObject.GetComponent<Walk>().StopMovingToPath(false);
//                        collision.gameObject.GetComponent<Walk>().RewalkToTarget();
//                    }
//                }
//            }
//        }



//    }
//    public void LookIfStiilStuck()
//    {
//        if (stuck)
//        {
//            BuildPathToTarget();

//        }
//        else
//        {
//            CancelInvoke("LookIfStiilStuck");
//        }
//    }



//    private void OnCollisionExit2D(Collision2D collision)
//    {

//        if (stuck)
//        {
//            if (collision.gameObject.GetComponent<Animator>() != null)
//            {

//                if (((this.tag == collision.collider.tag && this.name.Split(' ')[0] == collision.collider.name.Split(' ')[0] &&
//                 (collision.rigidbody.velocity.magnitude < 0.05f))
//                 || (this.tag == "enemy soldier" && collision.collider.tag == "enemy building") || (this.tag == "colony soldier" && collision.collider.tag == "colony building")))//stuck
//                {


//                    Walk colWalk = collision.gameObject.GetComponent<Walk>();
//                    if (!colWalk.moveToRigidbody && colWalk.reachedEndOfPath)
//                    {
//                        StopMovingToPath(true);
//                    }
//                    else
//                    {
//                        ResetSoldierAsObstacle(frontSoldier);
//                        stuck = false;
//                    }


//                }
//            }
//        }

//        //if (reachedEndOfPath && !moveToRigidbody && !automaticWalk && this.tag == collision.gameObject.tag)
//        //{
//        //    collision.gameObject.GetComponent<Walk>().StopMovingToPath();
//        //}
//    }





//    void SetSoldierAsObstacle(GameObject frontSoldier)
//    {
//        if (frontSoldier != null)
//        {
//            DynamicGridObstacle obstacle = frontSoldier.GetComponent<DynamicGridObstacle>();
//            string frontSoldierLayer = frontSoldier.name.Split(' ')[0];
//            obstacle.enabled = true;
//            Flock.SetAsObstacle(true, frontSoldierLayer);
//        }

//    }


//    void ResetSoldierAsObstacle(GameObject soldier)
//    {

//        stuck = false;
//        if (soldier != null)
//        {
//            DynamicGridObstacle obstacle = soldier.GetComponent<DynamicGridObstacle>();
//            obstacle.enabled = false;


//            string frontSoldierLayer = soldier.name.Split(' ')[0];

//            Flock.SetAsObstacle(false, frontSoldierLayer);
//        }


//    }


//    public void StopMovingToPath(bool stopAllUnit)
//    {
//        isMoving = false;
//        targetChosen = false;
//        CancelInvoke("UpdatePath");
//        reachedEndOfPath = true;
//        if (myAnimator == null)
//        {
//            myAnimator = this.gameObject.GetComponent<Animator>();
//        }

//        myAnimator.SetBool("move", false);
//        if (myRb == null)
//        {
//            myRb = this.gameObject.GetComponent<Rigidbody2D>();
//        }
//        myRb.AddForce(new Vector2(0, 0));
//        Collider2D[] unit = GetNearSoldiers(this.transform.position);

//        //if(unit !=null && unit.Length > 0 && stopAllUnit)
//        //{
//        //    for (int i = 0; i < unit.Length; i++)
//        //    {
//        //        if(unit[i].transform.position)
//        //        unit[i].GetComponent<Walk>().StopMovingToPath(false);
//        //    }
//        //}

//    }

//    public void RewalkToTarget()
//    {
//        if (!automaticWalk && moveToRigidbody)
//        {
//            soldierChosen = false;
//            targetChosen = true;
//            reachedEndOfPath = false;

//            BuildPathToTarget();
//        }

//    }


//    void FixedUpdate()
//    {

//        Animator anim = this.GetComponent<Animator>();
//        bool move = true;

//        if (anim != null)
//        {
//            move = anim.GetBool("move");
//        }
//        if (!move)
//        {
//            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

//        }
//        else
//        {
//            bool attacked = this.GetComponent<Attacked>().IsAttacked();//
//            if (attacked)
//            {

//                GameObject attacker = this.GetComponent<Attacked>().attacker;
//                if (attacker != null && this.GetComponent<Attack>() != null)
//                {
//                    this.GetComponent<Attack>().AttackCollision(attacker);
//                }


//            }
//        }



//        if (stuck && this.GetComponent<Animator>().GetBool("toAttack") && frontSoldier != null)
//        {

//            ResetSoldierAsObstacle(frontSoldier);
//        }

//        if (targetChosen)
//        {

//            MoveToPath();
//        }
//        else
//        {

//            if (automaticWalk && search)
//            {
//                SearchAndAttack();
//            }
//        }

//        if (target != null && targetObject != null)
//        {
//            if (target.tag.Contains("building"))
//            {

//                if (target.GetComponent<DestructOfBuilding>().IsObstacle())
//                {
//                    float distanceToBuilding = Mathf.Abs(Vector3.Distance(target.position, this.transform.position));
//                    if (distanceToBuilding < 3f)
//                    {
//                        ResetBuildingAsObstacle(targetObject);
//                        target.GetComponent<DestructOfBuilding>().SetObstacle(false);
//                    }

//                }

//            }

//            if (target.tag == "resource" && this.tag.Contains("gold miner"))
//            {


//                float distanceToBuilding = Mathf.Abs(Vector3.Distance(target.position, this.transform.position));
//                if (distanceToBuilding < 3f)
//                {
//                    Resource.ResetResourceAsObstacle(targetObject);

//                }



//            }
//        }



//    }

//    void ResetBuildingAsObstacle(GameObject building)
//    {

//        DynamicGridObstacle obstacle = building.GetComponent<DynamicGridObstacle>();
//        obstacle.enabled = false;
//        Flock.SetAsObstacle(false, "buildings");

//    }

//    public Vector2 GetCurrentMoveToPoint()
//    {
//        return point;
//    }
//    public bool ContainMyTag(Collider2D[] objects)
//    {

//        for (int i = 0; i < objects.Length; i++)
//        {
//            if (objects[i].tag.Contains("building") && this.tag.Split(' ')[0] == objects[i].tag.Split(' ')[0])
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//    void Update()
//    {


//        if (!automaticWalk)
//        {
//            //bool detectedTouch = Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase;
//            bool detectedTouch = Input.GetMouseButtonDown(0);
//            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            bool detectedTouchInZone = Move_out_of_zone.OnZone_Characters(p.x, p.y);

//            if (!detectedTouchInZone)
//            {
//                soldierChosen = false;
//            }


//            if (detectedTouch && detectedTouchInZone && !If_Butoon_Collide_On_Tool_Building)
//            {
//                //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//                //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
//                //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
//                Vector3 mousePos = Input.mousePosition;

//                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

//                Collider2D[] nearSoldiers1 = GetNearSoldiers(mousePos);

//                bool barSelected = false;


//                if (((hitInformation.collider != null && nearSoldiers1 != null && nearSoldiers1.Length > 0) || (hitInformation.collider.tag == "gold miner colony" && this.tag == "gold miner colony")) && !soldierChosen)
//                {
//                    if (hitInformation.collider.gameObject.transform != null && hitInformation.collider.gameObject.transform.parent != null)
//                    {
//                        barSelected = hitInformation.collider.gameObject.transform.parent.gameObject.name == "bar of buildings" && hitInformation.collider.gameObject.name != "collider black"
//                        && hitInformation.collider.gameObject.GetComponent<SpriteRenderer>() != null;
//                    }

//                    if (this.tag == "gold miner colony")
//                    {

//                        nearSoldiers1 = new Collider2D[1];
//                        nearSoldiers1[0] = hitInformation.collider;
//                    }
//                    Transform t = nearSoldiers1[0].transform;

//                    float dis = Mathf.Infinity;
//                    foreach (Collider2D collider in nearSoldiers1)
//                    {
//                        float currDis = Mathf.Abs(Vector3.Distance(collider.transform.position, mousePos));
//                        if (currDis < dis)
//                        {
//                            dis = currDis;
//                            t = collider.transform;
//                        }
//                    }





//                    if (((this.gameObject.name.Split(' ')[0] == t.gameObject.name.Split(' ')[0]) && (this.tag == t.tag)) && this.name.Contains("soldier") || (this.name == t.name))
//                    {

//                        mySoldier = t.transform;
//                        myRb = t.GetComponent<Rigidbody2D>();
//                        myRb.drag = 1.5f;
//                        myAnimator = t.GetComponent<Animator>();

//                        soldierChosen = true;

//                    }



//                }

//                else if ((!ContainMyTag(nearSoldiers1)) && !barSelected)
//                {
//                    if (soldierChosen)
//                    {

//                        Collider2D[] nearSoldiers2 = GetNearSoldiers(mousePos);
//                        if (hitInformation.collider.tag == "gold")
//                        {
//                            nearSoldiers2 = new Collider2D[1];
//                            nearSoldiers2[0] = hitInformation.collider;
//                        }

//                        moveToRigidbody = hitInformation.collider != null && nearSoldiers2.Length > 0;

//                        if (moveToRigidbody)
//                        {

//                            if (this.tag == "gold miner colony")
//                            {
//                                MoveToTarget(nearSoldiers2, false);
//                            }
//                            else
//                            {
//                                MoveToTarget(nearSoldiers2, true);
//                            }




//                        }
//                        else
//                        {

//                            if (this.tag == "gold miner colony")
//                            {
//                                MoveToTargetPos(mousePos, false);
//                            }
//                            else
//                            {
//                                MoveToTargetPos(mousePos, true);
//                            }



//                        }
//                        soldierChosen = false;

//                    }
//                }
//                else
//                {
//                    soldierChosen = false;
//                }
//                ////touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
//                // //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
//                // //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
//                // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                // RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
//                // Vector3 mousePos = Input.mousePosition;

//                // mousePos = Camera.main.ScreenToWorldPoint(mousePos);
//                // Debug.Log(mousePos);
//                // Collider2D[] nearSoldiers1 = GetNearSoldiers(mousePos);

//                // bool barSelected = false;
//                // if (hitInformation.collider.gameObject.transform !=null && hitInformation.collider.gameObject.transform.parent != null)
//                // {
//                //     barSelected = hitInformation.collider.gameObject.transform.parent.gameObject.name == "bar of buildings" && hitInformation.collider.gameObject.name != "collider black"
//                //     && hitInformation.collider.gameObject.GetComponent<SpriteRenderer>() != null;
//                // }
//                // Debug.Log("barSelected " + barSelected);
//                // if (((hitInformation.collider != null && nearSoldiers1 != null && nearSoldiers1.Length > 0) || (hitInformation.collider.tag == "gold miner colony"  &&this.tag == "gold miner colony"))&& !soldierChosen)
//                // {
//                //     Debug.Log("walk  ");
//                //     if (this.tag == "gold miner colony")
//                //     {

//                //         nearSoldiers1 = new Collider2D[1];
//                //         nearSoldiers1[0] = hitInformation.collider;
//                //     }
//                //     Transform t = nearSoldiers1[0].transform;

//                //     float dis = Mathf.Infinity;
//                //     foreach (Collider2D collider in nearSoldiers1)
//                //     {
//                //         float currDis = Mathf.Abs(Vector3.Distance(collider.transform.position, mousePos));
//                //         if (currDis < dis)
//                //         {
//                //             dis = currDis;
//                //             t = collider.transform;
//                //         }
//                //     }





//                //     if (((this.gameObject.name.Split(' ')[0] == t.gameObject.name.Split(' ')[0]) && (this.tag == t.tag))&& this.name.Contains("soldier")|| (this.name == t.name))
//                //     {
//                //         Debug.Log(t.name + " " + this.name);
//                //         mySoldier = t.transform;
//                //         myRb = t.GetComponent<Rigidbody2D>();
//                //         myRb.drag = 1.5f;
//                //         myAnimator = t.GetComponent<Animator>();
//                //         Debug.Log("soldier chosen");
//                //         soldierChosen = true;

//                //     }



//                // }

//                // else if ((!ContainMyTag(nearSoldiers1))&&!barSelected)
//                // {
//                //     if (soldierChosen)
//                //     {

//                //         Collider2D[] nearSoldiers2 = GetNearSoldiers(mousePos);
//                //         if(hitInformation.collider.tag == "gold" )
//                //         {
//                //             nearSoldiers2 = new Collider2D[1];
//                //             nearSoldiers2[0] = hitInformation.collider;
//                //          }

//                //         moveToRigidbody = hitInformation.collider != null && nearSoldiers2.Length > 0;

//                //         if (moveToRigidbody)
//                //         {
//                //             Debug.Log("MoveToTarget "+this.name);
//                //             if (this.tag == "gold miner colony")
//                //             {
//                //                 MoveToTarget(nearSoldiers2, false);
//                //             }
//                //             else
//                //             {
//                //                 MoveToTarget(nearSoldiers2, true);
//                //             }




//                //         }
//                //         else
//                //         {
//                //             Debug.Log("MoveToTargetPos");
//                //             if (this.tag == "gold miner colony")
//                //             {
//                //                 MoveToTargetPos(mousePos, false);
//                //             }
//                //             else{
//                //                 MoveToTargetPos(mousePos, true);
//                //             }



//                //         }
//                //         soldierChosen = false;

//                //     }
//                // }
//                // else
//                // {
//                //     soldierChosen = false;
//                // }

//            }





//        }
//    }



//    Collider2D[] GetNearSoldiers(Vector3 pos)
//    {
//        Collider2D[] allColliders = Physics2D.OverlapCircleAll(pos, 3f);
//        if (allColliders.Length > 0)
//        {
//            List<Collider2D> ans = new List<Collider2D>();

//            Collider2D closest = allColliders[0];
//            foreach (Collider2D collider in allColliders)
//            {
//                if (Mathf.Abs(Vector3.Distance(closest.gameObject.transform.position, pos)) > Mathf.Abs(Vector3.Distance(collider.gameObject.transform.position, pos)))
//                {
//                    closest = collider;
//                }
//            }
//            foreach (Collider2D collider in allColliders)
//            {
//                if (collider.gameObject.layer == closest.gameObject.layer)
//                {
//                    ans.Add(collider);
//                }
//            }
//            return ans.ToArray();
//        }
//        return allColliders;
//    }
//    public void MoveToTarget(Collider2D[] targets, bool setNewTarget)
//    {

//        potentialTargets = targets;
//        mySoldier = this.GetComponent<Transform>();
//        GameObject target = targets[0].gameObject;
//        float dis = Mathf.Infinity;
//        foreach (Collider2D t in targets)
//        {
//            float currDis = Mathf.Abs(Vector3.Distance(t.transform.position, mySoldier.position));
//            Attacked attckComp = t.GetComponent<Attacked>();
//            bool isAttacked = false;
//            if (attckComp != null)
//            {
//                isAttacked = attckComp.IsTargeted();
//            }
//            if (currDis < dis && !isAttacked)
//            {
//                dis = currDis;
//                target = t.gameObject;
//            }
//        }
//        if (target.GetComponent<Attacked>() != null)
//        {
//            target.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
//            target.GetComponent<Attacked>().SetTargeted(true);
//        }

//        myRb = this.GetComponent<Rigidbody2D>();
//        myRb.drag = 1.5f;
//        myAnimator = this.GetComponent<Animator>();
//        moveToRigidbody = true;
//        this.targetObject = target;
//        this.target = target.transform;

//        if (resource != null && resource.GetComponent<Resource>() != null)
//        {
//            resource.GetComponent<Resource>().occupied = false;

//            for (int i = 0; i < resourcesAround.Length; i++)
//            {

//                resourcesAround[i].GetComponent<Resource>().occupied = false;
//            }
//        }
//        resource = target;
//        resourcesAround = Physics2D.OverlapCircleAll(resource.transform.position, 3f);
//        if (resource != null && resource.GetComponent<Resource>() != null)
//        {
//            resource.GetComponent<Resource>().occupied = true;
//            for (int i = 0; i < resourcesAround.Length; i++)
//            {
//                if (resourcesAround[i].GetComponent<Resource>() != null)
//                {
//                    resourcesAround[i].GetComponent<Resource>().occupied = true;
//                }

//            }
//        }

//        if (setNewTarget && GetComponentInParent<Flock>() != null) GetComponentInParent<Flock>().SetNewTarget(target, false);
//        soldierChosen = false;
//        targetChosen = true;
//        reachedEndOfPath = false;
//        BuildPathToTarget();

//        //potentialTargets = targets;
//        //mySoldier = this.GetComponent<Transform>();
//        //GameObject target = targets[0].gameObject;
//        //float dis = Mathf.Infinity;
//        //foreach (Collider2D t in targets)
//        //{
//        //    float currDis = Mathf.Abs(Vector3.Distance(t.transform.position, mySoldier.position));
//        //    Attacked attckComp = t.GetComponent<Attacked>();
//        //    bool isAttacked = false;
//        //    if (attckComp != null)
//        //    {
//        //        isAttacked = attckComp.IsTargeted();
//        //    }
//        //    if (currDis < dis && !isAttacked)
//        //    {
//        //        dis = currDis;
//        //        target = t.gameObject;
//        //    }
//        //}
//        //if (target.GetComponent<Attacked>() != null)
//        //{
//        //    target.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
//        //    target.GetComponent<Attacked>().SetTargeted(true);
//        //}

//        //myRb = this.GetComponent<Rigidbody2D>();
//        //myRb.drag = 1.5f;
//        //myAnimator = this.GetComponent<Animator>();
//        //moveToRigidbody = true;
//        //this.targetObject = target;
//        //this.target = target.transform;

//        //if (setNewTarget && GetComponentInParent<Flock>() != null) GetComponentInParent<Flock>().SetNewTarget(target, false);
//        //soldierChosen = false;
//        //targetChosen = true;
//        //reachedEndOfPath = false;
//        //BuildPathToTarget();

//    }

//    public void MoveToTargetPos(Vector3 targetPos, bool setNewTarget)
//    {
//        mySoldier = this.GetComponent<Transform>();
//        myRb = this.GetComponent<Rigidbody2D>();
//        myRb.drag = 1.5f;
//        myAnimator = this.GetComponent<Animator>();
//        moveToRigidbody = false;
//        this.targetPos = targetPos;
//        if (setNewTarget && GetComponentInParent<Flock>() != null) GetComponentInParent<Flock>().SetNewTargetPos(targetPos);
//        soldierChosen = false;
//        targetChosen = true;
//        reachedEndOfPath = false;
//        BuildPathToTarget();

//    }


//    public void SetAutomaticWalking(bool automaticWalk)
//    {
//        this.automaticWalk = automaticWalk;
//    }


//    void Start()
//    {

//        Seeker s = this.gameObject.AddComponent<Seeker>();

//        seeker = s;
//        search = true;
//        DynamicGridObstacle obstacle = this.GetComponent<DynamicGridObstacle>();
//        obstacle.enabled = false;
//        string layer = this.name.Split(' ')[0];
//        Flock.SetAsObstacle(false, layer);

//        res = "gold";
//        if (this.tag == "gold miner enemy")
//        {

//            LookForResorces(res);
//        }
//    }

//    void LookForResorces(string res)
//    {

//        GameObject[] targets;
//        targets = GameObject.FindGameObjectsWithTag(res);

//        GameObject closest = null;
//        float distance = Mathf.Infinity;
//        Vector3 position = this.transform.position;
//        foreach (GameObject target in targets)
//        {
//            Vector3 diff = target.transform.position - position;
//            float curDistance = diff.sqrMagnitude;

//            if (curDistance < distance && !target.GetComponent<Resource>().occupied)
//            {

//                closest = target;
//                distance = curDistance;

//            }
//        }


//        if (closest != null)
//        {
//            if (resource != null && resource.GetComponent<Resource>() != null)
//            {

//                resource.GetComponent<Resource>().occupied = false;

//                for (int i = 0; i < resourcesAround.Length; i++)
//                {

//                    resourcesAround[i].GetComponent<Resource>().occupied = false;
//                }
//            }
//            resource = closest;
//            resourcesAround = Physics2D.OverlapCircleAll(resource.transform.position, 1f);

//            if (resource != null && resource.GetComponent<Resource>() != null)
//            {

//                resource.GetComponent<Resource>().occupied = true;
//                for (int i = 0; i < resourcesAround.Length; i++)
//                {
//                    if (resourcesAround[i].GetComponent<Resource>() != null)
//                    {
//                        resourcesAround[i].GetComponent<Resource>().occupied = true;
//                    }

//                }
//            }
//            MoveForwardTo(closest, true);
//        }
//        else
//        {
//            Debug.LogAssertion("there is no resorces to find");
//        }
//        //GameObject[] targets;
//        //targets = GameObject.FindGameObjectsWithTag(res);

//        //GameObject closest = null;
//        //float distance = Mathf.Infinity;
//        //Vector3 position = this.transform.position;
//        //foreach (GameObject target in targets)
//        //{
//        //    Vector3 diff = target.transform.position - position;
//        //    float curDistance = diff.sqrMagnitude;

//        //    if (curDistance < distance)
//        //    {

//        //        closest = target;
//        //        distance = curDistance;

//        //    }
//        //}


//        //if (closest != null)
//        //{

//        //    MoveForwardTo(closest, true);
//        //}
//        //else
//        //{
//        //    Debug.LogAssertion("there is no resorces to find");
//        //}
//    }
//    void onPathComplete(Path p)
//    {
//        if (!p.error)
//        {
//            path = p;
//            currentWaypoint = path.vectorPath.Count - 1;
//            targetChosen = true;
//            isMoving = true;
//            reachedEndOfPath = false;
//            if (!myAnimator.GetBool("toAttack")) myAnimator.SetBool("move", true);

//        }

//    }

//    public void MoveToResorce()
//    {
//        res = this.tag.Split(' ')[0];
//        LookForResorces(res);
//    }

//    private void Flip()//flip the animation
//    {
//        facingRight = !facingRight;
//        Vector3 theScale = transform.localScale;
//        theScale.x *= -1;
//        transform.localScale = theScale;
//    }

//    private void FlipAnimation()
//    {

//        float h = myAnimator.GetFloat("horizontal");
//        if (h > 0 && !facingRight)
//            Flip();
//        else if (h < 0 && facingRight)
//            Flip();
//    }



//    public bool IsMoving()
//    {
//        return isMoving;
//    }




//    void MoveToPath()
//    {
//        isMoving = true;
//        if (!reachedEndOfPath)
//        {


//            if (path == null)
//            {
//                return;
//            }
//            if (currentWaypoint <= 0)
//            {
//                reachedEndOfPath = true;

//                StopMovingToPath(true);
//                return;
//            }

//            else
//            {

//                reachedEndOfPath = false;
//            }

//            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - myRb.position).normalized;


//            Vector2 force = direction * speed * Time.deltaTime;


//            point = force;


//            myRb.AddForce(force);



//            if (myAnimator.GetBool("move"))
//            {

//                float horizontalVelocity = Vector2.Dot(direction, Vector2.right);
//                float verticalVelocity = Vector2.Dot(direction, Vector2.up);
//                if (prevVelocityX != Mathf.Infinity && prevVelocityY != Mathf.Infinity && (prevVelocityX < 0 && horizontalVelocity < 0) || (prevVelocityX > 0 && horizontalVelocity > 0) || (prevVelocityX == 0 && horizontalVelocity == 0)
//                    && (prevVelocityY < 0 && verticalVelocity < 0) || (prevVelocityY > 0 && verticalVelocity > 0) || (prevVelocityY == 0 && verticalVelocity == 0))
//                {
//                    myAnimator.SetFloat("horizontal", horizontalVelocity);
//                    myAnimator.SetFloat("vertical", verticalVelocity);

//                }
//                else if (prevVelocityX == Mathf.Infinity && prevVelocityY == Mathf.Infinity)
//                {
//                    myAnimator.SetFloat("horizontal", horizontalVelocity);
//                    myAnimator.SetFloat("vertical", verticalVelocity);

//                }
//                prevVelocityX = horizontalVelocity;
//                prevVelocityY = verticalVelocity;


//                float h = direction.x;
//                if (h > 0 && !facingRight)
//                    Flip();
//                else if (h < 0 && facingRight)
//                    Flip();

//                FlipAnimation();
//            }


//            float distance = Vector2.Distance(myRb.position, (Vector2)path.vectorPath[currentWaypoint]);

//            if (distance < nextWaypointDistance)
//            {
//                currentWaypoint--;
//                return;

//            }


//        }
//    }


//    GameObject FindClosestEnemy()
//    {
//        GameObject[] targets;
//        string[] tagsToSearch = new string[] { "colony soldier", "gold miner colony", "colony building" };
//        GameObject closest = null;
//        float distance = Mathf.Infinity;
//        foreach (string t in tagsToSearch)
//        {

//            targets = GameObject.FindGameObjectsWithTag(t);

//            Vector3 position = transform.position;
//            foreach (GameObject target in targets)
//            {

//                Attacked currAttackedTarget = target.GetComponent<Attacked>();
//                Attacked currAttacked = transform.GetComponent<Attacked>();
//                Vector3 diff = target.transform.position - position;
//                float curDistance = diff.sqrMagnitude;
//                bool targeted = true;
//                if (this.target != null && target.GetComponent<Attacked>() != null)
//                {
//                    targeted = (!target.GetComponent<Attacked>().IsTargeted() && this.target.name != target.name);
//                }
//                if (curDistance < distance && targeted)
//                {
//                    closest = target;
//                    distance = curDistance;

//                }


//            }
//        }
//        if (distance == Mathf.Infinity)
//        {
//            return null;
//        }

//        if (closest.GetComponent<Attacked>() != null)
//        {
//            closest.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
//            closest.GetComponent<Attacked>().SetTargeted(true);
//        }

//        return closest;



//    }


//    public void MoveForwardTo(GameObject soldier, bool setNewTarget)
//    {
//        targetObject = soldier;
//        mySoldier = this.GetComponent<Transform>();
//        myRb = this.GetComponent<Rigidbody2D>();
//        myAnimator = this.GetComponent<Animator>();
//        target = soldier.GetComponent<Transform>();
//        if (setNewTarget) GetComponentInParent<Flock>().SetNewTarget(enemyToAttack, true);
//        moveToRigidbody = true;
//        BuildPathToTarget();

//    }

//    public void SetTarget(Transform target)
//    {
//        this.target = target;
//    }

//    public void BuildPathToTarget()
//    {
//        seeker = mySoldier.GetComponent<Seeker>();
//        InvokeRepeating("UpdatePath", 0f, 0.5f);
//    }

//    void UpdatePath()
//    {

//        if (mySoldier != null)
//        {
//            if (seeker.IsDone() && !reachedEndOfPath)
//            {
//                targetChosen = true;

//                if (moveToRigidbody)
//                {
//                    if (target == null)
//                    {
//                        CancelInvoke("UpdatePath");
//                        return;
//                    }
//                    seeker.StartPath(target.position, mySoldier.position, onPathComplete);
//                }
//                else

//                {

//                    seeker.StartPath(targetPos, mySoldier.position, onPathComplete);
//                }




//            }
//        }
//        else
//        {
//            CancelInvoke("UpdatePath");
//        }


//    }


//    void SearchAndAttack()
//    {

//        enemyToAttack = FindClosestEnemy();

//        if (enemyToAttack != null)
//        {
//            if (target != null)
//            {
//                if (target.name != enemyToAttack.transform.name)
//                {
//                    if (enemyToAttack.GetComponent<Attacked>() != null)
//                    {
//                        enemyToAttack.GetComponent<Attacked>().SetTargeted(false);
//                    }

//                }
//            }

//            target = enemyToAttack.transform;

//        }


//        bool attacking = this.GetComponent<Animator>().GetBool("toAttack");


//        if (enemyToAttack != null && myRb != null && myAnimator != null && target != null)
//        {


//            if (!attacking)
//            {

//                if (seeker != null)
//                {

//                    seeker.CancelCurrentPathRequest();
//                    reachedEndOfPath = false;
//                    targetChosen = true;

//                    MoveForwardTo(enemyToAttack, true);

//                }

//            }



//        }
//        else
//        {
//            if (enemyToAttack != null && !attacking)
//            {
//                MoveForwardTo(enemyToAttack, true);

//            }

//        }

//    }

//    public void Set_If_Butoon_Collide_On_Tool_Building(bool b)
//    {
//        this.If_Butoon_Collide_On_Tool_Building = b;
//    }

//}
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

    bool If_Butoon_Collide_On_Tool_Building = false;


    Collider2D[] resourcesAround;
    GameObject resource;

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
            Flock.SetAsObstacle(true, frontSoldierLayer);
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

            Flock.SetAsObstacle(false, frontSoldierLayer);
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

        myAnimator.SetBool("move", false);
        if (myRb == null)
        {
            myRb = this.gameObject.GetComponent<Rigidbody2D>();
        }
        myRb.AddForce(new Vector2(0, 0));
       
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
            if (target.tag.Contains("building"))
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

            if (target.tag == "resource" && this.tag.Contains("gold miner"))
            {


                float distanceToBuilding = Mathf.Abs(Vector3.Distance(target.position, this.transform.position));
                if (distanceToBuilding < 3f)
                {
                    Resource.ResetResourceAsObstacle(targetObject);

                }



            }
        }



    }

    void ResetBuildingAsObstacle(GameObject building)
    {

        DynamicGridObstacle obstacle = building.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = false;
        Flock.SetAsObstacle(false, "buildings");

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


            if (detectedTouch && detectedTouchInZone && !If_Butoon_Collide_On_Tool_Building)
            {
                //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                Vector3 mousePos = Input.mousePosition;

                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

             
                bool barSelected = false;


                if (((hitInformation.collider != null) && !soldierChosen))
                {
                    if (hitInformation.collider.gameObject.transform != null && hitInformation.collider.gameObject.transform.parent != null)
                    {
                        barSelected = hitInformation.collider.gameObject.transform.parent.gameObject.name == "bar of buildings" && hitInformation.collider.gameObject.name != "collider black"
                        && hitInformation.collider.gameObject.GetComponent<SpriteRenderer>() != null;
                    }
                    if (!barSelected && hitInformation.collider.name == this.name )
                    {
                       
                        Transform t = hitInformation.collider.transform;
                        mySoldier = t.transform;
                        myRb = t.GetComponent<Rigidbody2D>();
                        myRb.drag = 1.5f;
                        myAnimator = t.GetComponent<Animator>();

                        soldierChosen = true;

                    }
                    else
                    {
                        soldierChosen = false;
                    }




                }

                else if (!barSelected && soldierChosen)
                {
                    
                        moveToRigidbody = hitInformation.collider != null;

                        if (moveToRigidbody)
                        {

                            
                            MoveToTarget(new Collider2D[] { hitInformation.collider }, false);
                           
    
                        }
                        else
                        {

                            MoveToTargetPos(mousePos, false);
                            



                        }
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
        GameObject target = targets[0].gameObject;
        if (setNewTarget)
        {
            potentialTargets = targets;
            mySoldier = this.GetComponent<Transform>();

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

        if (resource != null && resource.GetComponent<Resource>() != null)
        {
            resource.GetComponent<Resource>().occupied = false;

            for (int i = 0; i < resourcesAround.Length; i++)
            {

                resourcesAround[i].GetComponent<Resource>().occupied = false;
            }
        }
        if(target.tag == "gold")
        {
            resource = target;
        }
        else
        {
            resource = null;
        }
        if(resource != null)
        {
            resourcesAround = Physics2D.OverlapCircleAll(resource.transform.position, 3f);
            if (resource != null && resource.GetComponent<Resource>() != null)
            {
                resource.GetComponent<Resource>().occupied = true;
                for (int i = 0; i < resourcesAround.Length; i++)
                {
                    if (resourcesAround[i].GetComponent<Resource>() != null)
                    {
                        resourcesAround[i].GetComponent<Resource>().occupied = true;
                    }

                }
            }
        }
        

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
        Flock.SetAsObstacle(false, layer);

        res = "gold";
        if (this.tag == "gold miner enemy")
        {

            LookForResorces(res);
        }
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

            if (curDistance < distance && !target.GetComponent<Resource>().occupied)
            {

                closest = target;
                distance = curDistance;

            }
        }


        if (closest != null)
        {
            if (resource != null && resource.GetComponent<Resource>() != null)
            {

                resource.GetComponent<Resource>().occupied = false;

                for (int i = 0; i < resourcesAround.Length; i++)
                {

                    resourcesAround[i].GetComponent<Resource>().occupied = false;
                }
            }
            resource = closest;
            resourcesAround = Physics2D.OverlapCircleAll(resource.transform.position, 1f);

            if (resource != null && resource.GetComponent<Resource>() != null)
            {

                resource.GetComponent<Resource>().occupied = true;
                for (int i = 0; i < resourcesAround.Length; i++)
                {
                    if (resourcesAround[i].GetComponent<Resource>() != null)
                    {
                        resourcesAround[i].GetComponent<Resource>().occupied = true;
                    }

                }
            }
            MoveForwardTo(closest, true);
        }
        else
        {
            Debug.LogAssertion("there is no resorces to find");
        }
        //GameObject[] targets;
        //targets = GameObject.FindGameObjectsWithTag(res);

        //GameObject closest = null;
        //float distance = Mathf.Infinity;
        //Vector3 position = this.transform.position;
        //foreach (GameObject target in targets)
        //{
        //    Vector3 diff = target.transform.position - position;
        //    float curDistance = diff.sqrMagnitude;

        //    if (curDistance < distance)
        //    {

        //        closest = target;
        //        distance = curDistance;

        //    }
        //}


        //if (closest != null)
        //{

        //    MoveForwardTo(closest, true);
        //}
        //else
        //{
        //    Debug.LogAssertion("there is no resorces to find");
        //}
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
        //first go for the soldier
        string tag = "colony soldier";
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;

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
        if (distance == Mathf.Infinity)
        {
            //after the soldier go for the building
            tag = "colony building";
            targets = GameObject.FindGameObjectsWithTag(tag);
            closest = null;
            position = transform.position;
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
            if (distance == Mathf.Infinity)
            {
                //last attack the worker
                tag = "gold miner";
                targets = GameObject.FindGameObjectsWithTag(tag);
                closest = null;

                position = transform.position;
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

                if (distance == Mathf.Infinity)
                {
                    return null;
                }

            }

        }
        if (closest.GetComponent<Attacked>() != null)
        {
            closest.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
            closest.GetComponent<Attacked>().SetTargeted(true);
        }

        return closest;
     
    }
    //GameObject FindClosestEnemy()
    //{
    //    GameObject[] targets;
    //    string[] tagsToSearch = new string[] { "colony soldier", "gold miner colony", "colony building" };
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    foreach (string t in tagsToSearch)
    //    {

    //        targets = GameObject.FindGameObjectsWithTag(t);

    //        Vector3 position = transform.position;
    //        foreach (GameObject target in targets)
    //        {

    //            Attacked currAttackedTarget = target.GetComponent<Attacked>();
    //            Attacked currAttacked = transform.GetComponent<Attacked>();
    //            Vector3 diff = target.transform.position - position;
    //            float curDistance = diff.sqrMagnitude;
    //            bool targeted = true;
    //            if (this.target != null && target.GetComponent<Attacked>() != null)
    //            {
    //                targeted = (!target.GetComponent<Attacked>().IsTargeted() && this.target.name != target.name);
    //            }
    //            if (curDistance < distance && targeted)
    //            {
    //                closest = target;
    //                distance = curDistance;

    //            }


    //        }
    //    }
    //    if (distance == Mathf.Infinity)
    //    {
    //        return null;
    //    }

    //    if (closest.GetComponent<Attacked>() != null)
    //    {
    //        closest.GetComponent<Attacked>().SetTargetedAttacker(this.gameObject);
    //        closest.GetComponent<Attacked>().SetTargeted(true);
    //    }

    //    return closest;



    //}


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

    public void Set_If_Butoon_Collide_On_Tool_Building(bool b)
    {
        this.If_Butoon_Collide_On_Tool_Building = b;
    }

}