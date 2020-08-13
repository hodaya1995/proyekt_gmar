using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    Transform target;
    public float speed=50f;
    public float nextWaypointDistance=3f;
    Path path;
    int currentWaypoint=0;
    bool reachedEndOfPath=false;
    Seeker seeker;
    Rigidbody2D rb;
    Transform enemy;

    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Began;
    

    bool isMoving=false;
    Animator animator;
    bool toDie=false;
    bool toRot=false;
    bool flip=false;
    bool facingRight=true;
    float moveSpeed=2f;
    bool targetChosen;
    bool soldierChosen;
    void Flip(float horizontal){
        if((horizontal<0||facingRight)&&(horizontal>0||!facingRight)){
            facingRight=!facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Start(){
       facingRight=true;
    }

    void BuildPathToTarget(){
       seeker=GetComponent<Seeker>();
       InvokeRepeating("UpdatePath",0f,0.5f);
    }

    void UpdatePath(){
        Debug.Log("UpdatePath");
        if(seeker.IsDone()&&!reachedEndOfPath)seeker.StartPath(enemy.position,target.position,onPathComplete); 
    }
    void onPathComplete(Path p){
        if(!p.error){
            path=p;
            currentWaypoint=0;
            targetChosen=true;
            isMoving=true;
        }
    }

   





    void FixedUpdate()
    { 
        if(targetChosen)
        {
            MoveToPath();
        } 
        else{
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
                  
                    if (hitInformation.collider.tag == "colony")
                    {
                        Debug.Log("colony");
                        target = hitInformation.collider.transform;
                        rb = hitInformation.collider.GetComponent<Rigidbody2D>();
                        animator = hitInformation.collider.GetComponent<Animator>();
                        soldierChosen = true;

                    }
                    else if (soldierChosen)
                    {
                        if (hitInformation.collider.tag == "enemy")
                        {
                            Debug.Log("enemy");
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
                        Debug.Log("cancel");
                        soldierChosen = false;
                        targetChosen = false;
                    }
                }

                




            }



            

        }   
    }

    void MoveToPath(){
        if(!reachedEndOfPath){   
            if(path==null){
                return;
            } 
            if(currentWaypoint>=path.vectorPath.Count){
                return;
            } 
            
            else{
               reachedEndOfPath=false;
            }

            Vector2 direction=((Vector2)path.vectorPath[currentWaypoint]-rb.position).normalized;
            Vector2 force=direction*speed*Time.deltaTime;
            
            rb.AddForce(force);

            float distance=Vector2.Distance(rb.position,path.vectorPath[currentWaypoint]);
            if(distance<nextWaypointDistance){
                currentWaypoint++;
            }

            //Flip(direction.x);

            // if(rb.velocity.x>=0.03f){
            //     enemy.localScale=new Vector3(-1f,1f,1f);
            // }
            // else if(rb.velocity.x<=-0.03f){
            //     enemy.localScale=new Vector3(1f,1f,1f);
                
            // }

        }
    }

    void Update()
    {
       
     
        if(targetChosen){

            if(isMoving){
                animator.SetFloat("speed",rb.velocity.sqrMagnitude);

                if (rb.velocity.x < 0.01 && rb.velocity.y < 0.01)
                {
                    reachedEndOfPath = true;
                }
            }
            else{
                animator.SetFloat("speed",0f);
                targetChosen = false;
                soldierChosen = false;
                CancelInvoke();
            }
            
            if(reachedEndOfPath){
                isMoving=false;
                soldierChosen = false;
                targetChosen = false;
                CancelInvoke();

            }
        }
        

    }
}
