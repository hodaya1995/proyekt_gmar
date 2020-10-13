using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    bool to;
    GameObject stable;
    bool st;
    GameObject archerbuilding;
    bool ar;
    GameObject workerbuilding;
    bool wo;
    int sign_to_what_create = 0;
    GameObject temp;

    bool createaxe;
    bool createhorse;
    bool createworker;
    bool createarcher;
    bool Inbuilding;
    bool createall;

    // Start is called before the first frame update
    void Start()
    {





        createaxe = true;
        createhorse = true;
        Inbuilding = false;
        createarcher = true;
        createworker = true;
        createall = true;
        gold = 5000;

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < EnemeyWorkers.Count; i++)
        {
            if (EnemeyWorkers[i].activeSelf == false)
            {
                EnemeyWorkers.RemoveAt(i);

            }
        }
        for (int i = 0; i < Archers.Count; i++)
        {
            if (Archers[i].activeSelf == false)
            {

                Archers.RemoveAt(i);
            }
        }

        for (int i = 0; i < Horseman.Count; i++)
        {
            if (Horseman[i].activeSelf == false)
            {
                Horseman.RemoveAt(i);


            }

        }
        for (int i = 0; i < Axemen.Count; i++)
        {
            if (Axemen[i].activeSelf == false)
            {
                Axemen.RemoveAt(i);

            }
        }

        if (archerbuilding == null || !archerbuilding.activeSelf)
        {
            archerbuilding = null;
        }

        if (tower == null || !tower.activeSelf)
        {
            tower = null;
        }
        if (stable == null || !stable.activeSelf)
        {
            stable = null;
        }

        if (workerbuilding == null || !workerbuilding.activeSelf)
        {
            workerbuilding = null;
        }

        sign_to_what_create = FirstOperationEnemy();

        if (sign_to_what_create == 1)
        {
            if (gold >= 40 && createworker)
            {
                gold = gold - 40;

                GameObject o = GameObject.Find("enemy worker building(Clone)");
                GameObject copyworker = o.GetComponent<Flock>().CreateNewWorker(100, 100.0f / 60.0f);
                copyworker.SetActive(false);
                temp = copyworker;
                InvokeRepeating("timer1", 5f, 5f);
                createworker = false;
            }
        }
        else if (sign_to_what_create == 2)
        {
            if (gold >= 170 && !Inbuilding)
            {
                Inbuilding = true;
                this.gold = this.gold - 170;
                GameObject start_building = Instantiate(Camera.main.transform.Find("first stage of the building").gameObject);
                GameObject o = GameObject.Find("characters").transform.Find("enemy soldiers").Find("buildings").gameObject;
                start_building.transform.SetParent(o.transform);
                start_building.transform.position = new Vector3(4, 4, 0);
                start_building.GetComponent<Build_Building>().Set_Name_Of_Building("enemy archer building");
                start_building.GetComponent<Build_Building>().Set_Name_Building_shai("archer building");
                start_building.GetComponent<Build_Building>().Set_Side_Of_Player(false);
                Debug.Log(start_building.GetComponent<Build_Building>());
                start_building.SetActive(true);
            }
        }
        else if (sign_to_what_create == 3)
        {
            if (gold >= 40 && createworker)
            {
                //GameObject o = GameObject.Find("enemy worker building(Clone)");
                GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy worker building");
                GameObject copyworker = o.GetComponent<Flock>().CreateNewWorker(100, 100.0f / 60.0f);
                copyworker.SetActive(false);
                temp = copyworker;
                InvokeRepeating("timer1", 5f, 5f);
                createworker = false;
            }
        }
        else if (sign_to_what_create == 4)
        {
            if (gold >= 50 && createarcher)
            {
                gold = gold - 50;
                //GameObject o = this.transform.Find("enemy archer building").gameObject;
                GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy archer building");
                GameObject copyarcher = o.GetComponent<Flock>().CreateNewSoldier(20f, 20f, 1f);
                copyarcher.SetActive(false);
                temp = copyarcher;
                Invoke("timer3", 5f);
                createarcher = false;
            }
        }
        else if (sign_to_what_create == 5)
        {
            if (gold >= 40 && createworker)
            {
                //GameObject o = GameObject.Find("enemy worker building(Clone)");
                GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy worker building");
                GameObject copyworker = o.GetComponent<Flock>().CreateNewWorker(100, 100.0f / 60.0f);
                copyworker.SetActive(false);
                temp = copyworker;
                InvokeRepeating("timer1", 5f, 5f);
                createworker = false;
            }
        }
        else if (sign_to_what_create == 6)
        {
            if (gold >= 200 && !Inbuilding)
            {
                Inbuilding = true;
                this.gold = this.gold - 200;
                GameObject start_building = Instantiate(Camera.main.transform.Find("first stage of the building").gameObject);
                GameObject o = GameObject.Find("characters").transform.Find("enemy soldiers").Find("buildings").gameObject;
                start_building.transform.SetParent(o.transform);
                start_building.transform.position = new Vector3(2, 2, 0);
                start_building.GetComponent<Build_Building>().Set_Name_Of_Building("enemy stable");
                start_building.GetComponent<Build_Building>().Set_Name_Building_shai("stable");
                start_building.GetComponent<Build_Building>().Set_Side_Of_Player(false);
                start_building.SetActive(true);
            }
        }
        else if (sign_to_what_create == 7)
        {
            if (gold >= 60 && createhorse)
            {
                gold = gold - 60;
                //GameObject o = GameObject.Find("enemy stable1").gameObject;
                GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy stable").gameObject;
                GameObject copyhorse = o.GetComponent<Flock>().CreateNewSoldier(20f, 20f, 1f);
                copyhorse.SetActive(false);
                temp = copyhorse;
                Invoke("timer4", 5f);
                createhorse = false;
            }
        }

        else if (sign_to_what_create == 8)
        {
            if (gold >= 120 && !Inbuilding)
            {
                Inbuilding = true;
                this.gold = this.gold - 120;
                GameObject start_building = Instantiate(Camera.main.transform.Find("first stage of the building").gameObject);
                GameObject o = GameObject.Find("characters").transform.Find("enemy soldiers").Find("buildings").gameObject;
                start_building.transform.SetParent(o.transform);
                start_building.transform.position = new Vector3(3, 3, 0);
                start_building.GetComponent<Build_Building>().Set_Name_Of_Building("enemy protect tower");
                start_building.GetComponent<Build_Building>().Set_Name_Building_shai("protect tower");
                start_building.GetComponent<Build_Building>().Set_Side_Of_Player(false);
                start_building.SetActive(true);
            }
        }

        else if (sign_to_what_create == 9)
        {
            if (gold >= 40 && createworker)
            {
                //GameObject o = GameObject.Find("enemy worker building(Clone)");
                GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy worker building");
                GameObject copyworker = o.GetComponent<Flock>().CreateNewWorker(100, 100.0f / 60.0f);
                copyworker.SetActive(false);
                temp = copyworker;
                Invoke("timer1", 5f);
                createworker = false;
            }
        }

        else if (sign_to_what_create == 10)
        {
            if (gold >= 60 && createaxe)
            {
                gold = gold - 60;
                //GameObject o = GameObject.Find("enemy protect tower1").gameObject;
                GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy protect tower");
                GameObject copyaxe = o.GetComponent<Flock>().CreateNewSoldier(20f, 20f, 1f);
                copyaxe.SetActive(false);
                temp = copyaxe;
                Invoke("timer7", 5f);
                createaxe = false;
            }
        }
        else if (sign_to_what_create == 11)
        {
            float r = Random.Range(1f, 4f);
            if (r == 1)
            {
                if (gold >= 50 && createall)
                {
                    gold = gold - 50;
                    //GameObject o = GameObject.Find("enemy archer building1").gameObject;
                    GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy archer building").gameObject;
                    GameObject copyarcher = o.GetComponent<Flock>().CreateNewSoldier(20f, 20f, 1f);
                    copyarcher.SetActive(false);
                    temp = copyarcher;
                    Invoke("timer3", 5f);
                    createarcher = false;
                }
            }

            else if (r == 2)
            {
                if (gold >= 60 && createall)
                {
                    gold = gold - 60;
                    //GameObject o = GameObject.Find("enemy stable1").gameObject;
                    GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy stable").gameObject;
                    GameObject copyhorse = o.GetComponent<Flock>().CreateNewSoldier(20f, 20f, 1f);
                    copyhorse.SetActive(false);
                    temp = copyhorse;
                    Invoke("timer4", 5f);
                    createhorse = false;
                }
            }

            else if (r == 3)
            {
                if (gold >= 60 && createall)
                {
                    gold = gold - 60;
                    GameObject o = MainThreadOfGame.SearchBuildingInHirarchy(GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject,"enemy protect tower").gameObject;
                    GameObject copyaxe = o.GetComponent<Flock>().CreateNewSoldier(20f, 20f, 1f);
                    copyaxe.SetActive(false);
                    temp = copyaxe;
                    Invoke("timer7", 5f);
                    createaxe = false;
                }
            }
        }


    }


    void timer1()
    {



        temp.SetActive(true);
        EnemeyWorkers.Add(temp);
        createworker = true;
        createall = true;
        temp = null;
        CancelInvoke("timer1");





    }
    public void timer2(GameObject o)
    {
        archerbuilding = o;
        archerbuilding.SetActive(true);

    }

    void timer3()
    {
        temp.SetActive(true);
        Archers.Add(temp);
        createarcher = true;
        createall = true;
        temp = null;
    }

    void timer4()
    {
        temp.SetActive(true);
        Horseman.Add(temp);
        createhorse = true;
        createall = true;
        temp = null;
    }

    public void timer5(GameObject o)
    {
        stable = o;

    }
    public void timer6(GameObject o)
    {
        tower = o;

    }

    void timer7()
    {



        temp.SetActive(true);
        Axemen.Add(temp);
        createaxe = true;
        createall = true;
        temp = null;




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
        this.Inbuilding = b;
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