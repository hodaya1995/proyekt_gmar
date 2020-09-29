using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string name;
    public string buildingHealth;
    public int buildingCost;

    public GameObject Panel;

    void Start()
    {
        Panel.SetActive(false);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hitInformation.collider != null)
            {
                if (hitInformation.collider.tag == this.tag)
                {
                    OpenPanel();
                    //Invoke("OpenPanel", 3f);
                }

            }
        }

    }



    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }

}
