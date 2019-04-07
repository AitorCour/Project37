using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadPause : MonoBehaviour 
{
	public GameObject TextPanel = null;
	//public GameObject player;

	private bool isInsideTrigger = false;
	private bool MessageReaded = false;

	public string message = "Hello World";

	public Text eText;
    private InputManager iM;
    void Start()
	{
        //player = GameObject.FindGameObjectWithTag("Player");
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideTrigger && Input.GetButtonDown("Action") && !iM.isPaused && !iM.isInventoryOpened && !iM.isMapOpened)
        {
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

	private void Read()
	{
		TextPanel.SetActive(true);
		eText.text = message;
		Debug.Log("reading");
		MessageReaded = true;
		Time.timeScale = 0;
        iM.canPause = false;
    }

	private void ReadEnd()
	{
		TextPanel.SetActive(false);
		Debug.Log("quit");
		MessageReaded = false;
		Time.timeScale = 1;
        iM.canPause = true;
    }
}
