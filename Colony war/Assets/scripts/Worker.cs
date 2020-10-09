//using UnityEngine;

//public class Worker : MonoBehaviour
//{
//    public int health = 7;
//    public float miningSpeed = 100.0f / 60.0f; //mine speed: amount/second
//    Walk walk;
//    Animator animator;


//    void Start()
//    {
//        Attacked attacked = this.gameObject.AddComponent<Attacked>();
//        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
//        animator = this.gameObject.GetComponent<Animator>();
//        walk = this.gameObject.AddComponent<Walk>();
//        this.gameObject.AddComponent<DestructOfBuilding>();
//        this.gameObject.AddComponent<Build_Building>();


//    }
//    /// <summary>
//    /// when collides with resource- start to mine.
//    /// </summary>
//    /// <param name="collision"> resource's collison </param>
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        string res = tag.Split(' ')[0];

//        if (collision.collider.tag.Contains(res))
//        {
//            walk.StopMovingToPath(false);
//            animator.SetBool("mine", true);
//        }
//    }
//}

using UnityEngine;

public class Worker : MonoBehaviour
{
    public int health = 7;
    public float miningSpeed = 100.0f / 1.0f; //mine speed: amount/second
    Walk walk;
    Animator animator;
    bool stopedWalking;


    void Start()
    {
        Attacked attacked = this.gameObject.AddComponent<Attacked>();
        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        animator = this.gameObject.GetComponent<Animator>();
        walk = this.gameObject.AddComponent<Walk>();
    }
    /// <summary>
    /// when collides with resource- start to mine.
    /// </summary>
    /// <param name="collision"> resource's collison </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string res = "gold";

        if (collision.collider.tag == res || collision.collider.tag == "first stage of the building")
        {

            this.gameObject.GetComponent<Animator>().SetBool("mine", true);
            stopedWalking = true;
            if (walk != null) walk.StopMovingToPath(true);
            Debug.Log("OnCollisionEnter2D " + collision.collider.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (stopedWalking)
        {

            if (walk != null)

            {

                Debug.Log("OnCollisionExit2D worker " + collision.collider.name);
                this.gameObject.GetComponent<Animator>().SetBool("mine", false);

            }
            stopedWalking = false;

        }

    }

}


