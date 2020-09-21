using UnityEngine;

public class Attacked : MonoBehaviour
{
    Animator animator;
    public int maxHealth = 5;
    int currentHealth;
    bool attacked = false;
    public HealthBar healthBar;
    GameObject attacker;

    Rigidbody2D rb;
    Animator attackerAnimator;
    int attackers;
    float waitForSearch = 3f;
    bool decreaseLifeMehodCalled;
    bool targeted;
    bool moving;

    public void SetTargeted(bool targeted)
    {
        this.targeted = targeted;
    }

    public bool IsTargeted()
    {
        return targeted;
    }
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
        healthBar.SetMaxHealth(health);
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
      

        bool enemy2colony = (collision.collider.tag.Contains("gold miner")) && this.tag.Contains("enemy");
        bool colony2enemy = ((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy"));
        bool collisionTargeted = (collision.collider.tag.Contains("enemy") && collision.collider.tag != tag);
           
        if (enemy2colony || colony2enemy || collisionTargeted)
        {
            attacked = true;

            GetComponent<Walk>().SetSearch(false);
            if (!decreaseLifeMehodCalled)
            {
                InvokeRepeating("DecreaseLife", 0f, 0.5f);
            }
            decreaseLifeMehodCalled = true;
            attacker = collision.gameObject;
            attackerAnimator = attacker.GetComponent<Animator>();
            



        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        OnColliderEnter(collision);
        //the attacked soldier near the attacker

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!attacked)
        {
       
            OnColliderEnter(collision);

        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {


        bool enemy2colony = (collision.collider.tag.Contains("gold miner")) && this.tag.Contains("enemy");
        bool colony2enemy = ((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy"));
        bool collisionTargeted = (collision.collider.tag.Contains("enemy") && collision.collider.tag != tag);
        if (enemy2colony || colony2enemy || collisionTargeted)
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
        moving = GetComponent<Walk>().IsMoving();
   
        if (attacked && !moving)//dont move
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

        if (attackerAnimator != null)
        {
            if (attackerAnimator.GetBool("die"))//the attacker died
            {
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
        Destroy(this.gameObject);
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
            animator.SetBool("die", true);

            Invoke("Die",2.3f);
           

        }
    }



}
