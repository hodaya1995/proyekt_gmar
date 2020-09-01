using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public int health = 7;
    public int miningSpeed = 10;
    Walk walk;
    Animator animator;


    void Start()
    {
        Attacked attacked = this.gameObject.AddComponent<Attacked>();
        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        this.gameObject.AddComponent<Character>();
        animator = this.gameObject.GetComponent<Animator>();
        walk = this.gameObject.AddComponent<Walk>();
        walk.MoveToResorce();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string res = tag.Split(' ')[0];
        if (collision.collider.tag.Contains(res))
        {
            walk.StopMovingToPath();
            animator.SetBool("mine", true);
        }
    }
}

