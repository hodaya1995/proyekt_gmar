using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_Enemy : MonoBehaviour
{

    int EnemyWorkers=0;
    int EnemySoldiers = 0;
    List<GameObject> EnemeyObjects = new List<GameObject>();
    List<bool> Flag = new List<bool> ();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name.Contains("worker building") && EnemyWorkers < 5)
        {
            EnemySoldiers++;
            EnemyWorkers++;
            GameObject f = GameObject.Find("characters").transform.Find("enemy soldiers").Find("workers").gameObject;
            GameObject o = Instantiate(GameObject.Find("characters").transform.Find("enemy soldiers").Find("workers").Find("miner female").gameObject);
            EnemeyObjects.Add(o);
            o.SetActive(false);
            o.transform.SetParent(f.transform);
            o.transform.position = new Vector3(this.transform.position.x + 1.61f, this.transform.position.y + 1.61f, this.transform.position.z);
            Invoke("timer", 5f);
        }

        else if(5<=EnemyWorkers && !EnemeyObjects[EnemySoldiers].name.Contains("barrcks")){


		}
    }

    void timer()
	{
        EnemeyObjects[EnemySoldiers].SetActive(true);
	}
}
