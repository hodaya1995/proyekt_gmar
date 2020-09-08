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

    void Start()
    {
        origAmount = amount;
        statesCount = this.transform.childCount - 1;
        currState = 0;
        res = this.tag;
        textCanvas = GetTextCanvas();
        textCanvas.SetActive(false);
    }

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
                    SetText(res + "\n" + (int)amount);
                    textCanvas.SetActive(true);
                    Invoke("HideCanvas", 3f);
                }
            }
        }

    }

    void HideCanvas()
    {
        textCanvas.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("miner"))
        {
            miningSpeed = collision.gameObject.GetComponent<Worker>().miningSpeed;
            workerAnimator = collision.gameObject.GetComponent<Animator>();
            InvokeRepeating("DecraeseResource", 0, 1f);

        }
    }
    void SetText(string t)
    {

        GameObject canvas = this.transform.GetChild(this.transform.childCount - 1).gameObject;
        GameObject canvas2 = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
        Text textCompoent = canvas2.GetComponent<Text>();
        textCompoent.text = t;
    }

    GameObject GetTextCanvas()
    {
        return this.transform.GetChild(this.transform.childCount - 1).gameObject;
    }

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
        amount -= miningSpeed;
    }






}
