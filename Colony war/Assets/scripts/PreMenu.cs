using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreMenu : MonoBehaviour
{
    void Update()
    {
         
        // Use a coroutine to load the Scene in the background
        StartCoroutine(LoadGame());
        
    }


    IEnumerator LoadGame()
    {
     
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
