using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public GameObject TextPanel = null;
    //public GameObject player;

    private bool isInsideTrigger = false;
    private bool messageReaded = false;

    public string message = "Hello World";

    public Text eText;
    private InputManager iM;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        iM = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //solo funciona con player
        {
            if (messageReaded)
            {
                return;
            }
            else Read();

            isInsideTrigger = true; //cambia el bool
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(messageReaded)
            {
                return;
            }
            else 
            isInsideTrigger = false;
        }
    }

    private void Read()
    {
        TextPanel.SetActive(true);
        eText.text = message;
        //Debug.Log("reading");
        messageReaded = true;
    }

    private void ReadEnd()
    {
        TextPanel.SetActive(false);
        //Debug.Log("quit");
    }
}
