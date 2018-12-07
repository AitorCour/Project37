using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Key : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public GameObject keyObject;
	private bool isInsideTrigger = false;
	private int key = 1;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;

	private SoundPlayer sound;

	// Use this for initialization
	void Start () 
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
		sound = GetComponentInChildren<SoundPlayer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetButtonDown("Action"))
		{
			//plBehaviour.GetKeys(key);
			//keyObject.SetActive(false);
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
		plBehaviour.GetKeys(key);
		sound.Play(1, 2);
	}
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
		keyObject.SetActive(false);
	}
}
