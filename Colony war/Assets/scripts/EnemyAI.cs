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
    bool targetIsChosen;

    Vector3 touchPosWorld;
    TouchPhase touchPhase = TouchPhase.Began;
    

    bool isMoving=false;
    Animator animator;
    bool toDie=false;
    bool toRot=false;
    bool flip=false;
    bool facingRight=true;
    float moveSpeed=2f;

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
       Debug.LogError("target: "+target.localPosition);
    }

    void UpdatePath(){
        if(seeker.IsDone()&&!reachedEndOfPath)seeker.StartPath(rb.position,target.position,onPathComplete); 
    }
    void onPathComplete(Path p){
        if(!p.error){
            path=p;
            currentWaypoint=0;
            targetIsChosen=true;
            isMoving=true;
        }
    }

    void FixedUpdate()
    { 
        if(targetIsChosen){
            MoveToPath();
        } 
        else{
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == touchPhase) {
                touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
                if (hitInformation.collider != null) {       
                    if(hitInformation.collider.tag == "colony"){
                        target=hitInformation.collider.transform;
                        BuildPathToTarget();
                        
                    }else if(hitInformation.collider.tag =="enemy"){
                        enemy=hitInformation.collider.transform;
                        rb=hitInformation.collider.GetComponent<Rigidbody2D>();
                        
                        animator=hitInformation.collider.GetComponent<Animator>();
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

            Flip(direction.x);
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
       
     
        if(targetIsChosen){

            if(isMoving){
                animator.SetFloat("speed",rb.velocity.sqrMagnitude);

                if(rb.velocity.x<0.01&&rb.velocity.y <0.01)reachedEndOfPath=true;
            }else{
                animator.SetFloat("speed",0f);
                animator.SetBool("toAttack",true);
                
                targetIsChosen=false;    
            }
            
            if(reachedEndOfPath){
                animator.SetBool("toAttack",true);
                isMoving=false;
                targetIsChosen=false;
               
            }
        }
        

    }
}
