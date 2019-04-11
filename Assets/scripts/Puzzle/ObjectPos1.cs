using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPos1 : MonoBehaviour
{
    public bool isInsideTrigger = false;
    public bool occuped;
    private AudioSource sound;
    // Use this for initialization
    void Start()
    {
        sound = GetComponentInChildren<AudioSource>();
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
    public void PutSound()
    {
        sound.Play();
    }
}
