using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Rayo : MonoBehaviour 
{
	private Animator animRayo;
	void Start()
	{
		animRayo = GetComponentInChildren<Animator>();
	}
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //solo funciona con player
        {
            animRayo.SetTrigger("rayo");
			
        }
    }
}
