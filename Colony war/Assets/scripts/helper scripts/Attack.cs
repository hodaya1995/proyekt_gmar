using System;
using System.Collections;
using System.Collections.Generic;
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
    GameObject targetToAttack;
    bool targetChosen;
    Rigidbody2D rb;
    float waitForSearch = 5f;
    bool exitCollider;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        //the attacked soldier near the attacker- attack only soldiers/workers that are not you
        if (((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy")) ||
        (collision.collider.tag.Contains("gold miner")) && this.tag.Contains("enemy") ||
            (targetToAttack == collision.gameObject) && targetChosen)
        {
            exitCollider = false;
            GetComponent<Walk>().SetSearch(false);
            attacked = collision.gameObject;
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


    }


    public GameObject GetTarget()
    {
        return this.targetToAttack;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        attacked = collision.gameObject;

        bool moving = GetComponent<Walk>().IsMoving();

        if ((targetToAttack == collision.gameObject) && targetChosen)


        {
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

            attacked = collision.gameObject;
            attackedAnimator = attacked.GetComponent<Animator>();
            attacking = true;
            GetComponent<Walk>().StopMovingToPath();
        }

        if (moving)
        {
            animator.SetBool("toAttack", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //the attacked soldier is not near the attacker-dont attack
        if (((collision.collider.tag.Contains("colony")) && this.tag.Contains("enemy")) ||
            (collision.collider.tag.Contains("gold miner")) && this.tag.Contains("enemy") ||
                (targetToAttack != null) && targetChosen)

        {
            exitCollider = true;

            animator.SetBool("toAttack", false);
            targetChosen = false;
            attacking = false;

            Invoke("Search", waitForSearch);

        }

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


                if (hitInformation.collider != null && !soldierChosen)
                {



                    if (this.tag == hitInformation.collider.tag)
                    {


                        soldierChosen = true;

                    }



                }
                else if (soldierChosen)
                {




                    if (hitInformation.collider != null)
                    {

                        targetToAttack = hitInformation.collider.gameObject;
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












    }





}
