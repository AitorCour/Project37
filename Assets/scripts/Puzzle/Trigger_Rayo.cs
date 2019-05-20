using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Rayo : MonoBehaviour 
{
	private Animator animRayo;
    public bool activated;
    private AudioSource audioSc;
	void Start()
	{
		animRayo = GetComponentInChildren<Animator>();
        audioSc = GetComponent<AudioSource>();
	}
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //solo funciona con player
        {
            if (!activated)
            {
                animRayo.SetTrigger("rayo");
                audioSc.Play();
                activated = true;
            }
            else return;	
        }
    }
}
