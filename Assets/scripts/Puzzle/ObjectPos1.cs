using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPos1 : MonoBehaviour
{
    public bool isInsideTrigger = false;

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
