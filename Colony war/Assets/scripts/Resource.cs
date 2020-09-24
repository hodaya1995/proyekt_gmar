using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    Text countxet;
    int resbar = 0;

    void Start()
    {
        origAmount = amount;
        statesCount = this.transform.childCount - 1;
        currState = 0;
        res = this.tag;
        textCanvas = GetTextCanvas();
        textCanvas.SetActive(false);


        GameObject child = Camera.main.transform.Find("Canvas").gameObject;
        child = child.transform.Find("fill of the bar").gameObject;
     
        GameObject text = child.transform.Find("Text").gameObject;
        countxet = text.GetComponent<Text>();
        countxet.text = "" + resbar;
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

            if (hitInformation.collider != null)
            {
                if (hitInformation.collider.tag == res)
                {
                    SetText(""+(int)amount);
                    textCanvas.SetActive(true);
                    Invoke("HideCanvas", 3f);
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
        if (collision.collider.tag.Contains("miner"))
        {
            miningSpeed = collision.gameObject.GetComponent<Worker>().miningSpeed;
            workerAnimator = collision.gameObject.GetComponent<Animator>();
            InvokeRepeating("DecraeseResource", 0, 1f);
            
          

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("miner"))
        {
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
        resbar = resbar + t;
        countxet.text = "" + resbar;
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

 
        SetTextInBarResources((int)miningSpeed);
        SetText("" + (int)amount);

    }






}
