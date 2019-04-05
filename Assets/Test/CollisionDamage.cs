using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour 
{
	private PlayerBehaviour plBehaviour;
	public int damage = 1;
	public bool CanDoDamage;
	void Start ()
	{
		plBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
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
