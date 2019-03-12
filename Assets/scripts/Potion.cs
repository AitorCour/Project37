﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Potion : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public GameObject potionObject;
	private bool isInsideTrigger = false;
	private int potion = 1;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	public bool MessageReaded = false;

	private SoundPlayer sound;
	//private InputManager inputManager;

	// Use this for initialization
	void Start () 
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		sound = GetComponentInChildren<SoundPlayer>();
		//inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetButtonDown("Action"))
		{
			//plBehaviour.GetPotions(potion);//eso está movido a los read, para que 
			//potionObject.SetActive(false);//no se repita y cojas dos
			if(MessageReaded)
			{
				ReadEnd();
			}
			else Read();
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
	private void Read()
	{
		TextPanel.SetActive(true);
		eText.text = message;
		//Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
		plBehaviour.GetPotions(potion);
		sound.Play(1, 2);
		/*if (Input.GetButtonDown("Esc") || Input.GetButtonDown("Inv") || Input.GetButtonDown("Map"))
		{
			return;
		}*/
	}
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		//Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
		potionObject.SetActive(false);
	}
}
