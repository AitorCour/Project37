﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Munition : MonoBehaviour 
{
	private Gun weapon;
	//public GameObject ammoObject;
	public bool isInsideTrigger = false;
	public int munition;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	public bool MessageReaded = false;

	private SoundPlayer sound;
    private InputManager iM;
    // Use this for initialization
    void Start () 
	{
		weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Gun>();
		sound = GetComponentInChildren<SoundPlayer>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }
    void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
            //plBehaviour.GetKeys(key);
            //keyObject.SetActive(false);
            if (MessageReaded)
            {
                ReadEnd();
            }
            else Read();
        }
        else return;
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
		weapon.GetAmmo(munition);
		sound.Play(1, 2);
        iM.canPause = false;
    }
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
        isInsideTrigger = false;
        gameObject.SetActive(false);
        iM.canPause = true;
    }
}
