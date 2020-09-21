using System.Collections.Generic;
using UnityEngine;

public class CreateColony : MonoBehaviour
{

    void Start()
    {
        
        
        //CreateSoldier("axe soldier", 10f, 10, new Vector3(0, 0, 0));
        //CreateWorker("gold miner", 10, 10, new Vector3(5,1, 0));

    }

    void Update()
    {

    }

    void CreateSoldier(string name, float speed, int life, Vector3 pos)
    {
        string tag = "colony soldier";
        GameObject[] allObj = (GameObject[])FindObjectsOfTypeAll(typeof(GameObject));
        GameObject[] obj = FindGameObjectWithTag(allObj, tag);
        GameObject soldier = null;
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
            GameObject copiedSoldier = Instantiate(soldier, pos, rot) as GameObject;
            copiedSoldier.SetActive(true);
            copiedSoldier.GetComponent<Soldier>().health = life;
            copiedSoldier.GetComponent<Soldier>().speed = speed;
            Rigidbody2D rb = copiedSoldier.GetComponent<Rigidbody2D>();
            rb.drag = 1.5f;

        }

    }

    private GameObject[] FindGameObjectWithTag(GameObject[] allObj, string tag)
    {
        List<GameObject> res = new List<GameObject>();
        foreach (GameObject obj in allObj)
        {
            if (obj.tag == tag)
            {
                res.Add(obj);
            }
        }
        return (GameObject[])res.ToArray();
    }

    void CreateWorker(string tag, int speedMining, int life, Vector3 pos)
    {
        GameObject[] allObj = (GameObject[])FindObjectsOfTypeAll(typeof(GameObject));
        GameObject[] obj = FindGameObjectWithTag(allObj, tag);

        GameObject worker = null;

        if (obj.Length > 0)
        {
            worker = (GameObject)obj[0];
        }
        if (worker == null)
        {
            Debug.LogError("no such game object " + tag);
        }
        else
        {
            Quaternion rot = new Quaternion();
            GameObject copiedWorker = Instantiate(worker, pos, rot) as GameObject;
            copiedWorker.SetActive(true);
            copiedWorker.GetComponent<Attacked>().maxHealth = life;
            copiedWorker.GetComponent<MineResources>().speedMining = speedMining;
            Rigidbody2D rb = copiedWorker.GetComponent<Rigidbody2D>();
            rb.drag = 1.5f;

        }
    }



}
