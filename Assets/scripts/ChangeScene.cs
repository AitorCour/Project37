using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //poner esto en todo lo que vaya a cambiar de escena



public class ChangeScene : MonoBehaviour 
{
	public int scene; //se introduce la scena a la que se quiere ir
	private bool isInsideTrigger = false;

	void Update()
	{
		if (isInsideTrigger && Input.GetKeyDown(KeyCode.C))
		{
			Debug.Log ("Change Scene");
			SceneManager.LoadScene(scene); //linea que hace que funcione
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = true;
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = false;
		}
	}
	//Change Scene In Menu
	public void ChangeToScene (int num)
	{
		SceneManager.LoadScene(num);
	}

	public void ExitGame()
	{
		Debug.Log("Exit Game");
		Application.Quit();
	}
}
