using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPos1 : MonoBehaviour
{
    public bool isInsideTrigger = false;
    public bool bustPlaced;
    public bool boxPlaced;
    public bool ballPlaced;

    // Use this for initialization
    void Start ()
    {
        
	}

    /*void Update()
    {
        if(isInsideTrigger)
        {
            for (int i = 0; i < useButtons.Length; i++)
            {
                useButtons[i].SetActive(true);
            }
        }
    }*/

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
}
