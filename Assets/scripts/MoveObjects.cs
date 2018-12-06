using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour 
{
	//Este script se le pasa al objeto que vaya a moverse. 
	//Se complementa con el drag object del player, que controla como se movera.
	Vector3 objectPos; 
	private GameObject player;
	public GameObject item;

	private EnableDisable enableDisable;
	private bool isInsideTrigger = false;
	private bool isHolding = false;
	
	//public Animator subirObj;
	// Use this for initialization
	void Start () 
	{
		enableDisable = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();
		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isInsideTrigger)
		{
			if (Input.GetButton("Action"))
			{
				isHolding = true;
				Debug.Log("isHolding");
				item.transform.SetParent(player.transform);
				enableDisable.SetDrag();
			}
			if (Input.GetButtonDown("Run") && !isHolding)
			{
				player.transform.Translate(0, 6, 6);
				player.transform.position = item.transform.position;
				//player.transform.Translate(0, 6, 0);
				//player.transform.position = item.transform.position;
				Debug.Log("Up");
			}

			if (Input.GetButtonUp("Action"))
			{
				isHolding = false;
				Debug.Log("NOTHolding");
				objectPos = item.transform.position;
				item.transform.SetParent(null);
				enableDisable.SetTank();
			}
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
