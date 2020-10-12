using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializate_Postitions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (this.tag == "grid" || this.tag=="ground" || this.tag=="treelayer" || this.tag == "characters" || this.tag== "buildings" || this.tag== "workers"
            || this.tag== "enemy soldiers" || this.tag== "colony soldiers" || this.tag== "resource-test")
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
