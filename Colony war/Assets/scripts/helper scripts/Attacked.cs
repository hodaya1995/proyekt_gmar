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
   

  
    public void SetHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
        callDieFunc = health;
        healthBar.SetMaxHealth(health);
    }
   
  
    void Start()
    {
        SetHealth(maxHealth);
        
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //the attacked soldier near the attacker
        if (collision.collider.tag.Contains( "soldier" )&& collision.collider.tag!=tag)
        {

            attacked = true;
          

            InvokeRepeating("DecreaseLife", 0f, 0.5f);
            attacker = collision.gameObject;
            Vector2 velocity=attacker.GetComponent<Rigidbody2D>().velocity;
            Vector2 shootingDirection = new Vector2();
            if (Mathf.Abs(Attack.velocityX) > Mathf.Abs(velocity.y))
            {
                shootingDirection = new Vector2(velocity.y, 0);
            }
            else
            {
                shootingDirection = new Vector2(0, velocity.y);
            }
            animator.SetFloat("horizontal", -shootingDirection.x);
            animator.SetFloat("vertical", -shootingDirection.y);

            attackerAnimator = attacker.GetComponent<Animator>();
         



        }



    }

    void Update()
    {
        if (attacked)//dont move
        {
           
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (attackerAnimator != null)
        {
            if (attackerAnimator.GetBool("die"))//the attacker died
            {
                dying = true;
                CancelInvoke("DecreaseLife"); //stop dying
            }

        }
    }


    void Die()
    {
        //fade gradually the object and finally destroy it 
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

        if (currentHealth - 1 >= 0)//not died yet- decrease life
        {
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }
        else if (currentHealth == 0)//this died
        {
            attacked = false;
            CancelInvoke("DecreaseLife");
            dying = true;
            InvokeRepeating("Die", 0f, 0.5f);
            animator.SetBool("die", true);


        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //the attacked soldier is not near the attacker
        if (collision.collider.tag.Contains("soldier") && collision.collider.tag != tag)
        {
        
           CancelInvoke("DecreaseLife");
        }
    }

}
