﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    //private PlayerBehaviour plBehaviour;
    private HUD hud;
    private bool isInsideTrigger = false;

    public GameObject TextPanel = null;
    public string message = "Hello World";
    public Text eText;
    private bool MessageReaded = false;

    private AudioSource sound;
    private InputManager iM;
    private UseObject useObj;
    // Use this for initialization
    void Start()
    {
        //plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        sound = GetComponentInChildren<AudioSource>();
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        useObj = GameObject.FindGameObjectWithTag("ObjectPosition").GetComponent<UseObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened && !useObj.puzzleComplete)
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
        Debug.Log("reading");
        MessageReaded = true;
        Time.timeScale = 0;
        hud.PickBall();
        sound.Play();
        iM.canPause = false;
    }
    private void ReadEnd()
    {
        TextPanel.SetActive(false);
        Debug.Log("quit");
        MessageReaded = false;
        Time.timeScale = 1;
        //gameObject.SetActive(false);
      //gameObject.transform.position = new Vector3(0, 0, 0);
        iM.canPause = true;
        useObj.PickBall();
        //useObj.ballPlaced = false;
    }
}
