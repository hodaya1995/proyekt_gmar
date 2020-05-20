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

    int callDieFunc=5;
    Color fade;
    public static float velocityX;
    public static float velocityY;
   
       void Start()
    {
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

     void OnCollisionEnter2D(Collision2D collision)//the attacked soldier near the attacker
    {
        if(collision.collider.tag=="enemy"||collision.collider.tag=="colony"){
            InvokeRepeating("DecreaseLife",0f,0.5f);
            attacker=collision.gameObject;
            Animator attackerAnimator=attacker.GetComponent<Animator>();
            velocityX=collision.relativeVelocity.x;
            velocityY=collision.relativeVelocity.y;
            if(Mathf.Abs(velocityX)>Mathf.Abs(velocityY) ){
                attackerAnimator.SetFloat("horizontal",velocityX);
                attackerAnimator.SetFloat("vertical",0);
            }else{
                attackerAnimator.SetFloat("horizontal",0);
                attackerAnimator.SetFloat("vertical",velocityY);
            }
            


        }
        
        
    }

    void Update(){
        
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
                    Destroy (this.transform.parent.gameObject);
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
        else{
            CancelInvoke();
            died=true;
             InvokeRepeating("Die",0f,0.5f);
             Debug.Log("die ");
            animator.SetTrigger("die");

            if(attacker!=null){
               Animator animator= attacker.GetComponent<Animator>();
               if(animator!=null){
                   animator.SetBool("toAttack",false);
               }
            }
             
        }
    }

    void OnCollisionExit2D(Collision2D collision){//the attacked soldier not near the attacker
        if(collision.collider.tag=="enemy"||collision.collider.tag=="colony"){
        }
    }

}
