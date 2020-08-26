using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateColony : MonoBehaviour
{
    
    void Start()
    {
       
    }

    void Update()
    {
        
    }

    void CreateSoldier(string name,float speed,int life, Vector3 pos)
    {
        GameObject[] obj= GameObject.FindGameObjectsWithTag("colony soldier");
        GameObject soldier=null;
        foreach (GameObject sol in obj)
        {
           
            if (sol.name == name)
            {
                soldier = sol;
                break;
            }
        }
        if (soldier == null)
        {
            Debug.LogError("no such game object " + name);
        }
        else
        {
            Quaternion rot = new Quaternion();
            GameObject copiedSoldier = Instantiate(soldier,pos,rot);
            copiedSoldier.GetComponent<Attacked>().maxHealth= life;
            //copiedSoldier.GetComponent<Walk>().speed = speed;
            Rigidbody2D rb = copiedSoldier.GetComponent<Rigidbody2D>();
            rb.drag = 1.5f;
         
        }
        
    }

    void CreateWorker(string tag, int life, Vector3 pos,int speedMining)
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag(tag);
        GameObject worker = null;
        if (obj != null)
        {
            worker = obj[0];
        }
        if (worker == null)
        {
            Debug.LogError("no such game object " + tag);
        }
        else
        {
            Quaternion rot = new Quaternion();
            GameObject copiedWorker = Instantiate(worker, pos, rot);
            copiedWorker.GetComponent<Attacked>().maxHealth = life;
            copiedWorker.GetComponent<MineResources>().speedMining = speedMining;
            Rigidbody2D rb = copiedWorker.GetComponent<Rigidbody2D>();
            rb.drag = 1.5f;

        }
    }



}
