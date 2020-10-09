using SevenZip.CommandLineParser;
using UnityEngine;
public class Attack : MonoBehaviour
{
    Animator animator;
    public GameObject attacked;
    public static float velocityX;
    public static float velocityY;
    Animator attackedAnimator;
    public bool attacking;
    bool soldierChosen;
    string targetToAttack;
    bool targetChosen;
    Rigidbody2D rb;
    float waitForSearch = 0.5f;
    bool exitCollider=true;
    bool setAttack;
    float hitPower = 1f;
    
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
  
    }

    public void SetHitPower(float hitPower)
    {
        this.hitPower = hitPower;
    }

    public string GetTarget()
    {
        return this.targetToAttack;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      //the attacked soldier near the attacker- attack only soldiers/workers that are not you
        if (IsColliderTriggered(collision))
        {
            AttackCollision(collision.gameObject);

        }


    }

    public void AttackCollision (GameObject soldierToAttack)
    {
        if(soldierToAttack!=null && GetComponent<Walk>().target != null)
        {
            string desiredTarget = GetComponent<Walk>().target.name;
            if (desiredTarget != soldierToAttack.name)
            {
                GameObject targetedAttacker = soldierToAttack.GetComponent<Attacked>().GetTargetedAttacker();
                if(targetedAttacker != null) targetedAttacker.GetComponent<Walk>().SetSearch(true);
            }
        }
        
        
        exitCollider = false;
        GetComponent<Walk>().SetSearch(false);
        attacked = soldierToAttack;
        bool moving = GetComponent<Walk>().IsMoving();
        bool attacking = animator.GetBool("toAttack");
        if (!attacking)
        {
            attacked = soldierToAttack;
            animator.SetBool("toAttack", true);

            Vector3 dir = (attacked.transform.position-this.transform.position).normalized;
            rb.velocity = new Vector3(0, 0, 0);
            
            float h = Vector2.Dot(dir, Vector2.right);
            float v = Vector2.Dot(dir, Vector2.up);
            animator.SetFloat("horizontal", h);
            animator.SetFloat("vertical", v);
            
            FlipAnimation(dir);
        }


        attackedAnimator = attacked.GetComponent<Animator>();
        attacking = true;
        //GetComponent<Walk>().StopMovingToPath();
    }

    private void Flip()//flip the animation
    {

        GetComponent<Walk>().facingRight = !GetComponent<Walk>().facingRight;
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

    private void FlipAnimation(Vector2 dir)
    {
        
        float horizontal = dir.x;
        if (horizontal > 0 && !GetComponent<Walk>().facingRight)
        {
            Flip();
        }
            
        else if (horizontal < 0 && GetComponent<Walk>().facingRight)
        {
            Flip();
        }
       


    }


    void OnCollisionExit2D(Collision2D collision)
    {
        //the attacked soldier is not near the attacker-dont attack
        if (!setAttack)
        {
           
            if (IsColliderTriggered(collision))

            {
                exitCollider = true;
              
                animator.SetBool("toAttack", false);
                targetChosen = false;
                attacking = false;
                Invoke("Search",waitForSearch);

            }
        }
        

    }
    
    private bool IsColliderTriggered(Collision2D collision)
    {
        bool enemy2colony = (collision.collider.tag.Contains("gold miner colony")) && this.tag.Contains("enemy");
        bool colony2enemy = ((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy"));
        bool collisionTargeted = (collision.gameObject.name.Split(' ')[0] == (targetToAttack)) && targetChosen && this.tag != collision.collider.tag;
        return (enemy2colony || colony2enemy || collisionTargeted);

    }
    public GameObject GetAttacked()
    {
        return attacked;
    }
    public bool ExitCollider()
    {
        return exitCollider;
    }
    void Search()
    {
         GetComponent<Walk>().SetSearch(true);
    }
    public bool TargetIsChosen()
    {
        return this.targetChosen;
    }
    void Update()
    {
        bool moving = GetComponent<Walk>().IsMoving();
        if (attacking && !moving)
        {
            rb.velocity = new Vector3(0, 0, 0);

        }
        if (attackedAnimator != null)//the attacker died
        {
            if (attackedAnimator.GetBool("die"))
            {

                animator.SetBool("toAttack", false);
            }

        }

        if (this.tag.Contains("colony"))
        {
            bool detectedTouch = Input.GetMouseButtonDown(0);
            Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool detectedTouchInZone = Move_out_of_zone.OnZone_Characters(p.x, p.y);

            if (detectedTouch)
            {


                //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Collider2D[] nearSoldiers1 = GetNearSoldiers(mousePos);


                if ((hitInformation.collider != null && nearSoldiers1 != null&& nearSoldiers1.Length > 0) && !soldierChosen)
                {

                    Transform t = nearSoldiers1[0].transform;

                    float dis = Mathf.Infinity;
                    foreach (Collider2D collider in nearSoldiers1)
                    {
                        float currDis = Mathf.Abs(Vector3.Distance(collider.transform.position, mousePos));
                        if (currDis < dis)
                        {
                            dis = currDis;
                            t = collider.transform;
                        }
                    }

                    if (this.tag == t.tag)
                    {


                        soldierChosen = true;

                    }



                }
                else if (soldierChosen)
                {

                    Collider2D[] nearSoldiers2 = GetNearSoldiers(mousePos);

                    if (nearSoldiers2 != null&& nearSoldiers2.Length >0)
                    {

                        targetToAttack = (nearSoldiers2[0].name.Split(' '))[0];
                        targetChosen = true;
                   
                    }
                    else
                    {

                        targetChosen = false;
                    }
                    soldierChosen = false;
                }



            }
        }


       
        if (this.tag.Contains("colony"))
        {
            bool solAttacked = attacked != null;
            bool solAttackedAnimator = attacked != null;
            if (solAttacked && solAttackedAnimator)
            {
                setAttack = true;

                Attack attackCompOfAttacked = attacked.GetComponent<Attack>();
                if (attackCompOfAttacked != null)
                {
                    GameObject attackedOfAttacked = attackCompOfAttacked.GetAttacked();
                    //if (attackCompOfAttacked != null)
                    //{
                    //    AttackCollision(attackedOfAttacked);
                    //}
                }
               
            }
            else
            {
                setAttack = false;
            }

        }
       
       







    }

    public float GetHitPower()
    {
        return hitPower;
    }

    Collider2D[] GetNearSoldiers(Vector3 pos)
    {
        return Physics2D.OverlapCircleAll(pos, 1.5f);
    }



}
