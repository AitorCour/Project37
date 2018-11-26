using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsAuto : MonoBehaviour 
{
	private GameObject player;
	public GameObject item;
	
	private bool isInsideTrigger = false;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isInsideTrigger)
		{
			if (Input.GetButtonDown("Run"))
			{
				player.transform.position = item.transform.position;
				player.transform.Translate(0, 4, 0);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = true; //cambia el bool
			//Debug.Log ("Enterd 3");
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
