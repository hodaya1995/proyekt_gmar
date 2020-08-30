using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 7;
    bool flag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
        //true on start
        if (Input.GetMouseButtonDown(0))
        {
            
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            flag = Move_out_of_zone.OnZone_Camera(touchStart.x, touchStart.y);
            Debug.Log(touchStart);
            
        }
        //two touch on screen to zoom in or out
        if (Input.touchCount == 2)
        {
            
           //store two touch position
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //distance between the point
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0) && flag)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            flag = Move_out_of_zone.OnZone_Camera(point.x, point.y);
            if (flag)
            {
                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 v = Camera.main.transform.position + direction;
                if (Move_out_of_zone.OnZone_Camera(v.x, v.y)){
                    Camera.main.transform.position += direction;
                }
            }

        }

    }


    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
