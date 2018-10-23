using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public GameObject noteObject;
	private bool isInsideTrigger = false;

	public GameObject TextPanel = null;
	public string message = "Hello World";
	public Text eText;
	private bool MessageReaded = false;
	private bool noteInv = false;
	public GameObject note1;
	// Use this for initialization
	void Start () 
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInsideTrigger && Input.GetKeyDown(KeyCode.U))
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
	}
	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
		noteObject.SetActive(false);
		noteInv = true;
		note1.SetActive(true);
	}
}
