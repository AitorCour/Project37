using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadPause : MonoBehaviour 
{
	public GameObject TextPanel = null;
	public GameObject player;

	private bool isInsideTrigger = false;
	private bool MessageReaded = false;

	public string message = "Hello World";

	public Text eText;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		if (isInsideTrigger && Input.GetKeyDown(KeyCode.C))
		{
			if (MessageReaded)
			{
				ReadEnd();
			}	
			//Debug.Log("C pressed");

			else Read();
		}	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") //solo funciona con player
		{
			isInsideTrigger = true; //cambia el bool
			Debug.Log ("Enterd 2");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = false;
		}
	}

	private bool IsTextPanelActive
	{
		get
		{
			return TextPanel.activeInHierarchy;
		}
	}

	private void Read()
	{
		TextPanel.SetActive(true);
		eText.text = message;
		Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
	}

	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
	}
}
