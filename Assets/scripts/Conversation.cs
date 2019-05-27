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

    public string message_1 = "Hello World";
    public string message_2 = "Hello World";
    public string message_3 = "Hello World";
    public string message_4 = "Hello World";
    public string message_5 = "Hello World";

    public Text eText;
    public float timeCounter;
    void Update()
    {
        if(isInsideTrigger)
        {
            if (timeCounter >= 5)
            {
                eText.text = message_2;
            }
            if (timeCounter >= 10)
            {
                eText.text = message_3;
            }
            if (timeCounter >= 15)
            {
                eText.text = message_4;
            }
            if (timeCounter >= 20)
            {
                eText.text = message_5;
            }
            if (timeCounter >= 25)
            {
                ReadEnd();
                this.enabled = false;
            }
            timeCounter += Time.deltaTime;
        }
        
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
            //messageTime = 4;
            isInsideTrigger = true; //cambia el bool
        }
    }

    /*void OnTriggerExit(Collider other)
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
    }*/

    private void Read()
    {
        TextPanel.SetActive(true);
        eText.text = message_1;
        //Debug.Log("reading");
        messageReaded = true;
        //messageTime = 4;
    }

    private void ReadEnd()
    {
        TextPanel.SetActive(false);
        //Debug.Log("quit");
        //messageTime = 4;
    }
}
