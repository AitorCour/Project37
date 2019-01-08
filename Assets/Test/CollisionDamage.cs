using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;

	// Use this for initialization
	void Start ()
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			plBehaviour.Damage(1);
			Debug.Log("Harmed");
		}
	}
}
