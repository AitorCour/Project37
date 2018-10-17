using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_Element : MonoBehaviour 
{
	public int life;
	public bool isDead;
	private int iniLife;

	// Use this for initialization
	void Start () 
	{
		isDead = false;
		iniLife = life;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Damage(int hit)
	{
		if(isDead) return;

		life -= hit;

		if(life <= 0)
		{
			life = 0;
			Explosion();
		}
	}
	

	public void Explosion()
	{
		Debug.Log("Explosion");
	}
}
