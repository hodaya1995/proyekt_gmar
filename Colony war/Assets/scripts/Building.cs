using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class Building : CreateColony
{
    string name;
    public string buildingHealth;
    public int buildingCost;

    public GameObject Panel;

    void Start()
    {
        name = this.gameObject.name;
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
                if (hitInformation.collider.name == this.name && hitInformation.collider.tag == this.tag)
                {
                    OpenPanel();
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
