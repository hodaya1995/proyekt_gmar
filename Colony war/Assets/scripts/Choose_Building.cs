


using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Choose_Building : MonoBehaviour
{



    Vector3 Point_Tool_Buildings;
    GameObject Tool_Buildings;
    bool Choose_Worker;
    GameObject Move_Building_To_Build;
    bool Use_Move_Building_To_Build;
    GameObject start_building;
    bool No_Other_Build_In_Field = false;
    GameObject worker = null;
    bool Sturctue_Mine;

    

    void Start()
    {

        Point_Tool_Buildings = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Point_Tool_Buildings.z = Camera.main.transform.position.z + 5;
        Tool_Buildings = Camera.main.gameObject.transform.Find("Canvas2").gameObject.transform.Find("bar of buildings").gameObject;
        Tool_Buildings.transform.position = Point_Tool_Buildings;
        Tool_Buildings.gameObject.SetActive(false);
        Camera.main.transform.Find("stable move").gameObject.SetActive(false);
        Camera.main.transform.Find("barracks move").gameObject.SetActive(false);
        Camera.main.transform.Find("archer building move").gameObject.SetActive(false);
        Camera.main.transform.Find("worker building move").gameObject.SetActive(false);
        Camera.main.transform.Find("protect tower move").gameObject.SetActive(false);
        Camera.main.transform.Find("first stage of the building").gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        Point_Tool_Buildings = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Point_Tool_Buildings.z = Camera.main.transform.position.z + 5;
        Tool_Buildings.transform.position = Point_Tool_Buildings;

        bool detectedTouch = Input.GetMouseButtonDown(0);

        if (detectedTouch)
        {
            if (Use_Move_Building_To_Build && No_Other_Build_In_Field)
            {
                No_Other_Build_In_Field = false;
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                GameObject child = Camera.main.transform.Find("Canvas").gameObject;
                child = child.transform.Find("fill of the bar").gameObject;
                GameObject text = child.transform.Find("Text").gameObject;
                Text countxet = text.GetComponent<Text>();
                string building = "";
                int numberOfResource = 0;
                if (Move_Building_To_Build.name == "stable move")
                {
                    int num = int.Parse(countxet.text) - 200;
                    countxet.text = " " + num;
                    numberOfResource = num;
                    building = "stable";
                }

                else if (Move_Building_To_Build.name == "barracks move")
                {
                    int num = int.Parse(countxet.text) - 150;
                    countxet.text = " " + num;
                    numberOfResource = num;
                    building = "barracks";
                }

                else if (Move_Building_To_Build.name == "archer building move")
                {
                    int num = int.Parse(countxet.text) - 170;
                    countxet.text = " " + num;
                    numberOfResource = num;
                    building = "archer building";
                }

                else if (Move_Building_To_Build.name == "worker building move")
                {
                    int num = int.Parse(countxet.text) - 120;
                    countxet.text = " " + num;
                    numberOfResource = num;
                    building = "worker building";
                }

                else if (Move_Building_To_Build.name == "protect tower move")
                {
                    int num = int.Parse(countxet.text) - 120;
                    countxet.text = " " + num;
                    numberOfResource = num;
                    building = "protect tower";

                }

                Move_Building_To_Build.SetActive(false);
                start_building = Instantiate(Camera.main.transform.Find("first stage of the building").gameObject);
                if (Sturctue_Mine)
                {
                    GameObject o = GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject;
                    start_building.transform.SetParent(o.transform);
                }
                else if (!Sturctue_Mine)
                {
                    GameObject o = GameObject.Find("characters").transform.Find("enemy soldiers").Find("buildings").gameObject;
                    start_building.transform.SetParent(o.transform);
                }


                start_building.transform.position = new Vector3(Move_Building_To_Build.transform.position.x, Move_Building_To_Build.transform.position.y, Move_Building_To_Build.transform.position.z);
                start_building.GetComponent<Build_Building>().Set_Name_Of_Building(building);
                start_building.GetComponent<Build_Building>().Set_Side_Of_Player(Sturctue_Mine);
                start_building.SetActive(true);


                Resources.gold = numberOfResource;


            }



            Use_Move_Building_To_Build = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hitInformation.collider != null)
            {

                if (hitInformation.collider.gameObject.GetComponent<Worker>() != null && hitInformation.collider.gameObject.transform.parent.parent.parent.gameObject.name == "colony soldiers")
                {
                    


                        Sturctue_Mine = true;
                        worker = hitInformation.collider.gameObject;
                        worker.GetComponent<Walk>().Set_If_Butoon_Collide_On_Tool_Building(false);

                        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
                        child = child.transform.Find("fill of the bar").gameObject;
                        GameObject text = child.transform.Find("Text").gameObject;
                        Text countxet = text.GetComponent<Text>();
                        int number = int.Parse(countxet.text);


                        Tool_Buildings.gameObject.SetActive(true);
                        SpriteRenderer temp1 = Tool_Buildings.transform.Find("icon of stable").gameObject.GetComponent<SpriteRenderer>();
                        temp1.color = new Color(255, 0, 0);
                        SpriteRenderer temp2 = Tool_Buildings.transform.Find("icon of barracks").gameObject.GetComponent<SpriteRenderer>();
                        temp2.color = new Color(255, 0, 0);
                        SpriteRenderer temp3 = Tool_Buildings.transform.Find("icon of archers building").gameObject.GetComponent<SpriteRenderer>();
                        temp3.color = new Color(255, 0, 0);
                        SpriteRenderer temp4 = Tool_Buildings.transform.Find("icon of workers building").gameObject.GetComponent<SpriteRenderer>();
                        temp4.color = new Color(255, 0, 0);
                        SpriteRenderer temp5 = Tool_Buildings.transform.Find("icon of protecting tower").gameObject.GetComponent<SpriteRenderer>();
                        temp5.color = new Color(255, 0, 0);

                        if (number >= 120)
                        {
                            temp4.color = new Color(255, 255, 255);
                            temp5.color = new Color(255, 255, 255);
                        }

                        if (number >= 150)
                        {
                            temp2.color = new Color(255, 255, 255);
                            temp4.color = new Color(255, 255, 255);
                            temp5.color = new Color(255, 255, 255);
                        }

                        if (number >= 170)
                        {
                            temp2.color = new Color(255, 255, 255);
                            temp3.color = new Color(255, 255, 255);
                            temp4.color = new Color(255, 255, 255);
                            temp5.color = new Color(255, 255, 255);
                        }
                        if (number >= 200)
                        {
                            temp1.color = new Color(255, 255, 255);
                            temp2.color = new Color(255, 255, 255);
                            temp3.color = new Color(255, 255, 255);
                            temp4.color = new Color(255, 255, 255);
                            temp5.color = new Color(255, 255, 255);
                        }

                    }


                    else if (hitInformation.collider.gameObject.transform.parent.gameObject.name == "bar of buildings" && hitInformation.collider.gameObject.name != "collider black"
                        && hitInformation.collider.gameObject.GetComponent<SpriteRenderer>() != null
                        && !(hitInformation.collider.tag == "colony gold miner" && Sturctue_Mine))//hodaya
                    {
                        worker.GetComponent<Walk>().Set_If_Butoon_Collide_On_Tool_Building(true);
                        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
                        child = child.transform.Find("fill of the bar").gameObject;
                        GameObject text = child.transform.Find("Text").gameObject;
                        Text countxet = text.GetComponent<Text>();
                        int number = int.Parse(countxet.text);

                        if (hitInformation.collider.gameObject.name == "icon of stable" && number >= 200)
                        {
                            Move_Building_To_Build = Camera.main.transform.Find("stable move").gameObject;
                            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                            Move_Building_To_Build.SetActive(true);
                            Use_Move_Building_To_Build = true;
                        }
                        else if (hitInformation.collider.gameObject.name == "icon of barracks" && number >= 150)
                        {
                            Move_Building_To_Build = Camera.main.transform.Find("barracks move").gameObject;
                            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                            Move_Building_To_Build.SetActive(true);
                            Use_Move_Building_To_Build = true;
                        }
                        else if (hitInformation.collider.gameObject.name == "icon of archers building" && number >= 170)
                        {
                            Move_Building_To_Build = Camera.main.transform.Find("archer building move").gameObject;
                            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                            Move_Building_To_Build.SetActive(true);
                            Use_Move_Building_To_Build = true;
                        }
                        else if (hitInformation.collider.gameObject.name == "icon of workers building" && number >= 120)
                        {
                            Move_Building_To_Build = Camera.main.transform.Find("worker building move").gameObject;
                            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                            Move_Building_To_Build.SetActive(true);
                            Use_Move_Building_To_Build = true;
                        }
                        else if (hitInformation.collider.gameObject.name == "icon of protecting tower" && number >= 120)
                        {
                            Move_Building_To_Build = Camera.main.transform.Find("protect tower move").gameObject;
                            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                            Move_Building_To_Build.SetActive(true);
                            Use_Move_Building_To_Build = true;

                        }






                    }

                    else if (hitInformation.collider.transform.parent.gameObject.name == "bar of buildings")
                    {
                        {

                            if (worker != null)
                            {

                                worker.GetComponent<Walk>().Set_If_Butoon_Collide_On_Tool_Building(true);
                                worker = null;
                            }

                            Tool_Buildings.gameObject.SetActive(false);
                        }
                    }

                    else
                    {
                        if (worker != null)
                        {
                            worker.GetComponent<Walk>().Set_If_Butoon_Collide_On_Tool_Building(false);
                            worker = null;
                        }

                        Tool_Buildings.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (worker != null)
                    {
                        Debug.Log(worker);
                        worker.GetComponent<Walk>().Set_If_Butoon_Collide_On_Tool_Building(false);
                        worker = null;
                    }
                    Tool_Buildings.gameObject.SetActive(false);
                }


            }
            else if (Use_Move_Building_To_Build)
            {

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Move_Building_To_Build.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
                Collider2D[] allColliders = Physics2D.OverlapBoxAll(Move_Building_To_Build.transform.position, new Vector2(2f, 2f), 0);

                if (allColliders.Length > 0)
                {
                    Move_Building_To_Build.gameObject.SetActive(false);
                    No_Other_Build_In_Field = false;

                }

                else if (allColliders.Length == 0)
                {
                    Move_Building_To_Build.SetActive(true);
                    No_Other_Build_In_Field = true;
                }


            }


        }





    }









