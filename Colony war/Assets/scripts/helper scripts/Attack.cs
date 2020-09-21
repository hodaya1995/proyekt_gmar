using UnityEngine;
public class Attack : MonoBehaviour
{
    Animator animator;
    GameObject attacked;
    public static float velocityX;
    public static float velocityY;
    Animator attackedAnimator;
    bool attacking;
    bool soldierChosen;
    string targetToAttack;
    bool targetChosen;
    Rigidbody2D rb;
    float waitForSearch = 3f;
    bool exitCollider=true;
    bool setAttack;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

    }

 

    public string GetTarget()
    {
        return this.targetToAttack;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        bool enemy2colony = (collision.collider.tag.Contains("gold miner")) && this.tag.Contains("enemy");
        bool colony2enemy = ((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy"));
        bool collisionTargeted = (collision.gameObject.name.Split(' ')[0] == (targetToAttack)) && targetChosen&&this.tag!= collision.collider.tag;
       
        //the attacked soldier near the attacker- attack only soldiers/workers that are not you
        if (enemy2colony || colony2enemy|| collisionTargeted)
        {
            AttackCollision(collision.gameObject);

        }


    }

    void AttackCollision (GameObject soldierToAttack)
    {
        
        exitCollider = false;
        GetComponent<Walk>().SetSearch(false);
        attacked = soldierToAttack;
        bool moving = GetComponent<Walk>().IsMoving();

        if (!moving)
        {
            animator.SetBool("toAttack", true);

            rb.velocity = new Vector3(0, 0, 0);
            Vector2 dir = new Vector2((-this.transform.position.x + attacked.transform.position.x), (-this.transform.position.y + attacked.transform.position.y)).normalized;
            float h = dir.x;
            float v = dir.y;
            animator.SetFloat("horizontal", h);
            animator.SetFloat("vertical", v);
        }
       

        attackedAnimator = attacked.GetComponent<Animator>();
        attacking = true;
        GetComponent<Walk>().StopMovingToPath();
    }

  
    void OnCollisionExit2D(Collision2D collision)
    {
        //the attacked soldier is not near the attacker-dont attack
        if (!setAttack)
        {
            bool enemy2colony = (collision.collider.tag.Contains("gold miner")) && this.tag.Contains("enemy");
            bool colony2enemy = ((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy"));
            bool collisionTargeted = (collision.gameObject.name.Split(' ')[0] == (targetToAttack)) && targetChosen && this.tag != collision.collider.tag;
            if (enemy2colony || colony2enemy || collisionTargeted)

            {
                exitCollider = true;
              
                animator.SetBool("toAttack", false);
                targetChosen = false;
                attacking = false;

                Invoke("Search",waitForSearch);

            }
        }
        

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
                GameObject attackedOfAttacked = attackCompOfAttacked.GetAttacked();
                if (attackCompOfAttacked != null) ;
                {
                    AttackCollision(attackedOfAttacked);
                }
            }
            else
            {
                setAttack = false;
            }

        }
       
       







    }

    Collider2D[] GetNearSoldiers(Vector3 pos)
    {
        return Physics2D.OverlapCircleAll(pos, 1.5f);
    }



}
