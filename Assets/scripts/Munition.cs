using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour 
{
	private Gun weapon;
	public GameObject ammoObject;
	private bool isInsideTrigger = false;
	public int munition;
	// Use this for initialization
	void Start () 
	{
		weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetKeyDown(KeyCode.C))
		{
			weapon.GetAmmo(munition);
			ammoObject.SetActive(false);
		}
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") //solo funciona con player
		{
			isInsideTrigger = true; //cambia el bool
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
