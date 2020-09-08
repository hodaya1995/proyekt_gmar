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
    int attackers;
    bool facingRight;
    float waitForSearch = 5f;
    Collision2D collision;
    bool decreaseLifeMehodCalled;

    public int GetAttackersNum()
    {
        return attackers;
    }

    public void AddAttacker()
    {
        this.attackers++;
    }

    public void SetHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
        callDieFunc = health;
        healthBar.SetMaxHealth(health);
    }

    private void Flip(Transform transform)//flip the animation
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FlipAnimation(Animator myAnimator, Transform transform)
    {
        float h = myAnimator.GetFloat("horizontal");
        if (h > 0 && !facingRight)
            Flip(transform);
        else if (h < 0 && facingRight)
            Flip(transform);
    }

    void Start()
    {
        SetHealth(maxHealth);

        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }


    void Search()
    {
        GetComponent<Walk>().SetSearch(true);
    }

    void OnColliderEnter(Collision2D collision)
    {
        bool targeted = false;
        if (collision.gameObject.GetComponent<Attack>() != null)
        {
            targeted = (collision.collider.tag.Contains("colony") && tag.Contains("enemy")) &&
           collision.gameObject.GetComponent<Attack>().GetTarget() == this.gameObject && collision.gameObject.GetComponent<Attack>().TargetIsChosen();
        }
        if ((collision.collider.tag.Contains("enemy") && collision.collider.tag != tag) || targeted)
        {

            GetComponent<Walk>().SetSearch(false);
            if (!decreaseLifeMehodCalled)
            {
                InvokeRepeating("DecreaseLife", 0f, 0.5f);
            }
            decreaseLifeMehodCalled = true;

            attacker = collision.gameObject;
            attackerAnimator = attacker.GetComponent<Animator>();
            attacked = true;



        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        this.collision = collision;
        OnColliderEnter(collision);
        //the attacked soldier near the attacker




    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!attacked)
        {
            this.collision = collision;

            OnColliderEnter(collision);

        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {



        bool targeted = false;
        if (collision.gameObject.GetComponent<Attack>() != null)
        {
            targeted = (collision.collider.tag.Contains("colony") && tag.Contains("enemy")) &&
           collision.gameObject.GetComponent<Attack>().GetTarget() == this.gameObject && collision.gameObject.GetComponent<Attack>().TargetIsChosen();
        }

        //the attacked soldier is not near the attacker
        if ((collision.collider.tag.Contains("enemy") && collision.collider.tag != tag) || targeted)
        {
            OnColliderExit();

        }
    }


    void OnColliderExit()
    {
        CancelInvoke("DecreaseLife");
        attackers--;
        attacked = false;
        decreaseLifeMehodCalled = false;
        Invoke("Search", waitForSearch);
    }

    void Update()
    {
        bool moving = GetComponent<Walk>().IsMoving();
        Attack attack = GetComponent<Attack>();
        if (attack != null)
        {
            bool exitFromCollider = GetComponent<Attack>().ExitCollider();
            if (exitFromCollider)
            {
                OnColliderExit();
            }
            else
            {
                if (collision != null)
                    OnColliderEnter(collision);
            }
        }


        if (attacked && !moving)//dont move
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
    public bool IsAttacked()
    {
        return attacked;
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
    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    void DecreaseLife()
    {

        if (currentHealth - 1 > 0)//not died yet- decrease life
        {
            currentHealth--;
            healthBar.SetHealth(currentHealth);
        }
        else //this died
        {
            attacked = false;
            CancelInvoke("DecreaseLife");
            dying = true;
            InvokeRepeating("Die", 0f, 0.5f);
            animator.SetBool("die", true);


        }
    }



}
