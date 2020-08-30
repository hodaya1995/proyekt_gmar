using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Move_out_of_zone : MonoBehaviour
{


    public static bool OnZone_Camera(float x, float y)
    {
        if((y<=((0.5*x)+11.55)) && (y<=((-0.5*x)+14.05)) && (y>=((0.5*x)-13.10)) && (y >= ((-0.5 * x) - 15.10)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    // Update is called once per frame


}
