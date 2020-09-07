using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    public bool enemySoldier;
    public int health = 10;
    public float speed = 20f;
    Walk walk;
    Animator animator;

    void Start()
    {
        this.gameObject.AddComponent<Character>();
        this.gameObject.AddComponent<Attack>();

        animator = this.gameObject.GetComponent<Animator>();
        Attacked attacked = this.gameObject.AddComponent<Attacked>();
        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
      
        if (enemySoldier)
        {

            attacked.maxHealth = 20;

        }
        else
        {

            attacked.maxHealth = health;
        }
        walk = this.gameObject.AddComponent<Walk>();

        walk.SetAutomaticWalking(enemySoldier);
      
        walk.speed = speed;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != this.tag && (collision.collider.tag.Contains("soldier") || collision.collider.tag.Contains("miner")))
        {

            walk.StopMovingToPath();
           
        }


    }











}