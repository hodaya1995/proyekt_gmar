using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed=2f;
    public Rigidbody2D rb;
    Vector3 touchPosition,whereToMove,prevMovement;
    bool isMoving=false;
    float prevoiusDistanceToTouchPos,currDistanceToTouchPos;
    Touch touch;
    public Animator animator;
    bool toDie=false;
    bool toRot=false;
    bool flip=false;
    bool facingRight;
    bool invokeTouch=false;
   
    
    void Start(){
        facingRight=true;
    }
 
     void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }




    void Flip(float horizontal){
        //if(!(horizontal>0&&!facingRight||horizontal<0&&facingRight)){
        if((horizontal<0||facingRight)&&(horizontal>0||!facingRight)){
            facingRight=!facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    void Update()
    {
       
     
     
     if(isMoving){
         currDistanceToTouchPos=(touchPosition-transform.position).magnitude;
         animator.SetFloat("horizontal",whereToMove.x);
         animator.SetFloat("vertical",whereToMove.y);
         animator.SetFloat("speed",whereToMove.sqrMagnitude);
         //animator.SetBool("toAttack",false);      
     }
     else{
            animator.SetFloat("speed",0);
            //animator.SetBool("toAttack",true);    
     }

   
     
           
         if (invokeTouch &&Input.touchCount > 0){

            touch = Input.GetTouch(0);
            
            if(touch.phase== TouchPhase.Began){
               //shotOnce=false;
                animator.SetFloat("horizontal",whereToMove.x);
                animator.SetFloat("vertical",whereToMove.y);
                animator.SetFloat("speed",whereToMove.sqrMagnitude);
                //animator.SetBool("toAttack",false);
                prevoiusDistanceToTouchPos=0;
                currDistanceToTouchPos=0;
                isMoving=true;
                touchPosition=Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z=0;
                prevMovement=whereToMove;
                whereToMove=(touchPosition-transform.position).normalized;
                rb.velocity=new Vector2(whereToMove.x*moveSpeed,whereToMove.y*moveSpeed); 


                Flip(whereToMove.x);
                /* if(prevMovement.x<whereToMove.x||prevMovement.y<whereToMove.y){
                    flip=true;
                }else{
                        flip=false;
                }
                if(flip){
                    Flip();
                } */ 

                
            }
           

            

        }

        if(currDistanceToTouchPos>prevoiusDistanceToTouchPos){
            //animator.SetBool("toAttack",true);
            isMoving=false;
            rb.velocity=Vector2.zero;
        }
        if(isMoving){
           prevoiusDistanceToTouchPos=(touchPosition-transform.position).magnitude;
        }

    }

  



}
