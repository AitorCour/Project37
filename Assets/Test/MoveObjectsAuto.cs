using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectsAuto : MonoBehaviour 
{
	public GameObject player;
	
	private bool isInsideTrigger = false;
	// Use this for initialization
	void Start () 
	{
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<TankControl>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger)
		{
			//tankControl.SetSlow(true);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = true; //cambia el bool
			Debug.Log ("Enterd 3");
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
