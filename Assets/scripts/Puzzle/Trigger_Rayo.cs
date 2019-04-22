using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Rayo : MonoBehaviour 
{
	private Animator animRayo;
    private bool activated;
	void Start()
	{
		animRayo = GetComponentInChildren<Animator>();
	}
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //solo funciona con player
        {
            if (!activated)
            {
                animRayo.SetTrigger("rayo");
                activated = true;
            }
            else return;
			
        }
    }
}
