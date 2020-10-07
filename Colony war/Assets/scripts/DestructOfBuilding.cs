using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestructOfBuilding : MonoBehaviour
{
    
    GameObject  [] particles=new GameObject[7] ;
    Slider slide;
    public Gradient gradient;
    public Image fill;
    float Bar_Life_Building = 100;
    GameObject Destruct_Building;
    bool flag = false;
    bool flag2 = true;
    int Number_Objects_In_List_Attackers=0;
    int Number_Objects_In_List_Workers = 0;
    Vector3 [] Memory_For_The_First_Fires;
    List<GameObject> list_Attackers = new List<GameObject>();
    List<GameObject> list_Worksers = new List<GameObject>();
    bool Sturctue_Mine;
    bool isObstacle = true;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.gameObject.name == "buildings")
        {




                if (this.transform.parent.parent.gameObject.name == "colony soldiers")
			    {
                    Sturctue_Mine = true;
		      	}

                else if(this.transform.parent.parent.gameObject.name == "enemy soldiers")
		    	{
                Sturctue_Mine = false;

                }
                GameObject g = this.gameObject;
                slide = g.GetComponentInChildren<Slider>();
                fill.color = gradient.Evaluate(1f);
                slide.maxValue = Bar_Life_Building;
                slide.value = Bar_Life_Building;
                this.gameObject.tag = "buildings";
                Destruct_Building = Instantiate(this.transform.parent.gameObject.transform.Find("destruct building").gameObject);
                Destruct_Building.SetActive(false);
               


                particles[0] = this.transform.Find("Particle System1").gameObject;
                particles[1] = this.transform.Find("Particle System2").gameObject;
                particles[2] = this.transform.Find("Particle System3").gameObject;
                particles[3] = this.transform.Find("Particle System4").gameObject;
                particles[4] = this.transform.Find("Particle System5").gameObject;
                particles[5] = this.transform.Find("Particle System6").gameObject;
                particles[6] = this.transform.Find("Particle System7").gameObject;

                for (int i = 0; i < 7; i++)
                {
                    particles[i].SetActive(false);


                }
                Memory_For_The_First_Fires = new Vector3[4];

                for (int i = 0; i < 4; i++)
                {
                    Memory_For_The_First_Fires[i] = new Vector3(particles[i].transform.position.x, particles[i].transform.position.y, particles[i].transform.position.z);


            }
            


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

        if (this.Sturctue_Mine && this.tag == "buildings" && collider.gameObject.GetComponent<Attack>() != null && collider.gameObject.transform.parent.gameObject.name=="enemy soldiers"
            || !this.Sturctue_Mine && this.tag == "buildings" && collider.gameObject.GetComponent<Attack>() != null && collider.gameObject.transform.parent.gameObject.name == "colony soldiers")
        {
			
            Animator animator = collider.gameObject.GetComponent<Animator>();
            animator.SetBool("toAttack", true);
            list_Attackers.Add(collider.gameObject);
            Number_Objects_In_List_Attackers++;
            if (list_Attackers.Count==1 && Number_Objects_In_List_Attackers == 1)
            {
                InvokeRepeating("Decrease_Life_Building", 0f, 0.5f);
            }


        }

        else if (this.Sturctue_Mine && this.tag == "buildings" && collider.gameObject.GetComponent<Worker>() != null && collider.gameObject.transform.parent.parent.parent.gameObject.name== "colony soldiers"
            || !this.Sturctue_Mine && this.tag == "buildings" && collider.gameObject.GetComponent<Worker>() != null && collider.gameObject.transform.parent.parent.parent.gameObject.name == "enemy soldiers")
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
        if (this.Sturctue_Mine && this.tag == "buildings" && collision.gameObject.GetComponent<Attack>() != null && collision.gameObject.transform.parent.gameObject.name == "enemy soldiers"
             || !this.Sturctue_Mine && this.tag == "buildings" && collision.gameObject.GetComponent<Attack>() != null && collision.gameObject.transform.parent.gameObject.name == "colony soldiers")
		{
            Animator a = collision.gameObject.GetComponent<Animator>();
            a.SetBool("toAttack",false);
           
            for(int i= 0;i< list_Attackers.Count;i++)
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

        else if(this.Sturctue_Mine && this.tag == "buildings" && collision.gameObject.GetComponent<Worker>() != null && collision.gameObject.transform.parent.parent.parent.gameObject.name == "colony soldiers"
            || !this.Sturctue_Mine && this.tag == "buildings" && collision.gameObject.GetComponent<Worker>() != null && collision.gameObject.transform.parent.parent.parent.gameObject.name == "enemy soldiers")
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
				if (list_Attackers[i].gameObject.GetComponent<Attack>()!= null)
				{
                    hitPower = hitPower + list_Attackers[i].GetComponent<Attack>().GetHitPower();
				}
            }
            
            slide.value = slide.value - hitPower;
            fill.color = gradient.Evaluate(slide.normalizedValue);
           
            Set_Fire_Destroy();
        }
        else if(flag==false && slide.value<=0)
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

            for (int i = 0; i < 7; i++)
			{
                particles[i].SetActive(false);
			}

            this.gameObject.SetActive(false);
            Destruct_Building.transform.position = this.transform.position;
            Destruct_Building.SetActive(true);
            flag = true;
            CancelInvoke("Decrease_Life_Building");
            CancelInvoke("Increase_Life_Building");
            Number_Objects_In_List_Attackers = 0;
            Number_Objects_In_List_Workers = 0;
            list_Attackers.Clear();
            list_Worksers.Clear();
            InvokeRepeating("Decrease_Life_Building", 6f, 6f);



        }

		else
		{
            Destruct_Building.SetActive(false);
            flag = false;
            
            CancelInvoke("Decrease_Life_Building");
        }
    }



    void Increase_Life_Building()
	{
        if (slide.value > 0 && slide.value<100)
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

            Set_Fire_Fix();
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
        }
    }




    void Set_Fire_Destroy()
	{
		if (fill.color == gradient.colorKeys[1].color)
		{
            for(int i = 0; i < 4; i++)
			{
                particles[i].SetActive(true);
			}
		}

        else if (fill.color == gradient.colorKeys[0].color && flag2==true)
		{
            particles[6].transform.position= new Vector3(particles[1].transform.position.x, particles[1].transform.position.y, particles[1].transform.position.z);
            particles[1].transform.position = new Vector3(particles[3].transform.position.x, particles[3].transform.position.y, particles[3].transform.position.z);
            particles[3].transform.position = new Vector3(particles[0].transform.position.x, particles[0].transform.position.y, particles[0].transform.position.z); 
            particles[0].transform.position = new Vector3(particles[2].transform.position.x, particles[2].transform.position.y, particles[2].transform.position.z);
            particles[2].SetActive(false);
            
            particles[4].SetActive(true);
            particles[5].SetActive(true);
            particles[6].SetActive(true);
            flag2 = false;
           
            

            
			
		}
	}



    void Set_Fire_Fix()
	{
		if(fill.color == gradient.colorKeys[2].color)
		{
            for(int i = 0; i < 7; i++)
			{
                particles[i].SetActive(false);
			}
		}

        else if(fill.color == gradient.colorKeys[1].color )
		{
            flag2 = true;
            for(int i = 0; i < 7; i++)
			{
				if (i < 4)
				{
                    particles[i].transform.position = new Vector3(Memory_For_The_First_Fires[i].x, Memory_For_The_First_Fires[i].y, Memory_For_The_First_Fires[i].z);
				}
                else if (i >= 4)
				{
                    particles[i].SetActive(false);
				}
			}
		}

	}





}
   
