using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 7;
    bool flag;
    GameObject Tool_Resources;
    Vector3 Point_Tool_Resources;

    // Start is called before the first frame update
    void Start()
    {
        Point_Tool_Resources = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
        Point_Tool_Resources.z = Camera.main.transform.position.z + 5;
        Tool_Resources = GameObject.Find("fill of the bar");//Camera.main.gameObject.transform.Find("Canvas").gameObject.transform.Find("fill of the bar").gameObject;
        Tool_Resources.transform.position = Point_Tool_Resources;
    }

    // Update is called once per frame
    void Update()
    {

        Point_Tool_Resources = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
        Point_Tool_Resources.z = Camera.main.transform.position.z + 5;
        Tool_Resources.transform.position = Point_Tool_Resources;
        //true on start
        if (Input.GetMouseButtonDown(0))
        {

            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            flag = Move_out_of_zone.OnZone_Camera(touchStart.x, touchStart.y);


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
                if (Move_out_of_zone.OnZone_Camera(v.x, v.y))
                {
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


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraZoom : MonoBehaviour
//{
//    Vector3 touchStart;
//    public float zoomOutMin = 1;
//    public float zoomOutMax = 7;
//    bool flag;
//    GameObject Tool_Resources;
//    Vector3 Point_Tool_Resources;

//    Start is called before the first frame update
//    void Start()
//    {
//        Point_Tool_Resources = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
//        Point_Tool_Resources.z = Camera.main.transform.position.z + 5;
//        Tool_Resources = Camera.main.gameObject.transform.Find("Canvas").gameObject.transform.Find("fill of the bar").gameObject;
//        Tool_Resources.transform.position = Point_Tool_Resources;
//    }

//    Update is called once per frame
//    void Update()
//    {

//        Point_Tool_Resources = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
//        Point_Tool_Resources.z = Camera.main.transform.position.z + 5;
//        Tool_Resources.transform.position = Point_Tool_Resources;
//        true on start
//        if (Input.GetMouseButtonDown(0))
//        {

//            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            flag = Move_out_of_zone.OnZone_Camera(touchStart.x, touchStart.y);


//        }
//        two touch on screen to zoom in or out
//        if (Input.touchCount == 2)
//        {

//            store two touch position
//            Touch touchZero = Input.GetTouch(0);
//            Touch touchOne = Input.GetTouch(1);

//            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
//            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

//            distance between the point
//            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
//            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

//            float difference = currentMagnitude - prevMagnitude;

//            zoom(difference * 0.01f);
//        }
//        else if (Input.GetMouseButton(0) && flag)
//        {
//            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            flag = Move_out_of_zone.OnZone_Camera(point.x, point.y);
//            if (flag)
//            {
//                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                Vector3 v = Camera.main.transform.position + direction;
//                if (Move_out_of_zone.OnZone_Camera(v.x, v.y))
//                {
//                    Camera.main.transform.position += direction;
//                }
//            }

//        }

//    }


//    void zoom(float increment)
//    {
//        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
//    }
//}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraZoom : MonoBehaviour
//{
//    Vector3 touchStart;
//    public float zoomOutMin = 1;
//    public float zoomOutMax = 7;
//    bool flag;
//    GameObject Tool_Resources;
//    Vector3 Point_Tool_Resources;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Point_Tool_Resources = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
//        Point_Tool_Resources.z = Camera.main.transform.position.z + 5;
//        Tool_Resources = Camera.main.gameObject.transform.Find("Canvas").gameObject.transform.Find("fill of the bar").gameObject;
//        Tool_Resources.transform.position = Point_Tool_Resources;
//        Point_Tool_Resources = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.scaledPixelHeight, Camera.main.transform.position.z));
//        Point_Tool_Resources.z = Camera.main.transform.position.z + 5;
//        Tool_Resources.transform.position = Point_Tool_Resources;
//    }

//    // Update is called once per frame
//    void Update()
//    {


//        //true on start
//        if (Input.GetMouseButtonDown(0))
//        {

//            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            flag = Move_out_of_zone.OnZone_Camera(touchStart.x, touchStart.y);


//        }
//        //two touch on screen to zoom in or out
//        if (Input.touchCount == 2)
//        {

//            //store two touch position
//            Touch touchZero = Input.GetTouch(0);
//            Touch touchOne = Input.GetTouch(1);

//            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
//            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

//            //distance between the point
//            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
//            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

//            float difference = currentMagnitude - prevMagnitude;

//            zoom(difference * 0.01f);
//        }
//        else if (Input.GetMouseButton(0) && flag)
//        {
//            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            flag = Move_out_of_zone.OnZone_Camera(point.x, point.y);
//            if (flag)
//            {
//                Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                Vector3 v = Camera.main.transform.position + direction;
//                if (Move_out_of_zone.OnZone_Camera(v.x, v.y))
//                {
//                    Camera.main.transform.position += direction;
//                }
//            }

//        }

//    }


//    void zoom(float increment)
//    {
//        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
//    }
//}