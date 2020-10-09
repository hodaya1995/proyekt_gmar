using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadOfGame : MonoBehaviour
{
    public GameObject gold;
    public GameObject colonyWorkerBuilding;
    public GameObject enemyWorkerBuilding;
    float minRangewidth = -20.0f;
    float maxRangewidth = 20.3f;
    float minRangeheight = -7.2f;
    float maxRangegheight = 4.5f;




    // Start is called before the first frame update
    void Start()
    {
        CreateGoldOnMap();
        //LocateBuildingOnMap(colonyWorkerBuilding);
        //LocateBuildingOnMap(enemyWorkerBuilding);
        Instantiate(colonyWorkerBuilding, new Vector2(0.5f,-9.3f), Quaternion.identity);
        //Instantiate(enemyWorkerBuilding, new Vector2(21.1f, 0.4f), Quaternion.identity);
        Instantiate(enemyWorkerBuilding, new Vector2(6.1f, 2.5f), Quaternion.identity);
    }

    
    public void LocateBuildingOnMap(GameObject building)
    {

        Vector3 pos = GetRandomPosition();
        if((GetNearColliders(pos) == null || GetNearColliders(pos).Length == 0) && Move_out_of_zone.OnZone_Characters(pos.x, pos.y) )
        {
            Instantiate(building, pos, Quaternion.identity);
        
        }
        else
        {
            LocateBuildingOnMap(building);
        }


    }

    Collider2D[] GetNearColliders(Vector3 pos)
    {
        Collider2D[] allColliders = Physics2D.OverlapCircleAll(pos, 3f);
        return allColliders;
    }
        Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(minRangewidth, maxRangewidth), Random.Range(minRangeheight, maxRangegheight), 0);
        return position;
    }
    void CreateGoldOnMap()
    {
        Instantiate(gold, new Vector3(-0.5f, 5.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.0f, 5.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.4f, 6.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.9f, 6.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.8f, 6.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(2.8f, 6.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.3f, 6.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(4.2f, 6.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(4.5f, 5.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(4.3f, 5.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.4f, 5.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(2.8f, 5.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(2.7f, 5.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.8f, 5.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.4f, 5.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.5f, 5.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.0f, 5.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.6f, 5.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.3f, 5.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-0.2f, 5.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-0.3f, 5.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.3f, 4.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(2.6f, 5.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.1f, 5.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.9f, 6.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(5.2f, 6.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(5.1f, 6.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(5.2f, 8.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(4.7f, 8.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.9f, 8.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.5f, 9.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.3f, 9.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(3.7f, 9.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(4.1f, 10.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(4.4f, 10.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(5.3f, 10.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(5.5f, 10.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(2.7f, 9.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.8f, 9.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(1.5f, 9.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.3f, 9.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-0.1f, 9.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-0.4f, 8.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.0f, 8.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-2.0f, 8.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-2.2f, 8.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-2.7f, 7.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.0f, 6.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.0f, 5.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-2.4f, 5.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-2.1f, 5.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-1.8f, 5.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-2.1f, 8.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-1.4f, 9.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-0.5f, 9.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(0.0f, 9.4f, 0), Quaternion.identity);


        Instantiate(gold, new Vector3(-5.1f, -3.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-4.9f, -3.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-4.0f, -3.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.8f, -3.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-4.5f, -3.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-5.3f, -4.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.3f, -4.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.8f, -4.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.2f, -3.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.4f, -2.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.5f, -1.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.9f, -1.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.4f, -1.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.5f, -1.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.7f, -2.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.0f, -2.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.6f, -2.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.6f, -2.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.0f, -2.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.1f, -2.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.9f, -2.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-9.3f, -2.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-10.2f, -1.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-10.4f, -1.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-10.3f, -0.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-10.7f, -0.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-11.3f, -0.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-11.5f, 0.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-11.3f, 1.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-11.0f, 1.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-11.3f, 1.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-11.3f, 1.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.7f, 0.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.2f, 0.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.4f, 0.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.8f, -0.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.9f, -0.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.5f, -0.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.3f, -0.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-8.3f, -0.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.5f, 0.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.4f, 1.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.4f, 1.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.8f, 1.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.0f, 1.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.3f, 2.3f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.5f, 2.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.6f, 2.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.6f, 3.0f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.3f, 2.7f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.7f, 2.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.1f, 1.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.4f, 2.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-6.8f, 2.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-7.1f, 2.8f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-4.9f, 1.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-4.5f, 0.5f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-4.1f, 0.2f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.8f, -0.4f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.6f, -0.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.5f, -1.1f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.5f, -0.9f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.3f, -0.6f, 0), Quaternion.identity);



        Instantiate(gold, new Vector3(-3.1f, -0.4f, 0), Quaternion.identity);






    }
}
