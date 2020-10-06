using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestructOfBuilding : MonoBehaviour
{

    GameObject[] particles = new GameObject[7];
    Slider slide;
    public Gradient gradient;
    public Image fill;
    float Bar_Life_Building = 50;
    Animator animator;
    GameObject collider1;
    GameObject Destruct_Building;
    bool flag = false;
    bool flag2 = true;
    Vector3[] Memory_For_The_First_Fires=new Vector3[4] ;
    bool isObstacle = true;


    // Start is called before the first frame update
    void Start()
    {

        GameObject g = this.gameObject;
        slide = g.GetComponentInChildren<Slider>();
        slide.interactable = false;
        fill.color = gradient.Evaluate(1f);
        slide.maxValue = Bar_Life_Building;
        slide.value = Bar_Life_Building;

        Destruct_Building = this.transform.parent.gameObject.transform.Find("destruct building").gameObject;
        Destruct_Building.SetActive(false);



        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = this.transform.Find("Particle System"+(i+1)).gameObject;
            particles[i].SetActive(false);


        }

        for (int i = 0; i < Memory_For_The_First_Fires.Length; i++)
        {
            Memory_For_The_First_Fires[i] = new Vector3(particles[i].transform.position.x, particles[i].transform.position.y, particles[i].transform.position.z);


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

        if (collider.gameObject.GetComponent<Attack>() != null&&((collider.collider.tag=="enemy soldier"&&this.tag == "colony building")||
            (collider.collider.tag == "colony soldier" && this.tag == "enemy building")))
        {

            animator = collider.gameObject.GetComponent<Animator>();
            animator.SetBool("toAttack", true);
            collider1 = collider.gameObject;
            InvokeRepeating("Decrease_Life_Building", 0f, 0.5f);


        }
        else if (collider.gameObject.GetComponent<Worker>() != null)
        {
            animator = collider.gameObject.GetComponent<Animator>();
            animator.SetBool("mine", true);
            collider1 = collider.gameObject;
            InvokeRepeating("Increase_Life_Building", 0f, 0.5f);
        }
    }


    void Increase_Life_Building()
    {
        if (slide.value > 0 && slide.value < 50)
        {
            float hitPower = collider1.gameObject.GetComponent<Worker>().miningSpeed;
            slide.value = slide.value + hitPower;
            fill.color = gradient.Evaluate(slide.normalizedValue);

            Set_Fire_Fix();
        }
        else if (slide.value >= 50)
        {
            slide.value = 50;
            fill.color = gradient.Evaluate(slide.normalizedValue);
            animator.SetBool("mine", false);
            CancelInvoke("Increase_Life_Building");
        }
    }


    void Decrease_Life_Building()
    {
        if (slide.value > 0)
        {
            float hitPower = collider1.GetComponent<Attack>().GetHitPower();
            slide.value = slide.value - hitPower;
            fill.color = gradient.Evaluate(slide.normalizedValue);

            Set_Fire_Destroy();
        }
        else if (flag == false && slide.value <= 0)
        {
            animator.SetBool("toAttack", false);
            this.gameObject.SetActive(false);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].SetActive(false);
            }

            Destruct_Building = this.transform.parent.gameObject;
            Destruct_Building = Destruct_Building.transform.Find("destruct building").gameObject;
            Destruct_Building.transform.position = this.transform.position;
            Destruct_Building.SetActive(true);
            flag = true;
            CancelInvoke("Decrease_Life_Building");
            InvokeRepeating("Decrease_Life_Building", 6f, 6f);



        }

        else
        {
            Destruct_Building.SetActive(false);
            flag = false;

            CancelInvoke("Decrease_Life_Building");
        }
    }


    void Set_Fire_Destroy()
    {
        if (fill.color == gradient.colorKeys[1].color)
        {
            for (int i = 0; i < 4; i++)
            {
                particles[i].SetActive(true);
            }
        }

        else if (fill.color == gradient.colorKeys[0].color && flag2 == true)
        {
            particles[6].transform.position = new Vector3(particles[1].transform.position.x, particles[1].transform.position.y, particles[1].transform.position.z);
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
        if (fill.color == gradient.colorKeys[2].color)
        {
            for (int i = 0; i < 7; i++)
            {
                particles[i].SetActive(false);
            }
        }

        else if (fill.color == gradient.colorKeys[1].color)
        {
            flag2 = true;
            for (int i = 0; i < particles.Length; i++)
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


    public float GetHealth()
    {
        return slide.value;
    }







}