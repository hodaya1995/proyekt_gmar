using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attackable : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 5;
	public int currentHealth;
    bool attacked=false;
    bool died=false;
    public HealthBar healthBar;
    GameObject attacker;

    int callDieFunc=0;
    Color fade;
    public static float velocityX;
    public static float velocityY;
    Rigidbody2D rb;
    Boolean collided = false;
    Animator attackerAnimator;

       void Start()
    {
        currentHealth = maxHealth;
        callDieFunc = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
       
        rb = this.GetComponent<Rigidbody2D>();
    }

     void OnCollisionEnter2D(Collision2D collision)//the attacked soldier near the attacker
    {
        if(collision.collider.tag=="enemy"||collision.collider.tag=="colony"){
            collided = true;
            InvokeRepeating("DecreaseLife",0f,0.5f);
            attacker=collision.gameObject;
            animator.SetBool("toAttack", true);

            attackerAnimator = attacker.GetComponent<Animator>();
            velocityX = collision.relativeVelocity.x;
            velocityY = collision.relativeVelocity.y;

            if (Mathf.Abs(velocityX) > Mathf.Abs(velocityY))
            {
                attackerAnimator.SetFloat("horizontal", velocityX);
                attackerAnimator.SetFloat("vertical", 0);
            }
            else
            {
                attackerAnimator.SetFloat("horizontal", 0);
                attackerAnimator.SetFloat("vertical", velocityY);
            }



        }


    }

    void Update(){
        if (collided)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (attackerAnimator.GetBool("die"))//the attacker died
        {
            CancelInvoke(); //stop dying
            animator.SetBool("toAttack", false);
        }
    }
    void Die(){
       
            callDieFunc--;
            if (callDieFunc >= 0) {
                fade = GetComponent<Renderer> ().material.color;
                fade.a = fade.a /(1.0f+(1.0f/callDieFunc));
                GetComponent<Renderer>().material.color = fade;
    
                if(fade.a <=.1){
                    int childs=this.transform.childCount;
                    for(int i=0;i<childs; i++){
                        Destroy(this.transform.GetChild(i).gameObject);
                    }
                    Destroy (this.gameObject);
                    died=false;
                    
                }
                    
            }
    }
    void DecreaseLife(){

        if(currentHealth-1>=0){
            currentHealth --;
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth== 0){
             CancelInvoke();
             died=true;
             InvokeRepeating("Die",0f,0.5f);
             animator.SetBool("die",true);
             

            if(attacker!=null){
               
               if(animator!=null){
                   animator.SetBool("toAttack",false);
               }
            }
             
        }
    }

    void OnCollisionExit2D(Collision2D collision){//the attacked soldier is not near the attacker
        collided = false;
        if(collision.collider.tag=="enemy"||collision.collider.tag=="colony"){
            animator.SetBool("toAttack",false);
            CancelInvoke();

        }
    }

}
