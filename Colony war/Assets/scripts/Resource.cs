﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Resource : MonoBehaviour
{
    public float amount = 100f;
    float origAmount;
    float miningSpeed;
    int statesCount;
    int currState;
    Animator workerAnimator;
    string res;
    GameObject textCanvas;
    public bool occupied;

    Text countxet;
    int resbar = (int) Resources.gold;
    GameObject miner;
    GameObject enemyplayes;
    GameObject toolResources;
   

    void Start()
    {
        enemyplayes = GameObject.Find("enemy player");
        origAmount = amount;
        statesCount = this.transform.childCount - 1;
        currState = 0;
        res = this.tag;
        textCanvas = GetTextCanvas();
        textCanvas.SetActive(false);


        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;
        toolResources = child;
        GameObject text = child.transform.Find("Text").gameObject;
        countxet = text.GetComponent<Text>();
        countxet.text = "" + resbar;

    }

    public static void setToolBarColors(int number)
    {
       
        GameObject toolResources = Camera.main.gameObject.transform.Find("Canvas2").gameObject.transform.Find("bar of buildings").gameObject;
        SpriteRenderer temp1 = toolResources.transform.Find("icon of stable").gameObject.GetComponent<SpriteRenderer>();
        temp1.color = new Color(255, 0, 0);
        SpriteRenderer temp2 = toolResources.transform.Find("icon of barracks").gameObject.GetComponent<SpriteRenderer>();
        temp2.color = new Color(255, 0, 0);
        SpriteRenderer temp3 = toolResources.transform.Find("icon of archers building").gameObject.GetComponent<SpriteRenderer>();
        temp3.color = new Color(255, 0, 0);
        SpriteRenderer temp4 = toolResources.transform.Find("icon of workers building").gameObject.GetComponent<SpriteRenderer>();
        temp4.color = new Color(255, 0, 0);
        SpriteRenderer temp5 = toolResources.transform.Find("icon of protecting tower").gameObject.GetComponent<SpriteRenderer>();
        temp5.color = new Color(255, 0, 0);

        if (number >= 120)
        {
            Debug.Log(" setToolBarColors ");
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
    public static void ResetResourceAsObstacle(GameObject resorce)
    {

        DynamicGridObstacle obstacle = resorce.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = false;
        Flock.SetAsObstacle(false, "resources");

    }

    public static void SetResourceAsObstacle(GameObject resorce)
    {

        DynamicGridObstacle obstacle = resorce.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = true;
        Flock.SetAsObstacle(true, "resources");

    }
    /// <summary>
    /// for each frame: if the resource is touched, show description of the resource
    /// </summary>
    void Update()
    {
        bool detectedTouch = Input.GetMouseButtonDown(0);
        

        if (detectedTouch)
        {



            //touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Vector2.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            Vector3 mousePos = Input.mousePosition;

            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //Debug.Log(mousePos);
            if (hitInformation.collider != null)
            {
                if (hitInformation.collider.tag == res)
                {
                    Resource resource = hitInformation.collider.gameObject.GetComponent<Resource>();
                    resource.SetText("" + (int)amount);
                    resource.textCanvas.SetActive(true);
                    resource.Invoke("HideCanvas", 3f);
                }
            }
        }

    }

    /// <summary>
    /// hide the canvas of the description's text
    /// </summary>
    void HideCanvas()
    {
        textCanvas.SetActive(false);
    }


    /// <summary>
    /// when collides with its miner- decrase the resorce gardually
    /// </summary>
    /// <param name="collision">miner's collision</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("gold miner"))
        {
            miningSpeed = collision.gameObject.GetComponent<Worker>().miningSpeed;
            workerAnimator = collision.gameObject.GetComponent<Animator>();
            occupied = true;
            InvokeRepeating("DecraeseResource", 0, 1f);
            miner = collision.gameObject;


        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("gold miner"))
        //if (collision.collider.tag == "gold miner colony")
        {
           
            occupied = false;
            HideCanvas();
        }
    }

    void SetText(string t)
    {

        GameObject canvas = this.transform.GetChild(this.transform.childCount - 1).gameObject;
        GameObject canvas2 = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
        //GameObject text = canvas2.transform.GetChild(canvas.).gameObject;
        Text textCompoent = canvas2.GetComponentInChildren<Text>();
        textCompoent.text = t;
    }


    void SetTextInBarResources(int t)
    {
        resbar =t;
        //resbar = resbar + t;
        countxet.text = "" + resbar;
    }

    void SetGoldEnemy(int t)
    {
        enemyplayes.GetComponent<Options_Enemy>().SetGoldEnemyPlayer(t);
    }


    GameObject GetTextCanvas()
    {
        return this.transform.GetChild(this.transform.childCount - 1).gameObject;

    }


    /// <summary>
    /// decreasing resource by destroying its children
    /// </summary>
    void DecraeseResource()
    {

        if (amount <= 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this.gameObject);
            CancelInvoke("DecraeseResource");
            workerAnimator.SetBool("mine", false);
        }
        else
        {
            if (amount <= origAmount - (currState + 1) * (origAmount / statesCount))
            {
                Destroy(this.transform.GetChild(0).gameObject);
                currState++;
            }
        }

        int temp_a = (int)amount;
        int temp_m = (int)miningSpeed;
        amount = temp_a;
        miningSpeed = temp_m;
        amount -= miningSpeed;
        if (miner.tag == "gold miner colony")
        {
            
            Resources.gold += miningSpeed;
            SetTextInBarResources((int)Resources.gold);
            SetText("" + (int)amount);
            setToolBarColors((int)Resources.gold);


        }
       else if(miner.tag=="gold mine enemy")
		{
            Resources.enemygold += miningSpeed;
            SetGoldEnemy((int)Resources.enemygold);
            SetText("" + (int)amount);
        }

    }






}

