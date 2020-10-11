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
    
    List<GameObject> list_Attackers = new List<GameObject>();
    
    string Name_Of_Building;
    string Name_Of_Building_Shai;
    bool Sturctue_Mine;

    bool isObstacle = true;


    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.gameObject.name.Contains("buildings"))
        {
            Sturctue_Mine = true;
            GameObject g = this.gameObject;
            slide = g.GetComponentInChildren<Slider>();
            spriterender = g.GetComponent<SpriteRenderer>();
            fill.color = gradient.Evaluate(1f);
            slide.maxValue = Bar_Life_Building;
            slide.value = 1;
            this.gameObject.tag = "first stage of the building";
            this.gameObject.SetActive(true);
            InvokeRepeating("Increase_Life_Building", 0f, 2f);



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

        if (this.Sturctue_Mine &&  this.tag == "first stage of the building" && collider.gameObject.GetComponent<Attack>() != null && collider.gameObject.transform.parent.parent.gameObject.name == "enemy soldiers"
            || !this.Sturctue_Mine && this.tag == "first stage of the building" && collider.gameObject.GetComponent<Attack>() != null && collider.gameObject.transform.parent.parent.gameObject.name == "colony soldiers")
        {
             

            Animator animator = collider.gameObject.GetComponent<Animator>();
            animator.SetBool("toAttack", true);
            list_Attackers.Add(collider.gameObject);
            Number_Objects_In_List_Attackers++;
            if (list_Attackers.Count == 1 && Number_Objects_In_List_Attackers == 1)
            {
                CancelInvoke("Increase_Life_Building");
                InvokeRepeating("Decrease_Life_Building", 0f, 0.5f);
            }


        }

        
    }



    void OnCollisionExit2D(Collision2D collision)
    {
        if (this.Sturctue_Mine && this.tag == "first stage of the building" && collision.gameObject.GetComponent<Attack>() != null && collision.gameObject.transform.parent.parent.gameObject.name == "enemy soldiers"
             || !this.Sturctue_Mine && this.tag == "first stage of the building" && collision.gameObject.GetComponent<Attack>() != null && collision.gameObject.transform.parent.parent.gameObject.name == "colony soldiers")
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
                InvokeRepeating("Increase_Life_Building", 0f, 2f);
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

           



            this.gameObject.SetActive(false);
            CancelInvoke("Decrease_Life_Building");
            CancelInvoke("Increase_Life_Building");
            Number_Objects_In_List_Attackers = 0;
            
            list_Attackers.Clear();
            
            



        }

        
    }





    void Increase_Life_Building()
    {
        if (slide.value > 0 && slide.value < 100)
        {
            float hitPower = 5;
            

            slide.value = slide.value + hitPower;
            fill.color = gradient.Evaluate(slide.normalizedValue);

            Set_Phase_Building();
        }
        else if (slide.value >= 100)
        {
            slide.value = 100;
            fill.color = gradient.Evaluate(slide.normalizedValue);
            
            
            CancelInvoke("Increase_Life_Building");
            this.gameObject.SetActive(false);
            if (Sturctue_Mine)
            {
                GameObject o = Instantiate(this.transform.parent.Find(Name_Of_Building_Shai).gameObject);
                GameObject r = GameObject.Find(Name_Of_Building);
                o.transform.SetParent(r.transform);
                o.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                o.SetActive(true);
            }
            else if (!Sturctue_Mine)
			{
                GameObject o = Instantiate(this.transform.parent.Find(Name_Of_Building_Shai).gameObject);
                GameObject r = GameObject.Find(Name_Of_Building);
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


    


     public void Set_Name_Building_shai(string s)
    {
        this.Name_Of_Building_Shai = s;
    }

   






}
