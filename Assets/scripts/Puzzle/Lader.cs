﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Lader : MonoBehaviour
{
    private PlayerBehaviour plBehaviour;
	//public GameObject keyObject;
	private bool isInsideTrigger;
	//private int key = 1;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;

	private AudioSource sound;
    private InputManager iM;
    public bool getObj;
    // Use this for initialization
    void Start () 
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		sound = GetComponent<AudioSource>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
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
			//Debug.Log ("Enterd 2");
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
		plBehaviour.GetLader();
        Data.SetLader();
		//sound.Play(1, 2);
        iM.canPause = false;
        sound.Play();
    }
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		//Debug.Log("quit");
		MessageReaded = false;
        getObj = true;
		Time.timeScale = 1;
		this.transform.position = new Vector3(0,-10,0);
        iM.canPause = true;
    }
}
