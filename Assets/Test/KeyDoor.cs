using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyDoor : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public int scene; //se introduce la scena a la que se quiere ir
	private bool isInsideTrigger = false;
	public int key = 1;
	void Start()
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	void Update()
	{
		if (isInsideTrigger && Input.GetKeyDown(KeyCode.C))
		{
			if(plBehaviour.Keys >= 1)
			{
				Debug.Log ("Change Scene");
				SceneManager.LoadScene(scene); //linea que hace que funcione
				plBehaviour.LoseKeys(key);
			}
			else Debug.Log("GetKeys");
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
}
