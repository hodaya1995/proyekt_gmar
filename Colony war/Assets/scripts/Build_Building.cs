using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class Build_Building : MonoBehaviour
{
    
    Slider slide;
    SpriteRenderer spriterender;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Gradient gradient;
    public Image fill;
    float Bar_Life_Building = 100;
    
   
    int Number_Objects_In_List_Attackers = 0;
    int Number_Objects_In_List_Workers = 0;
    List<GameObject> list_Attackers = new List<GameObject>();
    List<GameObject> list_Worksers = new List<GameObject>();
    string Name_Of_Building;
    bool Sturctue_Mine;

    bool isObstacle = true;


    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.gameObject.name == "buildings")
        {

            GameObject g = this.gameObject;
            slide = g.GetComponentInChildren<Slider>();
            spriterender = g.GetComponent<SpriteRenderer>();
            fill.color = gradient.Evaluate(1f);
            slide.maxValue = Bar_Life_Building;
            slide.value = 1;
            this.gameObject.tag = "first stage of the building";
            this.gameObject.SetActive(true);
   

        }
    }

    public bool IsObstacle()
    {
        return isObstacle;
    }
    public void SetObstacle(bool isObstacle)
    {
        this.isObstacle = isObstacle;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {

        if (this.Sturctue_Mine &&  this.tag == "first stage of the building" && collider.gameObject.GetComponent<Attack>() != null && collider.gameObject.transform.parent.gameObject.name == "enemy soldiers"
            || !this.Sturctue_Mine && this.tag == "first stage of the building" && collider.gameObject.GetComponent<Attack>() != null && collider.gameObject.transform.parent.gameObject.name == "colony soldiers")
        {
             

            Animator animator = collider.gameObject.GetComponent<Animator>();
            animator.SetBool("toAttack", true);
            list_Attackers.Add(collider.gameObject);
            Number_Objects_In_List_Attackers++;
            if (list_Attackers.Count == 1 && Number_Objects_In_List_Attackers == 1)
            {
                InvokeRepeating("Decrease_Life_Building", 0f, 0.5f);
            }


        }

        //else if (this.Sturctue_Mine && this.tag == "first stage of the building" && collider.gameObject.GetComponent<Worker>() != null && collider.gameObject.transform.parent.parent.parent.gameObject.name == "colony soldiers"
        //     || !this.Sturctue_Mine && this.tag == "first stage of the building" && collider.gameObject.GetComponent<Worker>() != null && collider.gameObject.transform.parent.parent.parent.gameObject.name == "enemy soldiers")
        //{
        else if (this.Sturctue_Mine && this.tag == "first stage of the building" && collider.gameObject.tag.Contains("gold miner"))
        {
            Animator animator = collider.gameObject.GetComponent<Animator>();
           
            animator.SetBool("mine", true);
            list_Worksers.Add(collider.gameObject);
            Number_Objects_In_List_Workers++;
            if (list_Worksers.Count == 1 && Number_Objects_In_List_Workers == 1)
            {
                InvokeRepeating("Increase_Life_Building", 0f, 0.5f);
            }

        }
    }



    void OnCollisionExit2D(Collision2D collision)
    {
        if (this.Sturctue_Mine && this.tag == "first stage of the building" && collision.gameObject.GetComponent<Attack>() != null && collision.gameObject.transform.parent.gameObject.name == "enemy soldiers"
             || !this.Sturctue_Mine && this.tag == "first stage of the building" && collision.gameObject.GetComponent<Attack>() != null && collision.gameObject.transform.parent.gameObject.name == "colony soldiers")
        {
            Animator a = collision.gameObject.GetComponent<Animator>();
            a.SetBool("toAttack", false);

            for (int i = 0; i < list_Attackers.Count; i++)
            {

                if (list_Attackers[i].transform.position == collision.gameObject.transform.position)
                {
                    list_Attackers.RemoveAt(i);
                }
            }

            if (list_Attackers.Count == 0)
            {
                CancelInvoke("Decrease_Life_Building");
                Number_Objects_In_List_Attackers = 0;
            }

        }

        else if (this.Sturctue_Mine && this.tag == "first stage of the building" && collision.gameObject.GetComponent<Worker>() != null && collision.gameObject.transform.parent.parent.parent.gameObject.name == "colony soldiers"
           || !this.Sturctue_Mine && this.tag == "first stage of the building" && collision.gameObject.GetComponent<Worker>() != null && collision.gameObject.transform.parent.parent.parent.gameObject.name == "enemy soldiers")
        {
            Animator a = collision.gameObject.GetComponent<Animator>();
           
            a.SetBool("mine", false);

            for (int i = 0; i < list_Worksers.Count; i++)
            {

                if (list_Worksers[i].transform.position == collision.gameObject.transform.position)
                {
                    list_Worksers.RemoveAt(i);
                }
            }

            if (list_Attackers.Count == 0)
            {
                CancelInvoke("Increase_Life_Building");
                Number_Objects_In_List_Workers = 0;
            }
        }

    }

    void Decrease_Life_Building()
    {


        
        if (slide.value > 0)
        {
            float hitPower = 0;
            for (int i = 0; i < list_Attackers.Count; i++)
            {
                if (list_Attackers[i].gameObject.GetComponent<Attack>() != null)
                {
                    hitPower = hitPower + list_Attackers[i].GetComponent<Attack>().GetHitPower();
                }
            }

            slide.value = slide.value - hitPower;
            fill.color = gradient.Evaluate(slide.normalizedValue);

            Set_Phase_Building();
        }
        else if ( slide.value <= 0)
        {
            for (int i = 0; i < list_Attackers.Count; i++)
            {
                if (list_Attackers[i].gameObject.GetComponent<Attack>() != null)
                {
                    list_Attackers[i].GetComponent<Animator>().SetBool("toAttack", false);
                    list_Attackers.RemoveAt(i);
                }
            }

            for (int i = 0; i < list_Worksers.Count; i++)
            {
                if (list_Attackers[i].gameObject.GetComponent<Worker>() != null)
                {
                    list_Attackers[i].GetComponent<Animator>().SetBool("mine", false);
                    list_Attackers.RemoveAt(i);
                }
            }



            this.gameObject.SetActive(false);
            CancelInvoke("Decrease_Life_Building");
            CancelInvoke("Increase_Life_Building");
            Number_Objects_In_List_Attackers = 0;
            Number_Objects_In_List_Workers = 0;
            list_Attackers.Clear();
            list_Worksers.Clear();
            



        }

        
    }





    void Increase_Life_Building()
    {
        if (slide.value > 0 && slide.value < 100)
        {
            float hitPower = 0;
            for (int i = 0; i < list_Worksers.Count; i++)
            {
                if (list_Worksers[i].gameObject.GetComponent<Worker>() != null)
                {
                    hitPower = hitPower + list_Worksers[i].GetComponent<Worker>().miningSpeed;
                }
            }

            slide.value = slide.value + hitPower;
            fill.color = gradient.Evaluate(slide.normalizedValue);

            Set_Phase_Building();
        }
        else if (slide.value >= 100)
        {
            slide.value = 100;
            fill.color = gradient.Evaluate(slide.normalizedValue);
            for (int i = 0; i < list_Worksers.Count; i++)
            {
                if (list_Worksers[i].gameObject.GetComponent<Worker>() != null)
                {
                    list_Worksers[i].GetComponent<Animator>().SetBool("mine", false);
                   
                    list_Worksers.RemoveAt(i);
                }
            }
            Number_Objects_In_List_Workers = 0;
            CancelInvoke("Increase_Life_Building");
            this.gameObject.SetActive(false);
            if (Sturctue_Mine)
            {
                GameObject o = Instantiate(GameObject.Find("characthers").transform.Find("colony soldiers").Find("buildings").Find(Name_Of_Building).gameObject);
                GameObject r = GameObject.Find("characters").transform.Find("colony soldiers").Find("buildings").gameObject;
                o.transform.SetParent(r.transform);
                o.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                o.SetActive(true);
            }
            else if (!Sturctue_Mine)
			{
                GameObject o = Instantiate(GameObject.Find("characthers").transform.Find("enemy soldiers").Find("buildings").Find(Name_Of_Building).gameObject);
                GameObject r = GameObject.Find("characters").transform.Find("enemy soldiers").Find("buildings").gameObject;
                o.transform.SetParent(r.transform);
                o.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                o.SetActive(true);
            }
            
        }
    }




    void Set_Phase_Building()
    {
        if (fill.color == gradient.colorKeys[1].color)
        {
            spriterender.sprite = sprite2;
        }

        else if (fill.color == gradient.colorKeys[0].color)
        {
            spriterender.sprite = sprite1;

        }

        else if (fill.color == gradient.colorKeys[2].color)
        {
            spriterender.sprite = sprite3;
        }
    }

    public void Set_Name_Of_Building(string s)
	{
        this.Name_Of_Building = s;
	}

    public void Set_Side_Of_Player(bool b)
	{
        this.Sturctue_Mine = b;
	}









}
