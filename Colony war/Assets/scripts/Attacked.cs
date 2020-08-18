using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 5;
    int currentHealth;
    bool attacked = false;
    public HealthBar healthBar;
    GameObject attacker;

    int callDieFunc = 0;
    Color fade;
    public static float velocityX;
    public static float velocityY;
    Rigidbody2D rb;
    Animator attackerAnimator;
    bool dying = false;




    void Start()
    {
        currentHealth = maxHealth;
        callDieFunc = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)//the attacked soldier near the attacker
    {

        if (collision.collider.tag.Contains( "soldier" ))
        {
            attacked = true;
            InvokeRepeating("DecreaseLife", 0f, 0.5f);
            attacker = collision.gameObject;
     
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

    void Update()
    {
        if (attacked)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (attackerAnimator != null)//the attacker died
        {
            if (attackerAnimator.GetBool("die"))
            {
                dying = true;
                CancelInvoke("DecreaseLife"); //stop dying
            }

        }
    }


    void Die()
    {

        callDieFunc--;
        if (callDieFunc >= 0)
        {
            fade = GetComponent<Renderer>().material.color;
            fade.a = fade.a / (1.0f + (1.0f / callDieFunc));
            GetComponent<Renderer>().material.color = fade;

            if (fade.a <= .1)
            {
                int childs = this.transform.childCount;
                for (int i = 0; i < childs; i++)
                {
                    Destroy(this.transform.GetChild(i).gameObject);
                }
                Destroy(this.gameObject);

            }

        }
    }
    void DecreaseLife()
    {

        if (currentHealth - 1 >= 0)
        {
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth == 0)
        {
            attacked = false;
            CancelInvoke("DecreaseLife");
            dying = true;
            InvokeRepeating("Die", 0f, 0.5f);
            animator.SetBool("die", true);


        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {//the attacked soldier is not near the attacker
        if (collision.collider.tag.Contains("soldier"))
        {
           CancelInvoke("DecreaseLife");
        }
    }

}
