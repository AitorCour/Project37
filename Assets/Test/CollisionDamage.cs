using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public int damage = 1;
	public bool CanDoDamage;

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
		if(CanDoDamage)
		{
			if (other.tag == "Player")
			{
				plBehaviour.Damage(damage);
				//Debug.Log("Harmed");
			}
		}
		
	}
}
