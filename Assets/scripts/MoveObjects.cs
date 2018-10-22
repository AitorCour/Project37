using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour 
{
	//Este script se le pasa al objeto que vaya a moverse. 
	//Se complementa con el drag object del player, que controla como se movera.
	Vector3 objectPos; 
	public GameObject player;
	public GameObject item;
	private EnableDisable enableDisable;
	private bool isInsideTrigger = false;
	private bool isHolding = false;

	// Use this for initialization
	void Start () 
	{
		enableDisable = GameObject.FindGameObjectWithTag("Player").GetComponent<EnableDisable>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isInsideTrigger)
		{
			if (Input.GetKey(KeyCode.U))
			{
				isHolding = true;
				//Debug.Log("isHolding");
			}
			if (Input.GetKeyDown(KeyCode.O))
			{
				player.transform.Translate(0, 4, 0);
				Debug.Log("Up");
			}
			if(isHolding)
			{
				//Debug.Log("Object Grabed");
				item.transform.SetParent(player.transform);
				enableDisable.SetDrag();
			}

			if(!isHolding)
			{
				objectPos = item.transform.position;
				item.transform.SetParent(null);
				enableDisable.SetTank();
			}

			if (Input.GetKeyUp(KeyCode.U))
			{
				isHolding = false;
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
