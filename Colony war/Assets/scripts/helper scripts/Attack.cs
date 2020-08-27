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

    void OnCollisionEnter2D(Collision2D collision)
    {
        //the attacked soldier near the attacker- attack only soldiers/workers that are not you
        if ( ((collision.collider.tag != this.tag)&&collision.collider.tag.Contains("soldier") )|| ((collision.collider.tag != this.tag) && collision.collider.tag.Contains("gold miner")))
        {
       
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



    void OnCollisionExit2D(Collision2D collision){
        //the attacked soldier is not near the attacker-dont attack
        if (((collision.collider.tag != this.tag) && collision.collider.tag.Contains("soldier")) || ((collision.collider.tag != this.tag) && collision.collider.tag.Contains("gold miner")))
        {
          
            animator.SetBool("toAttack",false);
           

        }
    }

}
