using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_Enemy : MonoBehaviour
{

    List<bool> Flag = new List<bool>();
    int gold;





    List<GameObject> EnemeyWorkers = new List<GameObject>();
    List<GameObject> Horseman = new List<GameObject>();
    List<GameObject> Axemen = new List<GameObject>();
    List<GameObject> Archers = new List<GameObject>();
    GameObject tower;
    GameObject stable;
    GameObject archerbuilding;
    GameObject workerbuilding;
    int sign_to_what_create = 0;

    bool createworker;

    // Start is called before the first frame update
    void Start()
    {
        createworker = true;
        gold = 200;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(sign_to_what_create);
        for (int i = 0; i < EnemeyWorkers.Count; i++)
        {
            if (EnemeyWorkers[i].activeSelf == false)
            {
                EnemeyWorkers.RemoveAt(i);
            }
        }
        for (int i = 0; i < Archers.Count; i++)
        {
            if (Archers[i] == false)
            {
                Archers.RemoveAt(i);
            }
        }

        for (int i = 0; i < Horseman.Count; i++)
        {
            if (Horseman[i] == false)
            {
                Horseman.RemoveAt(i);
            }

        }
        for (int i = 0; i < Axemen.Count; i++)
        {
            if (Axemen[i] == false)
            {
                Axemen.RemoveAt(i);
            }
        }

        if (archerbuilding == null || archerbuilding.activeSelf == false)
        {
            archerbuilding = null;
        }

        if (tower == null || tower.activeSelf == false)
        {
            tower = null;
        }
        if (stable == null || stable.activeSelf == false)
        {
            stable = null;
        }

        if (workerbuilding == null || workerbuilding.activeSelf == false)
        {
            workerbuilding = null;
        }

        sign_to_what_create = FirstOperationEnemy();

        if (sign_to_what_create == 1)
        {
            if (gold >= 40 && createworker)
            {
                gold = gold - 40;
                Debug.Log(gold);
                GameObject o = GameObject.Find("enemy worker building(Clone)");
                GameObject copyworker = o.GetComponent<Flock>().CreateNewWorker(100, 100.0f / 60.0f);
                copyworker.SetActive(false);
                EnemeyWorkers.Add(copyworker);
                InvokeRepeating("timer1", 0f, 5f);
                Debug.Log("ssss");
                createworker = false;
            }
        }
        else if (sign_to_what_create == 2)
        {

        }
        else if (sign_to_what_create == 3)
        {

        }
        else if (sign_to_what_create == 4)
        {

        }
        else if (sign_to_what_create == 5)
        {

        }
        else if (sign_to_what_create == 6)
        {

        }
        else if (sign_to_what_create == 7)
        {

        }

        else if (sign_to_what_create == 8)
        {

        }

        else if (sign_to_what_create == 9)
        {

        }

        else if (sign_to_what_create == 10)
        {

        }
        else if (sign_to_what_create == 11)
        {

        }


    }


    void timer1()
    {



        EnemeyWorkers[EnemeyWorkers.Count - 1].SetActive(true);
        createworker = true;
        CancelInvoke("timer1");




    }
    public void SetBuilding(GameObject o)
    {




    }

    public void SetGoldEnemyPlayer(int t)
    {
        this.gold = t;
    }

    public void SetInbuilding(bool b)
    {

    }







    int FirstOperationEnemy()
    {

        if (EnemeyWorkers.Count < 5)
        {
            return 1;
        }
        else if (archerbuilding == null || !archerbuilding.activeSelf)
        {
            return 2;
        }
        else if (EnemeyWorkers.Count < 10)
        {
            return 3;
        }
        else if (Archers.Count < 10)
        {
            return 4;
        }

        else if (EnemeyWorkers.Count < 15)
        {
            return 5;
        }

        else if (stable == null || !stable.activeSelf)
        {
            return 6;
        }
        else if (Horseman.Count < 10)
        {
            return 7;
        }
        else if (tower == null || !tower.activeSelf)
        {
            return 8;
        }

        else if (EnemeyWorkers.Count < 20)
        {
            return 9;
        }
        else if (Axemen.Count < 10)
        {
            return 10;
        }

        else
        {
            return 11;
        }
    }
}
