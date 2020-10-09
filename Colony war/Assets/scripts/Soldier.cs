using UnityEngine;

public class Soldier : MonoBehaviour
{
    public bool enemySoldier;
    public float health = 10;
    public float speed = 20f;
    Walk walk;
    bool stopedWalking;


    void Start()
    {
        this.gameObject.AddComponent<DestructOfBuilding>();
        this.gameObject.AddComponent<Build_Building>();
        this.gameObject.AddComponent<Attack>();

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

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    /// <summary>
    /// when soldier collides with enemy- stop moving to path
    /// </summary>
    /// <param name="collision">enemy's collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != this.tag && (collision.collider.tag.Contains("soldier") || collision.collider.tag.Contains("miner")))
        {
            stopedWalking = true;
            walk.StopMovingToPath(true);

        }


    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.collider.tag != this.tag && (collision.collider.tag.Contains("soldier") || collision.collider.tag.Contains("miner")))
        if(stopedWalking)
        {
          
            walk.RewalkToTarget();
            stopedWalking = false;

        }

    }









}