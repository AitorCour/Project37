using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPoint : MonoBehaviour 
{
	public GameObject item;
	private bool isInsideTrigger = false;
	private GameObject player;
	private EnableDisable enableDisable;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		enableDisable = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isInsideTrigger)
		{
			if (Input.GetButtonDown("Run"))
			{
				enableDisable.NavActive = false;
				player.transform.Translate(0, 6, 6);
				player.transform.position = item.transform.position;

				Debug.Log("Up");
			}
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
