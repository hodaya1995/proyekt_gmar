using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateColony : MonoBehaviour
{
    public static int totalSoldier = 0, totalArcher = 0, totalHorse = 0, totalWorker = 0;
    GameObject copiedSoldier;


    void Start()
    {

    }

    void Update()
    {
    }

    void CreateSoldier(string name, string buildingName, float speed, float life, Vector3 pos)
    {
        string tag = "colony soldier";
        GameObject[] allObj = (GameObject[])FindObjectsOfTypeAll(typeof(GameObject));
        GameObject[] obj = FindGameObjectWithTag(allObj, tag);
        GameObject soldier = null;
        GameObject soldierBuilding = null;

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
            foreach (GameObject sol in allObj)
            {
                if (sol.name == buildingName)
                {
                    soldierBuilding = sol;
                    break;
                }
            }

            int dis = 0;
            if (name == "horse soldier") dis = totalHorse;
            else dis = totalArcher;

            Vector3 newSoldierPos = new Vector3(soldierBuilding.transform.position.x,
                soldierBuilding.transform.position.y - soldierBuilding.transform.localScale.y - 0.5f, soldierBuilding.transform.position.z);

            foreach (GameObject p in obj)
            {
                if (p.transform.position == newSoldierPos)
                {
                    if (dis % 2 == 0)
                    {
                        newSoldierPos = new Vector3(soldierBuilding.transform.position.x + dis / 2,
                            soldierBuilding.transform.position.y - soldierBuilding.transform.localScale.y - 0.5f, soldierBuilding.transform.position.z);
                    }
                    else
                    {
                        newSoldierPos = new Vector3(soldierBuilding.transform.position.x - dis / 2,
                            soldierBuilding.transform.position.y - soldierBuilding.transform.localScale.y - 0.5f, soldierBuilding.transform.position.z);
                    }

                    break;
                }
            }

            Quaternion rot = new Quaternion();
            //GameObject copiedSoldier = Instantiate(soldier, newSoldierPos, rot) as GameObject;
            copiedSoldier = Instantiate(soldier, newSoldierPos, rot) as GameObject;
            copiedSoldier.SetActive(false);
            //copiedSoldier.SetActive(true);
            copiedSoldier.GetComponent<Soldier>().health = life;
            copiedSoldier.GetComponent<Soldier>().speed = speed;
            Rigidbody2D rb = copiedSoldier.GetComponent<Rigidbody2D>();
            rb.drag = 1.5f;
            Invoke("TimerBarHelper", 5f);

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


    public void CreateAnArcher()
    {
        /*
        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;

        GameObject text = child.transform.Find("Text").gameObject;
        Text context = text.GetComponent<Text>();

        int currGold = int.Parse(context.text);
        if (currGold >= 50)
        {
        */
            totalArcher++;
            totalSoldier++;
        
            CreateSoldier("archer soldier", "archer building", 10f, 10, new Vector3(0, 0, 0));
        /*
            currGold -= 50;
            context.text = "" + currGold;
        }
        */
    }


    public void CreateAnWorker()
    {
        totalWorker++;
        totalSoldier++;
        CreateWorker("gold miner", 10, 10, new Vector3(5, 1, 0));
    }

    public void CreateAnKnight()
    {
        totalHorse++;
        totalSoldier++;
        CreateSoldier("horse soldier", "stable building", 10, 10, new Vector3(5, 1, 0));
    }

    void TimerBarHelper()
    {
        copiedSoldier.SetActive(true);
    }


}
