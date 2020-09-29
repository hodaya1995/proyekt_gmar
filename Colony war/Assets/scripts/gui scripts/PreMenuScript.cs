using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreMenuScript : MonoBehaviour
{

    public void Awake()
    {  
        Invoke("DontStopLoading", 3f);
    }

    public void DontStopLoading()
    {
        SceneManager.LoadScene(2);
    }
  

}
