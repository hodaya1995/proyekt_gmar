using Pathfinding;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public bool enemySoldier;
    public int health=10;
    public float speed = 20f;
    bool searchForSoldiers;//if true- it will allways search for soldiers to attack 
    bool selectWithTouch;//if true - the player can tocuh it and select other soldier to attack
    Walk walk;

    void Start()
    {
        this.gameObject.AddComponent<Attack>();
        this.gameObject.AddComponent<Seeker>();
        Attacked attacked=this.gameObject.AddComponent<Attacked>();
        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();

       
        if (enemySoldier)
        {
            searchForSoldiers = true;
            selectWithTouch = false;
            attacked.maxHealth = 20;

        }
        else
        {
            searchForSoldiers = false;
            selectWithTouch = true;
            attacked.maxHealth = health;
        }
        walk = this.gameObject.AddComponent<Walk>();

        walk.SetSearch(searchForSoldiers);
        walk.SetTouchable(selectWithTouch);
        walk.speed = speed;

    }





 





}
