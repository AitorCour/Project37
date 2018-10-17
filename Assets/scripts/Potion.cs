using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public GameObject potionObject;
	private bool isInsideTrigger = false;
	private int potion = 1;
	// Use this for initialization
	void Start () 
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetKeyDown(KeyCode.C))
		{
			plBehaviour.GetPotions(potion);
			potionObject.SetActive(false);
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
