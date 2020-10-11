using UnityEngine;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System;

public class Building : CreateColony
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
                }
            }
        }

    }


    void SetBuildingAsObstacle(GameObject building)
    {

        DynamicGridObstacle obstacle = building.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = false;
        Flock.SetAsObstacle(true, "buildings");

    }

    void ResetBuildingAsObstacle(GameObject building)
    {

        DynamicGridObstacle obstacle = building.GetComponent<DynamicGridObstacle>();
        obstacle.enabled = false;
        Flock.SetAsObstacle(false, "buildings");

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
