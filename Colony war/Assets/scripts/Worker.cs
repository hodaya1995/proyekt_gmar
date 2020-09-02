using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public int health = 7;
    public float miningSpeed = 100.0f/60.0f; //mine speed: amount/second
    Walk walk;
    Animator animator;


    void Start()
    {
        Attacked attacked = this.gameObject.AddComponent<Attacked>();
        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        this.gameObject.AddComponent<Character>();
        animator = this.gameObject.GetComponent<Animator>();
        walk = this.gameObject.AddComponent<Walk>();
     

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

