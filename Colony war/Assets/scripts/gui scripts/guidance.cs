using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class guidance : MonoBehaviour
{
	// Start is called before the first frame update

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hitInformation = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hitInformation.collider.name == "right")
			{
				if(SceneManager.GetActiveScene().name=="help 1")
				{
					SceneManager.LoadScene("MenuScene");
				}
				else if (SceneManager.GetActiveScene().name == "help 2")
				{
					SceneManager.LoadScene("help 1");
				}
				else if (SceneManager.GetActiveScene().name == "help 3")
				{
					SceneManager.LoadScene("help 2");
				}
			}
			else if (hitInformation.collider.name == "left")
			{
				if (SceneManager.GetActiveScene().name == "help 1")
				{
					SceneManager.LoadScene("help 2");
				}
				else if (SceneManager.GetActiveScene().name == "help 2")
				{
					SceneManager.LoadScene("help 3");
				}
				else if (SceneManager.GetActiveScene().name == "help 3")
				{
					SceneManager.LoadScene("MenuScene");
				}
			}
		}
	}
}
