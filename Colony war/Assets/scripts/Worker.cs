using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public int health=7;
    public int miningSpeed=10;
    MineResources mine;


    void Start()
    {
        this.gameObject.AddComponent<Seeker>();
        Attacked attacked = this.gameObject.AddComponent<Attacked>();
        attacked.healthBar = this.gameObject.GetComponentInChildren<HealthBar>();
        mine = this.gameObject.AddComponent<MineResources>();
        mine.SetMiningSpeed(miningSpeed);
    }

}
