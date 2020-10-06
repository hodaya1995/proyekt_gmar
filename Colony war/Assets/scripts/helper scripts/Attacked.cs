using UnityEngine;

public class Attacked : MonoBehaviour
{
    Animator animator;
    public float maxHealth = 5;
    float currentHealth;
    public bool attacked = false;
    public HealthBar healthBar;
    public GameObject attacker;

    Rigidbody2D rb;
    Animator attackerAnimator;
    int attackers;
    float waitForSearch = 3f;
    bool decreaseLifeMehodCalled;
    bool targeted;
    bool moving;
    float lifeToDecrease;
    GameObject targetedAttacker;

    public void SetTargetedAttacker(GameObject targetedAttacker)
    {
        this.targetedAttacker = targetedAttacker;
    }

    public GameObject GetTargetedAttacker()
    {
        return this.targetedAttacker;
    }
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

  
    public void SetHealth(float health)
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
        if (IsColliderTriggered(collision))
        {


            attacked = true;
            attackers++;
            lifeToDecrease += collision.gameObject.GetComponent<Attack>().GetHitPower();
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
        if (!attacked)
        {
            OnColliderEnter(collision);
        }
        
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
        if (IsColliderTriggered(collision))
        {
           OnColliderExit(collision.gameObject);

        }
    }

    private bool IsColliderTriggered(Collision2D collision)
    {
        //bool enemy2colony = false;
        //bool colony2enemy = false;
        //if (collision.gameObject.GetComponent<Attack>() != null)
        //{
        //    string taregt = collision.gameObject.GetComponent<Attack>().GetTarget();
        //    enemy2colony = (collision.collider.tag == "gold miner") && this.tag == "enemy soldier" && taregt == this.name.Split(' ')[0] && collision.gameObject.GetComponent<Animator>().GetBool("toAttack") && collision.gameObject.GetComponent<Walk>().target.name == this.name;
        //    colony2enemy = ((collision.collider.tag == "colony soldier") && this.tag == "enemy soldier") && taregt == this.name.Split(' ')[0] && collision.gameObject.GetComponent<Animator>().GetBool("toAttack");
        //}

        //bool collisionTargeted = (collision.collider.tag == "enemy soldier" && collision.collider.tag != tag)&&collision.gameObject.GetComponent<Animator>().GetBool("toAttack") && collision.gameObject.GetComponent<Walk>().target.name == this.name;
        //return ((enemy2colony || colony2enemy || collisionTargeted) );

        if (collision.gameObject.GetComponent<Attack>() != null)
        {
            if (collision.gameObject.GetComponent<Animator>().GetBool("toAttack") && collision.gameObject.GetComponent<Attack>().attacked != null)
            {
                if (collision.gameObject.GetComponent<Attack>().attacked.name == this.name) return true;
            }
            //string taregt = collision.gameObject.GetComponent<Attack>().get
           // enemy2colony = (collision.collider.tag == "gold miner") && this.tag == "enemy soldier" && taregt == this.name.Split(' ')[0] && collision.gameObject.GetComponent<Animator>().GetBool("toAttack") && collision.gameObject.GetComponent<Walk>().target.name == this.name;
            //colony2enemy = ((collision.collider.tag == "colony soldier") && this.tag == "enemy soldier") && taregt == this.name.Split(' ')[0] && collision.gameObject.GetComponent<Animator>().GetBool("toAttack");
        }
        return false;
    }
    void OnColliderExit(GameObject obj)
    {
        attacked = false;
        CancelInvoke("DecreaseLife");
        lifeToDecrease -= obj.GetComponent<Attack>().GetHitPower();
        attackers--;      
        decreaseLifeMehodCalled = false;
        Invoke("Search", waitForSearch);
    }

    void Update()
    {
        moving = GetComponent<Walk>().IsMoving();
   
        //if (attacked && !moving)//dont move
        //{
        //    rb.velocity = new Vector3(0, 0, 0);
        //}

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
    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
    void DecreaseLife()
    {
        if (!attackerAnimator.GetBool("toAttack"))
        {
           attacker.GetComponent<Attack>().AttackCollision(this.gameObject);
        }
        float hitPower = lifeToDecrease;//attacker.GetComponent<Attack>().GetHitPower();
        if (currentHealth - hitPower > 0)//not died yet- decrease life
        {
            currentHealth-= hitPower;
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
