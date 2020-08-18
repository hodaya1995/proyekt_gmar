using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    Animator animator;
    GameObject attacker;
    public static float velocityX;
    public static float velocityY;
    Animator attackerAnimator;


    void Start()
    {
        animator = this.GetComponent<Animator>();

    }

    void OnCollisionEnter2D(Collision2D collision)//the attacked soldier near the attacker
    {
       
        if ((collision.collider.tag== "enemy soldier" && this.tag == "colony soldier") || (collision.collider.tag == "colony soldier" && this.tag== "enemy soldier")){
            attacker = collision.gameObject;
            animator.SetBool("toAttack", true);
            attackerAnimator = attacker.GetComponent<Animator>();




        }


    }

    void Update(){
       
        if (attackerAnimator!=null)//the attacker died
        {
            if (attackerAnimator.GetBool("die"))
            {
              
                animator.SetBool("toAttack", false);
            }
               
        }
    }



    void OnCollisionExit2D(Collision2D collision){//the attacked soldier is not near the attacker
        if((collision.collider.tag== "enemy soldier" && this.tag == "colony soldier" )|| (collision.collider.tag== "colony soldier" && this.tag == "enemy soldier"))
        {
            animator.SetBool("toAttack",false);
        
        }
    }

}
