﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour 
{
	private bool isInsideTrigger = false;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;

    private SoundObj sound;
    private InputManager iM;
    private HUD hud;
	public bool getObj;
    // Use this for initialization
    void Start () 
	{
        sound = GetComponentInChildren<SoundObj>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
		{
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
		Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
        sound.Play(this.gameObject, 0);
        hud.hasFragNote_1 = true;
        hud.SetKey();
        Data.SetNoteFrag_1();
        iM.canPause = false;
    }
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
		getObj = true;
		gameObject.SetActive(false);
        iM.canPause = true;
    }
}
