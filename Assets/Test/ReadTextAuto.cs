using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadTextAuto : MonoBehaviour 
{
	public GameObject TextPanel = null;

	private bool isInsideTrigger = false;

	//public string Text = "Hello World";
	public string message = "He World";

	public Text eText;

	

	// Update is called once per frame
	void Update () 
	{
		if (isInsideTrigger)
		{
			
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") //solo funciona con player
		{
			isInsideTrigger = true; //cambia el bool
			Debug.Log ("Enterd 2");
			TextPanel.SetActive(true);

			eText.text = message;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			isInsideTrigger = false;
			TextPanel.SetActive(false);
		}
	}

	private bool IsTextPanelActive
	{
		get
		{
			return TextPanel.activeInHierarchy;
		}
	}
	
}
